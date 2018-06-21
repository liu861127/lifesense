$(function () {
    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#QQTxt option:selected").text() == "") {
            $("#QQTxt").focus();
            var d = dialog({
                content: '请选择QQ包',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('QQTxt'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Account/AddQQAD_ThirdYes?QQTxt=' + $("#QQTxt").val(),
            type: 'POST',
            dataType: "json",
            success: function (context) {
                if (context.result) {
                    if (context.message != "") {
                        var d = dialog({
                            title: '提示',
                            content: context.message,
                        });
                        d.show();
                        setTimeout(function () {
                            d.close().remove();
                        }, 3000);
                        location.href = "../Account/ViewQQAD";
                    }
                }
                else {
                    var d = dialog({
                        title: '提示',
                        content: context.message,
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
    $("#treeview_2").addClass("active");
    $("#treeview_2_1").addClass("active");
});