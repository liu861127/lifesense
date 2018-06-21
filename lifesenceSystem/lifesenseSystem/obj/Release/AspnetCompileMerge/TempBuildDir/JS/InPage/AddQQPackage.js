$(function () {
    //初始化fileinput
    var control = $('#exampleInputFile');
    //初始化上传控件的样式
    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: "../Account/TxtUpload", //上传的地址
        allowedFileExtensions: ['txt'],//接收的文件后缀
        showUpload: true, //是否显示上传按钮
        showCaption: true,//是否显示标题
        browseClass: "btn btn-primary", //按钮样式
        dropZoneEnabled: false,//是否显示拖拽区域
        //minImageWidth: 50, //图片的最小宽度
        //minImageHeight: 50,//图片的最小高度
        //maxImageWidth: 1000,//图片的最大宽度
        //maxImageHeight: 1000,//图片的最大高度
        maxFileSize: 2048,//单位为kb，如果为0表示不限制文件大小
        //minFileCount: 0,
        //maxFileCount: 10, //表示允许同时上传的最大文件个数
        enctype: 'multipart/form-data',
        validateInitialCount: true,
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        msgFilesTooMany: "选择上传的文件数量({n}) 超过允许的最大数值{m}！",
        showPreview: false,
        showRemove: false,
        showZoom: false,
        showDelete: false
    });
    //导入文件上传完成之后的事件
    $('#exampleInputFile').on("fileuploaded", function (event, data, previewId, index) {
        if (data == undefined) {
            var d = dialog({
                title: '提示',
                content: '文件格式类型不正确',
                quickClose: true// 点击空白处快速关闭
            });
            d.show();
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return;
        }
        else {
            if (data.response.result) {
                $("#QQPName").val(data.response.title);
                var d = dialog({
                    title: '提示',
                    content: data.response.message,
                    quickClose: true// 点击空白处快速关闭
                });
                d.show();
                setTimeout(function () {
                    d.close().remove();
                }, 3000);
            }
            else {
                var d = dialog({
                    title: '提示',
                    content: data.response.message,
                    quickClose: true// 点击空白处快速关闭
                });
                d.show();
                setTimeout(function () {
                    d.close().remove();
                }, 3000);
            }
        }
    });

    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#QQPName").val() == "") {
            $("#QQPName").focus();
            var d = dialog({
                content: '请上传QQ包',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('QQPName'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Account/AddQQPackageYes/',
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
                        location.href = "../Account/ViewQQPackage";
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
    $("#treeview_3").addClass("active");
    $("#treeview_3_2").addClass("active");
});