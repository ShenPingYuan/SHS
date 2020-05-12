layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    var apibase = "/api/courses/";

    function getQueryString(name) {
        var result = window.location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }

    var id = getQueryString("id");
    if (id == "") {
        window.location.href = "/html/error.html";
    } else {
        LoadInfo(id);
    }
    function LoadInfo(id) {
        $.ajax({
            url: apibase + id,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                $("input[name='coursename']").val(res.courseName);
                $("input[name='englishname']").val(res.englishName);
                $("input[name='coursescore']").val(res.courseScore);
                if (res.isCompulsory) {
                    $("input[name='iscompulsory']").prop("checked", true);
                } else {
                    $("input[name='iscompulsory']").prop("checked", false);
                }
                form.render();
            }
        })
    }
    form.on('submit(update)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: apibase + data.field.courseid,
            type: "PUT",
            data: JSON.stringify({
                "courseid": parseInt(data.field.courseid),
                "coursename": data.field.coursename,
                "englishname": data.field.englishname,
                "coursescore": parseInt(data.field.coursescore),
                "iscompulsory": data.field.iscompulsory == "on" ? true : false,,
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
