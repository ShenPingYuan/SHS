layui.use(['form', 'layer', 'jquery'],
    function () {
        $ = layui.jquery;
        var form = layui.form,
            layer = layui.layer;

        function getQueryString(name) {
            var result = window.location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
            if (result == null || result.length < 1) {
                return "";
            }
            return result[1];
        }

        LoadCourses();
        LoadColleges();
        LoadRoles();
        function LoadCourses() {
            $.ajax({
                url: "/api/courses/",
                type: "GET",
                dataType: "json",
                async: false,
                success: function (res) {
                    var s = $("select[name='courseid']");
                    for (var i = 0; i < res.count; i++) {
                        var html = "<option value='" + res.data[i].courseId + "'>" + res.data[i].courseName + "</option> ";
                        s.append(html);
                    }
                    form.render();
                }
            });
        }
        function LoadColleges() {
            $.ajax({
                url: "/api/colleges/simplecolleges/",
                type: "GET",
                dataType: "json",
                async: false,
                success: function (res) {
                    var s = $("select[name='collegeid']");
                    for (var i = 0; i < res.length; i++) {
                        var html = "<option value='" + res[i].collegeId + "'>" + res[i].collegeName + "</option> ";
                        s.append(html);
                    }
                    form.render();
                }
            })
        }
        function LoadTeacherInfo(id) {
            $.ajax({
                url: "/api/teachers/" + id,
                type: "GET",
                dataType: "json",
                async: false,
                success: function (res) {
                    $("input[name='teacherid']").val(res.teacherId);
                    $("input[name='teacherName']").val(res.teacherName);
                    $("input[name='userEmail']").val(res.userEmail);
                    $("input[name='phoneNumber']").val(res.phoneNumber);
                    $("input[name='age']").val(res.age);
                    $("select[name='courseid']>option[value='" + res.courseId + "']").attr("selected", true);
                    $("select[name='roleid']>option[value='" + res.roleId + "']").attr("selected", true);
                    $("select[name='collegeid']>option[value='" + res.collegeId + "']").attr("selected", true);
                    $("textarea[name='userdescription']").val(res.userDescription);
                }
            });
            form.render();
        }
        function LoadRoles() {
            $.ajax({
                url: "/api/roles/simpleroles/",
                type: "GET",
                dataType: "json",
                async: false,
                success: function (res) {
                    var s = $("select[name='roleid']");
                    for (var i = 0; i < res.length; i++) {
                        var html = "<option value='" + res[i].id + "'>" + res[i].roleName + "</option> ";
                        s.append(html);
                    }
                    form.render();
                    var id = getQueryString("id");
                    if (id == "") {
                        window.location.href = "/html/error.html";
                    } else {
                        LoadTeacherInfo(id);
                    }
                }
            })
        }
        //监听提交
        form.on('submit(update)', function (data) {
            var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
            var tag = false;
            $.ajax({
                url: "/api/teachers/" + data.field.teacherid,
                type: "PUT",
                data: JSON.stringify({
                    "teacherid": parseInt(data.field.teacherid),
                    "courseid": parseInt(data.field.courseid),
                    "collegeid": parseInt(data.field.collegeid),
                    "roleid": data.field.roleid,
                }),
                contentType: "application/json;",
                success: function (res) {
                    tag = true;
                },
                error: function (res) {
                    tag = false;
                }
            })
            $.ajax({
                url: "/api/roles/addtorole",
                type: "POST",
                data: JSON.stringify({
                    "teacherid": parseInt(data.field.teacherid),        
                    "roleid": data.field.roleid,
                }),
                contentType: "application/json;",
                success: function (res) {
                    top.layer.close(index);
                    if (tag == true) {
                        layer.alert("修改成功", {
                            icon: 6,
                        }, function () {
                            xadmin.close();
                            xadmin.father_reload();
                        });
                    }
                },
                error: function (res) {
                    top.layer.close(index);
                    layer.alert("修改失败", {
                        icon: 6,
                        time: 1000
                    }, function () {
                    });
                }
            })
            return false;
        });
    });

var _hmt = _hmt || []; (function () {
    var hm = document.createElement("script");
    hm.src = "https://hm.baidu.com/hm.js?b393d153aeb26b46e9431fabaf0f6190";
    var s = document.getElementsByTagName("script")[0];
    s.parentNode.insertBefore(hm, s);
})();
