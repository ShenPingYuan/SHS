
layui.use('laydate',
    function () {
        var laydate = layui.laydate;

        //执行一个laydate实例
        laydate.render({
            elem: '#start' //指定元素
        });

        //执行一个laydate实例
        laydate.render({
            elem: '#end' //指定元素
        });

    });

layui.use('table',
    function () {
        var table = layui.table;

        //监听单元格编辑
        table.on('edit(test)',
            function (obj) {
                var value = obj.value //得到修改后的值
                    ,
                    data = obj.data //得到所在行所有键值
                    ,
                    field = obj.field; //得到字段
                layer.msg('[ID: ' + data.id + '] ' + field + ' 字段更改为：' + value);
            });

        //头工具栏事件
        table.on('toolbar(test)',
            function (obj) {
                var checkStatus = table.checkStatus(obj.config.id);
                switch (obj.event) {
                    case 'getCheckData':
                        var data = checkStatus.data;
                        layer.alert(JSON.stringify(data));
                        break;
                    case 'getCheckLength':
                        var data = checkStatus.data;
                        layer.msg('选中了：' + data.length + ' 个');
                        break;
                    case 'isAll':
                        layer.msg(checkStatus.isAll ? '全选' : '未全选');
                        break;
                };
            });
    });

var _hmt = _hmt || []; (function () {
    var hm = document.createElement("script");
    hm.src = "https://hm.baidu.com/hm.js?b393d153aeb26b46e9431fabaf0f6190";
    var s = document.getElementsByTagName("script")[0];
    s.parentNode.insertBefore(hm, s);
})();

layui.use(['laydate', 'form'], function () {
    var laydate = layui.laydate;
    var form = layui.form;


    // 监听全选
    form.on('checkbox(checkall)', function (data) {

        if (data.elem.checked) {
            $('tbody input').prop('checked', true);
        } else {
            $('tbody input').prop('checked', false);
        }
        form.render('checkbox');
    });

    //执行一个laydate实例
    laydate.render({
        elem: '#start' //指定元素
    });

    //执行一个laydate实例
    laydate.render({
        elem: '#end' //指定元素
    });


});

/*用户-停用*/
function member_stop(obj, id) {
    layer.confirm('确认要停用吗？', function (index) {

        if ($(obj).attr('title') == '启用') {

            //发异步把用户状态进行更改
            $(obj).attr('title', '停用')
            $(obj).find('i').html('&#xe62f;');

            $(obj).parents("tr").find(".td-status").find('span').addClass('layui-btn-disabled').html('已停用');
            layer.msg('已停用!', { icon: 5, time: 1000 });

        } else {
            $(obj).attr('title', '启用')
            $(obj).find('i').html('&#xe601;');

            $(obj).parents("tr").find(".td-status").find('span').removeClass('layui-btn-disabled').html('已启用');
            layer.msg('已启用!', { icon: 5, time: 1000 });
        }

    });
}

/*用户-删除*/
function member_del(obj, id) {
    layer.confirm('确认要删除吗？', function (index) {
        //发异步删除数据
        $(obj).parents("tr").remove();
        layer.msg('已删除!', { icon: 1, time: 1000 });
    });
}

function delAll(argument) {
    var checkStatus = layui.table.checkStatus('test');
    var datas = checkStatus.data;
    var teacherIds = [];
    if (datas.length > 0) {
        for (var i in datas) {
            teacherIds.push(parseInt(datas[i].teacherId));
        }
        layer.confirm('确认要删除选中用户吗？', { icon: 3, title: '提示信息' }, function (index) {
            $.ajax({
                url: "/api/teachers/",
                type: "Delete",
                data: JSON.stringify({
                    teacherIds,
                }),
                contentType: "application/json;",
                dataType:"json",
                success: function (res) {
                    layer.msg("删除成功");
                },
                error: function (res) {
                    layer.msg("删除失败");
                },
            });
        });
    }
}
