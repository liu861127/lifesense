$(function () {
    $("#example1").dataTable(
        {
            //lengthMenu: [5, 10, 20, 30],//这里也可以设置分页，但是不能设置具体内容，只能是一维或二维数组的方式，所以推荐下面language里面的写法。
            paging: true,//分页
            ordering: true,//是否启用排序
            searching: false,//搜索
            language: {
                lengthMenu: '显示 <select class="form-control input-sm" aria-controls="example1" name="example1_length">' + '<option value="5">5</option>' + '<option value="10">10</option>' + '<option value="20">20</option>' + '<option value="30">30</option></select> 行',//左上角的分页大小显示。
                search: '<span class="label label-success">搜索：</span>',//右上角的搜索文本，可以写html标签

                paginate: {//分页的样式内容。
                    previous: "上一页",
                    next: "下一页",
                    first: "第一页",
                    last: "最后"
                },

                zeroRecords: "没有内容",//table tbody内容为空时，tbody的内容。
                //下面三者构成了总体的左下角的内容。
                info: "总共_PAGES_ 页，显示第_START_ 到第 _END_ ，筛选之后得到 _TOTAL_ 条，初始_MAX_ 条 ",//左下角的信息显示，大写的词为关键字。
                infoEmpty: "0条记录",//筛选为空时左下角的显示。
                infoFiltered: ""//筛选之后的左下角筛选提示，
            },
            paging: true,
            pagingType: "full_numbers",//分页样式的类型
            iDisplayLength: 5 //每页默认显示5条记录
        });

    $(".select2").select2({
        language: "zh-CN"
    });

    $("#example1_wrapper").find(".row").eq(1).wrap("<div style='overflow-x:auto;'></div>");
    $(".treeview").removeClass("active");
    $(".treeview-menu").find("li").removeClass("active");
    $("#treeview_5").addClass("active");
    $("#treeview_5_2").addClass("active");
});