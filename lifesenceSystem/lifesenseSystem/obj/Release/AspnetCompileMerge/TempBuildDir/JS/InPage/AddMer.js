$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#CustomerName").val() == "") {
            $("#CustomerName").focus();
            var d = dialog({
                content: '客户名称不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('CustomerName'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#UserID").val() == "") {
            $("#UserID").focus();
            var d = dialog({
                content: '用户名不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('UserID'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#ShowUserPwd").val() == "") {
            $("#ShowUserPwd").focus();
            var d = dialog({
                content: '密码不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ShowUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#Tel").val() == "") {
            $("#Tel").focus();
            var d = dialog({
                content: '联系手机不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('Tel'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#QQ").val() == "") {
            $("#QQ").focus();
            var d = dialog({
                content: '腾讯QQ不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('QQ'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#Emaill").val() == "") {
            $("#Emaill").focus();
            var d = dialog({
                content: '电子邮件不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('Emaill'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var loginPwd = $.md5($("#UserID").val() + $("#ShowUserPwd").val() + "yms");
        $("#UserPwd").val(loginPwd);
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Account/AddMerYes/',
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
                        location.href = "../Account/MerManger";
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
    $("#treeview_1").addClass("active");
    $("#treeview_1_2").addClass("active");
});