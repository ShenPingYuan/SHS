layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    LoadTeachers();
    //LoadCollege();
    function LoadTeachers() {
        $.ajax({
            url: "/api/teachers/Instructors/",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='teacherid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].teacherId + "'>" + res[i].instructorName + "</option> ";
                    select.append(html);
                }
                form.render();
                LoadCollege();
            }
        });
    }
    function LoadCollege() {
        $.ajax({
            url: "/api/colleges/",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='collegeid']");
                for (var i = 0; i < res.count; i++) {
                    var html = "<option value='" + res.data[i].collegeId + "'>" + res.data[i].collegeName + "</option> ";
                    select.append(html);
                }
                form.render();
                var id = getQueryString("id");
                if (id == "") {
                    window.location.href = "/html/error.html";
                } else {
                    LoadInfo(id);
                }

            }
        });
    }
    function getQueryString(name) {
        var result = window.location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }

    function LoadInfo(id) {
        $.ajax({
            url: "/api/classes/" + id,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                $("input[name='classid']").val(res.classId);
                $("input[name='classname']").val(res.className);
                $("input[name='englishname']").val(res.englishName);
                $("select[name='teacherid']>option[value='" + res.instructorName + "']").attr("selected", true);
                form.render();
            }
        })
    }
    form.on('submit(update)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/classes/" + data.field.classid,
            type: "PUT",
            data: JSON.stringify({
                "classid": parseInt(data.field.classid),
                "classname": data.field.classname,
                "englishname": data.field.englishname,
                "teacherid": parseInt(data.field.teacherid),
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
