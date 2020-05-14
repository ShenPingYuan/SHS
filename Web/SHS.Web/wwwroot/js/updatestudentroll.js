var form,$;
layui.use(['table', 'form', 'jquery', 'layer', 'laydate'], function () {
    var laydate = layui.laydate;
    var table = layui.table;
    form = layui.form;
    $ = layui.jquery;
    layer = layui.layer;

    var apibase = "/api/students/";

    function getQueryString(name) {
        var result = window.location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }
    LoadCollege();
    function LoadInfo(id) {
        $.ajax({
            url: apibase + id,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                LoadClass(res.collegeId);
                $("input[name='studentid']").val(res.studentId);
                $("input[name='studentname']").val(res.studentName);
                $("input[name='englishname']").val(res.englishName);
                $("input[name='password']").val(res.password);
                $("input[name='birthday']").val(res.birthday);
                $("input[name='age']").val(res.age);
                $("input[name='email']").val(res.email);
                $('#address').xcity(res.province, res.city, res.area);
                $("input[name='userfaceimgurl']").val(res.userFaceImgUrl);
                $("input[name='description']").val(res.description);
                $("select[name='sex']>option[value='" + res.sex + "']").attr("selected", true);
                $("select[name='classid']>option[value='" + res.classId + "']").attr("selected", true);
                $("select[name='collegeid']>option[value='" + res.collegeId + "']").attr("selected", true);
                form.render();
            }
        })
    }
    form.on('select(collegeid)', function (data) {
        $("select[name='classid']").html("<option value=''>请选择班级<option>");
        form.render();
        if (data.value != "") {
            LoadClass(data.value);
        } else {
            $("select[name='classid']").addClass("layui-disabled")
        }
    });
    function LoadCollege() {
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
                var id = getQueryString("id");
                if (id != "") {
                    LoadInfo(id);
                } else {
                    window.location.href = "/html/error.html";
                }
                
            },
            error: function (res) {
                window.location.href = "/html/error.html";
            }
        });
    }
    function LoadClass(para) {
        $.ajax({
            url: "/api/classes/search?collegeid=" + para,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='classid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].classId + "'>" + res[i].className + "</option> ";
                    select.append(html);
                }
                $("select[name='classid']").removeClass("layui-disabled")
                form.render();
            }
        });
    }
    form.on('submit(update)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: apibase + data.field.studentid,
            type: "PUT",
            data: JSON.stringify({
                "studentid": parseInt(data.field.studentid),
                "studentname": data.field.studentname,
                "englishname": data.field.englishname,
                "sex": data.field.sex,
                "classid": parseInt(data.field.classid),
                "description": data.field.description,
                "birthday": data.field.birthday,
                "province": data.field.province,
                "city": data.field.city,
                "area": data.field.area,
                "password": data.field.password,
                "email": data.field.email,
                "userfaceimgurl": data.field.userfaceimgurl
            }),
            contentType: "application/json;",
            success: function (res) {
                top.layer.close(index);
                layer.alert("修改成功", {
                    icon: 6,
                }, function () {
                    xadmin.close();
                    xadmin.father_reload();
                });
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
    laydate.render({
        elem: '#birthday' //指定元素
    });
});
