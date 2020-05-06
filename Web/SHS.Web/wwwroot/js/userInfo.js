var form, $,areaData;
layui.use(['form','layer','upload','laydate'],function(){
    form = layui.form;
    $ = layui.jquery;
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        upload = layui.upload,
        laydate = layui.laydate;

    $.ajax({
        url: "/api/Account/userinfo/current",
        method: "GET",
        success: function (res) {
            alert("收到数据");
        },
        error: function (res) {
            
        }
    });

    upload.render({
        elem: '.userFaceBtn',
        url: '/api/file/uploadImageApi',
        method: "post",
        done: function (res, index, upload) {
            $('.userFace').attr('src', res.data.src);
            //$('.thumbBox').css("background", "#fff");
        }
    });

    //添加验证规则
    form.verify({
        userBirthday : function(value){
            if(!/^(\d{4})[\u4e00-\u9fa5]|[-\/](\d{1}|0\d{1}|1[0-2])([\u4e00-\u9fa5]|[-\/](\d{1}|0\d{1}|[1-2][0-9]|3[0-1]))*$/.test(value)){
                return "出生日期格式不正确！";
            }
        }
    })
    //选择出生日期
    laydate.render({
        elem: '.userBirthday',
        format: 'yyyy年MM月dd日',
        trigger: 'click',
        max : 0,
        mark : {"0-12-15":"生日"},
        done: function(value, date){
            if(date.month === 12 && date.date === 4){ //点击每年12月15日，弹出提示语
                layer.msg('今天是平元兄的生日，快来送上祝福吧！');
            }
        }
    });
    //提交个人资料
    form.on("submit(changeUser)", function (data) {
        var index = layer.msg('提交中，请稍候', { icon: 16, time: false, shade: 0.8 });

        $.ajax({
            url: "/Account/UpdateUser",
            type: "POST",
            data: {
                NickName: data.field.nickName,
                UserName: data.field.userName,
                EnglishName: data.field.englishName,
                UserEmail:data.field.email,
                UserSex:data.field.sex,
                Province:data.field.province,
                City: data.field.city,
                Area: data.field.area,
                BirthDate: data.field.birthDate,
                RealName: data.field.realName,
                PhoneNumber: data.field.phoneNumber,
                UserDesc: data.field.userDescription,
                UserFaceImgUrl:$('.userFace').attr('src'),
            },
            dataType: "json",
            success: function (res) {
                if (res == "SUCCEED") {
                    top.layer.close(index);
                    top.layer.msg("信息修改成功！");
                    layer.closeAll("iframe");
                    //刷新父页面
                    parent.location.reload();
                } else {
                    top.layer.close(index);
                    top.layer.msg("信息修改失败！");
                    layer.closeAll("iframe");
                    //刷新父页面
                    parent.location.reload();
                }
            }
        })
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    })

    //修改密码
    form.on("submit(changePwd)",function(data){
        var index = layer.msg('提交中，请稍候',{icon: 16,time:false,shade:0.8});
        setTimeout(function(){
            layer.close(index);
            layer.msg("密码修改成功！");
            $(".pwd").val('');
        },2000);
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    })
})