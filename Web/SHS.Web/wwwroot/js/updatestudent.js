layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    var apibase = "/api/students/";

    function getQueryString(name) {
        var result = window.location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }
    LoadClass();
    function LoadInfo(id) {
        $.ajax({
            url: apibase + id,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                $("input[name='studentid']").val(res.studentId);
                $("input[name='studentname']").val(res.studentName);
                $("input[name='englishname']").val(res.englishName);
                $("input[name='password']").val(res.password);
                $("select[name='sex']>option[value='" + res.sex + "']").attr("selected", true);
                $("select[name='classid']>option[value='" + res.classId + "']").attr("selected", true);
                form.render();
            }
        })
    }
    function LoadClass() {
        $.ajax({
            url: "/api/classes/",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='classid']");
                for (var i = 0; i < res.count; i++) {
                    var html = "<option value='" + res.data[i].classId + "'>" + res.data[i].className + "</option> ";
                    select.append(html);
                }
                form.render();
                var id = getQueryString("id");
                if (id == "") {
                    window.location.href = "/html/error.html";
                } else {
                    LoadInfo(id);
                }
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
                "password": data.field.password,
                "classid": parseInt(data.field.classid),
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
});
