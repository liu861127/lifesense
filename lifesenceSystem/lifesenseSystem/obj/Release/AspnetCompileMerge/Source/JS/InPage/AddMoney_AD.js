$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Admin/AddMoneyYes?seluserid=' + $("#seluser").val() + '&selusername=' + $("#seluser option:selected").text(),
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
                        location.href = "../Admin/ViewMoney";
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
    $("#treeview_6").addClass("active");
    $("#treeview_6_1").addClass("active");
});

function Valconfirm() {
    if ($("#RechargeAmount").val() == "") {
        $("#RechargeAmount").focus();
        var d = dialog({
            content: '充值金额不能为空',
            quickClose: true// 点击空白处快速关闭
        });
        d.show(document.getElementById('RechargeAmount'));
        setTimeout(function () {
            d.close().remove();
        }, 3000);
        return;
    }
    if (parseFloat($("#RechargeAmount").val()) == 0) {
        $("#RechargeAmount").focus();
        var d = dialog({
            content: '充值金额不能等于0',
            quickClose: true// 点击空白处快速关闭
        });
        d.show(document.getElementById('RechargeAmount'));
        setTimeout(function () {
            d.close().remove();
        }, 3000);
        return;
    }
    if ($("#seluser option:selected").text() == "") {
        $("#seluser").focus();
        var d = dialog({
            content: '请选择用户名',
            quickClose: true// 点击空白处快速关闭
        });
        d.show(document.getElementById('seluser'));
        setTimeout(function () {
            d.close().remove();
        }, 3000);
        return;
    }
    var d = dialog({
        title: '提示',
        content: '是否确认给' + $("#seluser option:selected").text() + '充值' + $("#RechargeAmount").val() + '金额?',
        okValue: '是',
        ok: function () {
            $("#sub").click();
        },
        cancelValue: '否',
        cancel: function () {

        }
    });
    d.show();
}