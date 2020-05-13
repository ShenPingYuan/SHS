layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var form = layui.form;
    var $ = layui.jquery;
    layer = layui.layer;

    $("select[name='collegeid']").change(function () {
        if ($(this).val() != "") {
            LoadClass($(this).val());
        } else {
            $("select[name='classid']").addClass("layui-disabled")
        }
    });

    LoadCollege();
    function LoadCollege() {
        $.ajax({
            url: "/api/colleges/",
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                var select = $("select[name='collegeid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].collegeId + "'>" + res[i].collegeName + "</option> ";
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
    form.on('submit(add)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/students/one/",
            type: "POST",
            data: JSON.stringify({
                "studentname": data.field.studentname,
                "collegeid": data.field.collegeid,
                "classid": data.field.classid,
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
