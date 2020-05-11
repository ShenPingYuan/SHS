layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;
    form.on('submit(addrole)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/roles/",
            type: "POST",
            data: JSON.stringify({
                "positionGrade": parseInt(data.field.positionGrade),
                "roleName": $("select[name='positionGrade']>option:selected").html(),
            }),
            contentType: "application/json;",
            dataType: "json",
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
                    time:1000
                }, function () {
                });
            }
        })
        return false;
    });
});
