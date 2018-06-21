$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
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
            return false;
        }
        if ($("#SummaryDate").val() == "") {
            $("#SummaryDate").focus();
            var d = dialog({
                content: '时间不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('SummaryDate'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#ADExposure").val() == "") {
            $("#ADExposure").focus();
            var d = dialog({
                content: '曝光量不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ADExposure'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#ClickRate").val() == "") {
            $("#ClickRate").focus();
            var d = dialog({
                content: '点击量不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('ClickRate'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#CTR").val() == "") {
            $("#CTR").focus();
            var d = dialog({
                content: '点击率不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('CTR'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        if ($("#Expense").val() == "") {
            $("#Expense").focus();
            var d = dialog({
                content: '花费不能为空',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('Expense'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Admin/AddStatisticalSummaryYes?seluser=' + $("#seluser").val(),
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
                        location.href = "../Admin/StatisticalSummary";
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
    $("#treeview_5").addClass("active");
    $("#treeview_5_3").addClass("active");
});

