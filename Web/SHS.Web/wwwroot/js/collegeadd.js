layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    LoadTeachers();

    function LoadTeachers() {
        $.ajax({
            url: "/api/teachers/deans/",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='teacherid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].teacherId + "'>" + res[i].deanName + "</option> ";
                    select.append(html);
                }
                form.render();
            }
        })
    }

    form.on('submit(add)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/colleges/",
            type: "POST",
            data: JSON.stringify({
                "collegename": data.field.collegename,
                "englishname": data.field.englishname,
                "teacherid": parseInt(data.field.teacherid),
                "deanname": $("select[name='teacherid']>option:selected").html(),
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
