﻿layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var apibase = "/api/scs/";

    LoadCourses();
    LoadCollege();
    form.on('select(collegeid)', function (data) {
        $("select[name='classid']").html("<option value=''>请选择班级<option>");
        form.render();
        if (data.value != "") {
            LoadClass(data.value);
        } else {
            $("select[name='classid']").addClass("layui-disabled")
            $("select[name='classid']").prop("disabled", true);
        }
    });
    function LoadCourses() {
        $.ajax({
            url: "/api/courses/",
            type: "GET",
            dataType: "json",
            async: true,
            success: function (res) {
                var s = $("select[name='courseid']");
                for (var i = 0; i < res.count; i++) {
                    var html = "<option value='" + res.data[i].courseId + "'>" + res.data[i].courseName + "</option> ";
                    s.append(html);
                }
                form.render();
            }
        })
    }

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
                $("select[name='classid']").prop("disabled", false);
                form.render();
            }
        });
    }
    table.on('tool(scs)',
        function (obj) {
            var event = obj.event;
            var data = obj.data;
            if (event === 'delete') {
                DeleteRow(data.studentId);
            } else if (event === "edit") {
                EditRow(data.studentId);
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
    form.on('submit(add)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.5 });
        $.ajax({
            url: apibase,
            type: "POST",
            data: JSON.stringify({
                "courseid": parseInt(data.field.courseid),
                "classid": parseInt(data.field.classid),
            }),
            contentType: "application/json;",
            success: function (res) {
                top.layer.close(index);
                layer.alert("添加成功", {
                    icon: 6,
                }, function () {
                    location.reload();
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
function delAllSc() {
    var checkStatus = layui.table.checkStatus('scs');
    var datas = checkStatus.data;
    CourseIds = [];
    StudentIds = [];
    if (datas.length > 0) {
        for (var i in datas) {
            CourseIds.push(parseInt(datas[i].courseId));
            StudentIds.push(parseInt(datas[i].studentId))
        }
        layer.confirm('确认要删除选中选课信息吗？', { icon: 3, title: '提示信息' }, function (index) {
            $.ajax({
                url: "/api/scs/",
                type: "Delete",
                data: {
                    CourseIds: CourseIds,
                    StudentIds: StudentIds
                },
                success: function (res) {
                    layer.msg("删除成功");
                    $(".layui-form-checked").not('.header').parents('tr').remove();
                },
                error: function (res) {
                    layer.msg("删除失败");
                },
            });
        });
    }
}