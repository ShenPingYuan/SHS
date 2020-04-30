// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    $(".login-submit").click(function () {
        $("img.login-loading").css("display", "inline-block");
        $.ajax({
            url: "/Account/Login",
            type: "POST",
            data: {
                UserName: $(".form-login #username").val(),
                Password: $(".form-login #password").val(),
            },
            asycn: false,
            dataType: "json",
            beforeSend: function (res) {
            },
            success: function (res) {
                if (res.code == 0) {
                    $(".form-login .loading>p").html("登陆成功");
                    setTimeout(' window.location.href = "/Home/Index"', 1000);
                    // window.location.href = "/Home/Index";
                } else {
                    setTimeout('$(".form-login .loading").css("display", "none")', 2000);
                }
            }
        });
    })
})
