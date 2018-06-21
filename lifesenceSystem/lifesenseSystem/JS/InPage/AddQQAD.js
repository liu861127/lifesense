$(function () {
    //时间插件
    //$('#ADDate span').html(moment().subtract('hours', 1).format('YYYY-MM-DD HH:mm:ss') + ' - ' + moment().format('YYYY-MM-DD HH:mm:ss'));

    $('#ADDate').daterangepicker(
            {
                // startDate: moment().startOf('day'),
                //endDate: moment(),
                //minDate: '01/01/2012',	//最小时间
                //maxDate: moment(), //最大时间 
                //dateLimit: {
                //    days: 30
                //}, //起止时间的最大间隔
                showDropdowns: true,
                showWeekNumbers: false, //是否显示第几周
                timePicker: true, //是否显示小时和分钟
                timePickerIncrement: 60, //时间的增量，单位为分钟
                timePicker12Hour: false, //是否使用12小时制来显示时间
                //ranges: {
                //    //'最近1小时': [moment().subtract('hours',1), moment()],
                //    '今日': [moment().startOf('day'), moment()],
                //    '昨日': [moment().subtract('days', 1).startOf('day'), moment().subtract('days', 1).endOf('day')],
                //    '最近7日': [moment().subtract('days', 6), moment()],
                //    '最近30日': [moment().subtract('days', 29), moment()]
                //},
                opens: 'right', //日期选择框的弹出位置
                buttonClasses: ['btn btn-default'],
                applyClass: 'btn-small btn-primary blue',
                cancelClass: 'btn-small',
                format: 'YYYY-MM-DD HH:mm:ss', //控件中from和to 显示的日期格式
                separator: ' 至 ',
                locale: {
                    applyLabel: '确定',
                    cancelLabel: '取消',
                    fromLabel: '起始时间',
                    toLabel: '结束时间',
                    customRangeLabel: '自定义',
                    daysOfWeek: ['日', '一', '二', '三', '四', '五', '六'],
                    monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
                            '七月', '八月', '九月', '十月', '十一月', '十二月'],
                    firstDay: 1
                },
                singleDatePicker: true
            }, function (start, end, label) {//格式化日期显示框

                $('#ADDate span').html(start.format('YYYY-MM-DD HH:mm:ss') + ' 至 ' + end.format('YYYY-MM-DD HH:mm:ss'));
            });

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
                $("#QQTxt").val(data.response.title);
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

    $('input[type="checkbox"]').on('ifClicked', function (event) {
        checkonclick();
    });

    var form = $('#form1');
    form.submit(function ()//提交表单
    {
        if ($("#QQTxt").val() == "") {
            $("#lbqq").focus();
            var d = dialog({
                content: '请选择QQ包',
                quickClose: true// 点击空白处快速关闭
            });
            d.show(document.getElementById('lbqq'));
            setTimeout(function () {
                d.close().remove();
            }, 3000);
            return false;
        }
        var options = {
            beforeSubmit: function () {
                return true;
            },
            url: '/Account/AddQQADYes?QQTxt=' + $("#QQTxt").val(),
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
                        location.href = "../Account/ViewQQAD";
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
    $("#treeview_2").addClass("active");
    $("#treeview_2_1").addClass("active");
})