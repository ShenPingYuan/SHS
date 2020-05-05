// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var webApiHost = "https://localhost:5001/";
    $(".login-submit").click(function () {
        if ($(".form-login input[name='username']").val() == "" ||
            $(".form-login input[name='password']").val() == "") {
            return true;
        }
        $("img.login-loading").css("display", "inline-block");
        $.ajax({
            url: webApiHost + "api/Account/LoginApi",
            type: "POST",
            data: JSON.stringify({
                "username": $(".form-login input[name='username']").val(),
                "password": $(".form-login input[name='password']").val(),
            }),
            contentType: "application/json;",
            asycn: false,
            dataType: "json",
            beforeSend: function (res) {
            },
            success: function (res) {
                $("img.login-loading").css("display", "none");
                if (res.code == 0) {
                    $("input.login-submit").css("color", "#6dff3e");
                    $("input.login-submit").val("登陆成功");
                    setTimeout(' window.location.href = "/Home/Index"', 1000);
                    // window.location.href = "/Home/Index";
                } else {
                    $("input.login-submit").val(res.msg);
                    $("input.login-submit").css("color", "#ffd475");
                    setTimeout(function () {
                        $("input.login-submit").val("登陆");
                        $("input.login-submit").css("color", "white");
                    }, 2000);
                }
            },
            error: function (res) {
                window.location.href = "/html/error.html";
            }
        });
    });
    $(".register-submit").click(function () {
        if ($(".form-register input[name='usernamesignup']").val() == "" ||
            $(".form-register input[name='emailsignup']").val() == "" ||
            $(".form-register input[name='passwordsignup']").val() == "" ||
            $(".form-register input[name='passwordsignup_confirm']").val() == "") {
            return true;
        }
        if ($(".form-register input[name='passwordsignup']").val() != $(".form-register input[name='passwordsignup_confirm']").val()) {

            $("input.register-submit").val("两密码不同");
            $("input.register-submit").css("color", "#ffd475");
            setTimeout(function () {
                $("input.register-submit").val("注册");
                $("input.register-submit").css("color", "white");
            }, 2000);
            //setTimeout('$("input.register-submit").val("注册")', 2000);
            return true;
        }
        $("img.register-loading").css("display", "inline-block");
        
        $.ajax({
            url: webApiHost + "api/Account/RegisterApi",
            type: "POST",
            data: JSON.stringify({
                "userName": $(".form-register input[name='usernamesignup']").val(),
                "password": $(".form-register input[name='passwordsignup']").val(),
                "email": $(".form-register input[name='emailsignup']").val(),

            }),
            contentType: "application/json;",
            asycn: false,
            dataType: "json",
            beforeSend: function (res) {
            },
            success: function (res) {
                $("img.register-loading").css("display", "none");
                if (res.code == 0) {
                    $("input.register-submit").val("注册成功");
                    $("input.register-submit").css("color", "#6dff3e");
                    setTimeout('window.location.href = "/#tologin"', 1000);
                    // window.location.href = "/Home/Index";
                } else {
                    $("input.register-submit").val(res.msg);
                    $("input.register-submit").css("color", "#ffd475");
                    setTimeout(function () {
                        $("input.register-submit").val("注册");
                        $("input.register-submit").css("color", "white");
                    }, 2000);
                }
            },
            error: function (res) {
                window.location.href = "/html/error.html";
            }
        });
    })
})
