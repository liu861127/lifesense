$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#seluser").val() == "") {
            $("#seluser").focus();
            var d = dialog({
                content: '请选择需要添加子代理商的客户',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('seluser'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
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
        if ($("#ShowUserPwd").val() == "") {
            $("#ShowUserPwd").focus();
            var d = dialog({
                content: '用户名不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ShowUserPwd'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#UserPwd").val() == "") {
            $("#UserPwd").focus();
            var d = dialog({
                content: '密码不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('UserPwd'));
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
            url: '/Admin/AddMerYes?seluser=' + $("#seluser").val(),
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
                        location.href = "../Admin/MerManger";
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

    $(".select2").select2({
        language: "zh-CN"
    });

    $(".treeview").removeClass("active");
    $(".treeview-menu").find("li").removeClass("active");
    $("#treeview_1").addClass("active");
    $("#treeview_1_2").addClass("active");
});