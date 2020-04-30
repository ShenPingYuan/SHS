// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var webApiHost = "https://localhost:5002/";
    $(".login-submit").click(function () {
        $("img.login-loading").css("display", "inline-block");
        $.ajax({
            url: webApiHost+"api/Account/LoginApi",
            type: "POST",
            data: JSON.stringify({
                //"username":$(".form-login #username").val(),
                //"password": $(".form-login #password").val(),
                userName: "123",
                password: "456"
            }),
            contentType: "application/json;",
            asycn: false,
            dataType: "json",
            beforeSend: function (res) {
            },
            success: function (res) {
                $("img.login-loading").css("display", "none");
                if (res.code == 0) {
                    $("input.login-submit").val("登陆成功");
                    setTimeout(' window.location.href = "/Home/Index"', 1000);
                    // window.location.href = "/Home/Index";
                } else {
                    $("input.login-submit").val(res.msg);
                    setTimeout('$("input.login-submit").val("登陆")', 2000);
                }
            },
            error: function (res) {
                window.location.href = "/html/error.html";
            }
        });
    })
})
