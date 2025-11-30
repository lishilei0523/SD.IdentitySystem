(function (object) {
    object.extend(object, {
        //EasyUI扩展对象
        easyuiExt: {
            //更新tab里的DataGrid组件
            updateGridInTab: function () {
                //清除选中，刷新表格datagrid
                window.top.topHelper.updateGridInTab();
            },

            //更新tab里的TreeGrid组件
            updateTreeGridInTab: function () {
                //清除选中，刷新表格TreeGrid
                window.top.topHelper.updateTreeGridInTab();
            },

            //代理方法：调用后台主页面的topHelper.showWindow方法
            showWindow: function (title, url, width, height, resizable) {
                window.top.topHelper.showWindow(title, url, width, height, resizable);
            },

            //代理方法：调用后台主页面的topHelper.showComWindow(title, url, width, height)方法
            closeWindow: function () {
                window.top.topHelper.closeWindow();
            },

            //消息框
            messager: window.top.$.messager,

            //EasyUI Grid组件属性设置
            datagrid: {
                url: null,
                title: null,
                columns: null,
                fitColumns: false,
                fit: true,
                idField: "Id",
                loadMsg: "正在加载...",
                //多选
                singleSelect: true,
                selectOnCheck: false,
                checkOnSelect: false,
                rownumbers: true,
                //启用分页
                pagination: true,
                //第一次请求默认请求的页码
                pageNumber: 1,
                //页容量数组
                pageList: [5, 10, 15, 20, 30, 50],
                //页容量（必须和 pageList 里某一个值一致）
                pageSize: 20,
                queryParams: null,
                onLoadSuccess: null,
                //工具栏
                toolbar: [],
                //工具栏元素Id
                toolbarId: null,
                footer: null,
                init: function (url, columns, idFiled) {
                    this.url = url;
                    this.columns = columns;
                    this.idField = idFiled;
                }
            },

            //EasyUI TreeGrid组件属性设置
            treegrid: {
                url: null,
                title: null,
                columns: null,
                fitColumns: false,
                fit: true,
                idField: "Id",
                treeField: 'Name',
                loadMsg: "正在加载...",
                singleSelect: true,
                rownumbers: true,
                queryParams: null,
                onLoadSuccess: null,
                //工具栏
                toolbar: [],
                //工具栏元素Id
                toolbarId: null,
                checkbox: null,
                footer: null,
                init: function (url, columns, idFiled) {
                    this.url = url;
                    this.columns = columns;
                    this.idField = idFiled;
                }
            }
        }
    });
})($);
