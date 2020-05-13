layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var $ = layui.jquery;
    var layer = layui.layer;
    table.on('tool(classes)',
        function (obj) {
            var event = obj.event;
            var data = obj.data;
            if (event === 'delete') {
                DeleteRow(data.classid);
            } else if (event === "edit") {
                EditRow(data.classid);
            }
        });
    function DeleteRow(para) {
        //删除学院
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/classid/" + para,
            type: "delete",
            success: function (res) {
                top.layer.close(index);
                layer.alert("删除成功", {
                    icon: 6,
                }, function () {
                    location.reload();
                });
            },
            error: function (res) {
                top.layer.close(index);
                layer.alert("删除失败", {
                    icon: 6,
                }, function () {
                    location.reload();
                });
            }
        })
    }
    function EditRow(para) {
        //编辑学院信息
        xadmin.open('编辑', '/html/updateclass.html?collegeid=' + para);
    }
});