layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var apibase = "/api/students/";
    table.on('tool(newstudents)',
        function (obj) {
            var event = obj.event;
            var data = obj.data;
            if (event === 'delete') {
                DeleteRow(data.studentid);
            } else if (event === "edit") {
                EditRow(data.studentid);
            }
        });
    function DeleteRow(para) {
        //删除学院
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: apibase + para,
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

    LoadColleges();

    function LoadColleges() {
        $.ajax({
            url: "/api/colleges/simplecolleges",
            type: "GET",
            dataType: "json",
            async: true,
            success: function (res) {
                var select = $("select[name='collegeid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].collegeId + "'>" + res[i].collegeName + "</option> ";
                    select.append(html);
                }
                form.render();
            }
        })
    }

    function EditRow(para) {
        //编辑学院信息
        xadmin.open('编辑', '/html/updatecourse.html?id=' + para);
    }
    form.on('submit(add)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.5 });
        $.ajax({
            url: apibase,
            type: "POST",
            data: JSON.stringify({
                "studentname": data.field.studentname,
                "collegeid": parseInt(data.field.collegeid),
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