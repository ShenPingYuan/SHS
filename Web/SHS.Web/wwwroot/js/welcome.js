layui.use(['form'], function () {
    var $ = layui.jquery;

    var date = new Date();
    var currentTime = date.getFullYear() + "-" + date.getMonth() + "-" + date.getDate() + "  " + date.getHours() + ":"
        + date.getMinutes() + ":" + date.getSeconds();
    $(".currentTime").html("当前时间：" + currentTime);

});