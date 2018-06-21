$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#OldUserPwd").val() == "") {
            $("#OldUserPwd").focus();
            var d = dialog({
                content: '原始密码不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('OldUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#NewUserPwd").val() == "") {
            $("#NewUserPwd").focus();
            var d = dialog({
                content: '新密码不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('NewUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#ConfirmUserPwd").val() == "") {
            $("#ConfirmUserPwd").focus();
            var d = dialog({
                content: '确认密码不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ConfirmUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#NewUserPwd").val() != $("#ConfirmUserPwd").val()) {
            $("#ConfirmUserPwd").focus();
            var d = dialog({
                content: '新密码与确认密码不同',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ConfirmUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var userid = $("#hiduser").val();
        var oldPwd = $.md5(userid + $("#OldUserPwd").val() + "yms");
        var newPwd = $.md5(userid + $("#NewUserPwd").val() + "yms");
        $("#HiOldUserPwd").val(oldPwd);
        $("#HiNewUserPwd").val(newPwd);
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Account/ModifyPasswordYes/',
            type: 'POST',
            dataType: "json",
            success: function (context) {
                if (context.result) {
                    if (context.message != "") {
                        var d = dialog({
                            title: '提示',
                            content: context.message,
                            quickClose: true// 点击空白处快速关闭
                        });
                        d.show();
                        setTimeout(function () {
                            d.close().remove();
                        }, 3000);
                        location.href = "../Account/Login";
                    }
                }
                else {
                    var d = dialog({
                        title: '提示',
                        content: context.message,
                        quickClose: true// 点击空白处快速关闭
                    });
                    d.show();
                    setTimeout(function () {
                        d.close().remove();
                    }, 3000);
                }
            },
            error: function (XMLResponse) {
                var d = dialog({
                    title: '提示',
                    content: XMLResponse.responseText,
                    quickClose: true// 点击空白处快速关闭
                });
                d.show();
                setTimeout(function () {
                    d.close().remove();
                }, 3000);
            }

        };
        form.ajaxSubmit(options);
        return false; //为了不刷新页面,返回false
    });

    $(".treeview").removeClass("active");
    $(".treeview-menu").find("li").removeClass("active");
    $("#treeview_4").addClass("active");
    $("#treeview_4_2").addClass("active");
});