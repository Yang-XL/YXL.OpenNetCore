var RoleMenu = function () {
    
    function Tree_OnCheck(event, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("RoleMenus");
        var checkedNodes = zTree.getCheckedNodes();
        var div = $("#RoleMenusSelect");
        div.html("");
        for (var i = 0; i < checkedNodes.length; i++) {
            var input = document.createElement("input");  
            input.setAttribute("type", "hidden");
            input.setAttribute("name", "RoleMenus[" + i + "]");
            input.setAttribute("id", "RoleMenus_" + i);
            input.value = checkedNodes[i].id;
            div.append(input);
        }
    };

    var TreeSetting = {
        view: {
            dblClickExpand: false,
            selectedMulti: false,
            nameIsHTML: true
        }, 
        check: {
            enable: true,
            chkStyle: "checkbox",
            radioType: "all"
        },
        data: {
            simpleData: {
                enable: true
            },key: {
                title: "description"
            }
        },
        callback: {
            onCheck: Tree_OnCheck
        }
    };

    function BindTree() {
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Menu/RoleMenus",
            data: { roleID: $("#ID").val()},
            success: function (data) {
                $.fn.zTree.init($("#RoleMenus"), TreeSetting, data);
                BuidCheckTree();
            },
            error: function (result) {
                alert("加载树出错：" + result.status);
            }
        });
    }

    function BuidCheckTree() {
        var zTree = $.fn.zTree.getZTreeObj("RoleMenus");
        $("#RoleMenusSelect").children("input").each(function() {
            var node = zTree.getNodeByParam("id", $(this).val(), null);
            zTree.selectNode(node);
        });
    }
   
    return {
        init: function () {
            BindTree();
        }
    };
}();

jQuery(document).ready(function () {
    RoleMenu.init();
});