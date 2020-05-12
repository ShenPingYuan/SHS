layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    form.on('submit(add)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/courses/",
            type: "POST",
            data: JSON.stringify({
                "coursename": data.field.coursename,
                "englishname": data.field.englishname,
                "coursescore": data.field.coursescore,
                "iscompulsory": data.field.iscompulsory == "on" ? true : false,
            }),
            contentType: "application/json;",
            success: function (res) {
                top.layer.close(index);
                layer.alert("添加成功", {
                    icon: 6,
                }, function () {
                    xadmin.close();
                    xadmin.father_reload();
                });
            },
            error: function (res) {
                top.layer.close(index);
                layer.alert("添加失败", {
                    icon: 6,
                    time: 1000
                }, function () {
                });
            }
        })
        return false;
    });
});
