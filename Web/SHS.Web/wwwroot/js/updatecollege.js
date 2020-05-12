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
                var collegeid = getQueryString("collegeid");
                if (collegeid == "") {
                    window.location.href = "/html/error.html";
                } else {
                    LoadCollegeInfo(collegeid);
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

    function LoadCollegeInfo(collegeId) {
        $.ajax({
            url: "/api/colleges/" + collegeId,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (res) {
                $("input[name='collegeid']").val(res.collegeId);
                $("input[name='collegename']").val(res.collegeName);
                $("input[name='englishname']").val(res.englishName);
                $("select[name='teacherid']>option[value='" + res.teacherId + "']").attr("selected", true);
                form.render();
            }
        })
    }
    form.on('submit(update)', function (data) {
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.ajax({
            url: "/api/colleges/" + data.field.collegeid,
            type: "PUT",
            data: JSON.stringify({
                "collegeid": parseInt(data.field.collegeid),
                "collegename": data.field.collegename,
                "englishname": data.field.englishname,
                "teacherid": parseInt(data.field.teacherid),
                "deanname": $("select[name='teacherid']>option:selected").html(),
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
