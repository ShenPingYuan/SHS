layui.use(['form', 'layer', 'jquery'],
    function () {
        $ = layui.jquery;
        var form = layui.form,
            layer = layui.layer;

        //自定义验证规则
        form.verify({
            username: function (value) {
                if (value.length < 5) {
                    return '账号至少得5个字符';
                }
            },
            password: [/(.+){6,12}$/, '密码必须6到12位'],
            repassword: function (value) {
                if ($('#password').val() != $('#repassword').val()) {
                    return '两次密码不一致';
                }
            }
        });
        $("#username").keyup(function () {
            if (this.value.length >= 5) {
                $(".username-text").html("");
            }
        });
        //监听提交
        form.on('submit(add)',
            function (data) {
                var flag = false;
                //发异步，把数据提交给php
                $.ajax({
                    url: "/api/teachers/checkuser/" + data.field.username,
                    type: "Get",
                    dataType: "json",
                    async:false,
                    success(res) {
                        if (res == false) {
                            $(".username-text").html("用户已存在");
                        } else {
                            flag = true;
                        }
                    }
                });
                if (flag == false) {
                    return false;
                }
                $.ajax({
                    url: "/api/teachers",
                    type: "Post",
                    async: false,
                    data: JSON.stringify({
                        "username": data.field.username,
                        "password": data.field.password,
                    }),
                    contentType:"application/json",
                    success: function (res) {
                        layer.alert("增加成功", {
                            icon: 6
                        }, function () {
                            //关闭当前frame
                            xadmin.close();

                            // 可以对父窗口进行刷新
                            xadmin.father_reload();
                        });
                    },
                    error: function (res) {
                        layer.alert("增加失败", {
                            icon: 6
                        }, function () {
                            //关闭当前frame
                            xadmin.close();

                            // 可以对父窗口进行刷新
                            xadmin.father_reload();
                        });
                    }
                });
                return false;
            });
    });

var _hmt = _hmt || []; (function () {
    var hm = document.createElement("script");
    hm.src = "https://hm.baidu.com/hm.js?b393d153aeb26b46e9431fabaf0f6190";
    var s = document.getElementsByTagName("script")[0];
    s.parentNode.insertBefore(hm, s);
})();
