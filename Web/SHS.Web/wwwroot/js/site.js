// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var webApiHost = "https://localhost:5002/";
    $(".login-submit").click(function () {
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
    });
    //$(".form-register").validate({
    //    submitHandler: function (form) {
    //        $(".register-submit").click(function () {
    //            if ($(".form-register input[name='passwordsignup']").val() != $(".form-register input[name='passwordsignup_confirm']").val()) {
    //                $("input.register-submit").val("两密码不同");
    //                setTimeout('$("input.register-submit").val("注册")', 2000);
    //                return false;
    //            }
    //            $("img.register-loading").css("display", "inline-block");
    //            $.ajax({
    //                url: webApiHost + "api/Account/RegisterApi",
    //                type: "POST",
    //                data: JSON.stringify({
    //                    "username": $(".form-register input[name='username']").val(),
    //                    "password": $(".form-register input[name='passwordsignup']").val(),
    //                    "email": $(".form-register input[name='emailsignup']").val(),

    //                }),
    //                contentType: "application/json;",
    //                asycn: false,
    //                dataType: "json",
    //                beforeSend: function (res) {
    //                },
    //                success: function (res) {
    //                    $("img.register-loading").css("display", "none");
    //                    if (res.code == 0) {
    //                        $("input.register-submit").val("注册成功");
    //                        setTimeout(' window.location.href = "/Home/Index"', 1000);
    //                        // window.location.href = "/Home/Index";
    //                    } else {
    //                        $("input.register-submit").val(res.msg);
    //                        setTimeout('$("input.register-submit").val("注册")', 2000);
    //                    }
    //                },
    //                error: function (res) {
    //                    window.location.href = "/html/error.html";
    //                }
    //            });
    //        })
    //    }
    //});
    $(".register-submit").click(function () {
        if ($(".form-register input[name='usernamesignup']").val() == "" ||
            $(".form-register input[name='emailsignup']").val() == "" ||
            $(".form-register input[name='passwordsignup']").val() == "" ||
            $(".form-register input[name='passwordsignup_confirm']").val() == "") {
            return true;
        }
        if ($(".form-register input[name='passwordsignup_confirm']").val() != $(".form-register input[name='passwordsignup_confirm']").val()) {
            $("input.register-submit").val("两密码不同");
            setTimeout('$("input.register-submit").val("注册")', 2000);
            return false;
        }
        $("img.register-loading").css("display", "inline-block");
        
        $.ajax({
            url: webApiHost + "api/Account/RegisterApi",
            type: "POST",
            data: JSON.stringify({
                "username": $(".form-register input[name='username']").val(),
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
                    setTimeout(' window.location.href = "/Home/Index"', 1000);
                    // window.location.href = "/Home/Index";
                } else {
                    $("input.register-submit").val(res.msg);
                    setTimeout('$("input.register-submit").val("注册")', 2000);
                }
            },
            error: function (res) {
                window.location.href = "/html/error.html";
            }
        });
    })
})
