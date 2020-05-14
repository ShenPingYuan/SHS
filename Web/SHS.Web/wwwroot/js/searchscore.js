layui.use(['table', 'form', 'jquery', 'layer'], function () {
    var table = layui.table;
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var apibase = "/api/scs/";


    LoadCourses();
    LoadColleges();
    LoadStudents();
    LoadClasses();
    LoadTable("");
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
    function LoadColleges() {
        $.ajax({
            url: "/api/colleges/simplecolleges/",
            type: "GET",
            dataType: "json",
            async: true,
            success: function (res) {
                var s = $("select[name='collegeid']");
                for (var i = 0; i < res.length; i++) {
                    var html = "<option value='" + res[i].collegeId + "'>" + res[i].collegeName + "</option> ";
                    s.append(html);
                }
                form.render();
            }
        })
    }
    function LoadStudents() {
        $.ajax({
            url: "/api/students/",
            type: "GET",
            dataType: "json",
            async: true,
            success: function (res) {
                var s = $("select[name='studentid']");
                for (var i = 0; i < res.count; i++) {
                    var html = "<option value='" + res.data[i].studentId + "'>" + res.data[i].studentName + "</option> ";
                    s.append(html);
                }
                form.render();
            }
        })
    }
    function LoadClasses() {
        $.ajax({
            url: "/api/classes/",
            type: "GET",
            dataType: "json",
            async: true,
            success: function (res) {
                var s = $("select[name='classid']");
                for (var i = 0; i < res.count; i++) {
                    var html = "<option value='" + res.data[i].classId + "'>" + res.data[i].className + "</option> ";
                    s.append(html);
                }
                form.render();
            }
        })
    }

    function LoadTable(para) {
        table.render({
            elem: '#scs',
            url: '/api/scs/search' + para,
            cellMinWidth: 95,
            page: true,
            height: "full-125",
            limits: [10, 15, 20, 25],
            limit: 10,
            toolbar: '#toolbarscore',
            id: "scs",
            cols: [[
                { type: "checkbox", fixed: "left", width: 50 },
                { field: "courseId", title: '课程号', fixed: "left", sort: "true", align: 'center'},
                { field: 'courseName', title: '课程名字', align: "center" },
                { field: 'studentId', title: '学号', align: "center" },
                { field: 'studentName', title: '学生名字', align: "center" },
                { field: 'score', title: '分数', align: "center",sort: "true"},
            ]]
        });
    }
    form.on('select(collegeid)', function () {
        $("select[name='classid']>option[value='']").prop("selected", true);
        $("select[name='studentid']>option[value='']").prop("selected", true);
        $("select[name='courseid']>option[value='']").prop("selected", true);
        form.render();
    });
    form.on('select(courseid)', function () {
        $("select[name='classid']>option[value='']").prop("selected", true);
        $("select[name='studentid']>option[value='']").prop("selected", true);
        $("select[name='collegeid']>option[value='']").prop("selected", true);
        form.render();
    });
    form.on('select(studentid)', function () {
        $("select[name='classid']>option[value='']").prop("selected", true);
        $("select[name='collegeid']>option[value='']").prop("selected", true);
        $("select[name='courseid']>option[value='']").prop("selected", true);
        form.render();
    });
    form.on('select(classid)', function () {
        $("select[name='studentid']>option[value='']").prop("selected", true);
        $("select[name='collegeid']>option[value='']").prop("selected", true);
        $("select[name='courseid']>option[value='']").prop("selected", true);
        form.render();
    });
    $("#searchbtn").click(function () {
        var courseid = $("select[name='courseid']").val();
        var studentid = $("select[name='studentid']").val();
        var collegeid = $("select[name='collegeid']").val();
        var classid = $("select[name='classid']").val();
        var para = "?";
        if (courseid != "" && studentid == "" && collegeid == "" && classid == "") {
            para += "courseid=" + courseid;
        } else if (courseid == "" && studentid != "" && collegeid == "" && classid == "") {
            para += "studentid=" + studentid;
        } else if (courseid == "" && studentid == "" && collegeid != "" && classid == "") {
            para += "collegeid=" + collegeid;
        } else if (courseid == "" && studentid == "" && collegeid == "" && classid != "") {
            para += "classid=" + classid;
        } else {
            layer.msg("请选择一个查询条件");
            return;
        }
        LoadTable(para);
        return false;
    })
});