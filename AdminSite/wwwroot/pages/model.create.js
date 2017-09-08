var modelCreate = function () {

    var setting = {
        view: {
            dblClickExpand: false
        },
        check: { enable: true, chkStyle: "radio", radioType: "level" },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeClick: beforeClick,
            onClick: onClick
        }
    };
    var zNodes = [
        { id: 1, pId: 0, name: "--未搜索到任何菜单--" }
    ];

    function beforeClick(treeId, treeNode) {
        return treeNode.id !== 1;
    }

    function onClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj($("#ParentMenuName").attr("treeid"));
       var nodes = zTree.getSelectedNodes();
       if (nodes.length <= 0) return ;
        $("#ParentMenuName").attr("value", nodes[0].name);
        $("#ParentID").attr("value", nodes[0].id);
        $("#ParentMenuName").trigger("hideTree");
    }

    function ParentMenuNameBind() {
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Menu/QueryJson",
            data: { applicationId: $("#ApplicationID").val() },
            success: function (data) {
                if (data.length <= 0) {
                    $("#ParentMenuName").ztreeSelect(setting, zNodes);
                } else {
                    $("#ParentMenuName").ztreeSelect(setting, data);
                }
            }
        });
    }

    function menuTypeClick() {
        if ($('input[name="MenuType"]:checked').val() === "1") {
            $("#ActionName").val("").prop("readonly", "readonly");
            $("#ControllerName").val("").prop("readonly", "readonly");
            $("#AreaName").val("").prop("readonly", "readonly");
        } else {
            $("#ActionName").removeAttr("readonly");
            $("#ControllerName").removeAttr("readonly");
            $("#AreaName").removeAttr("readonly");
        }
    }

    return {
        //main function to initiate the module
        init: function () {
            ParentMenuNameBind();
            menuTypeClick();
            $('input[name="MenuType"]').change(menuTypeClick);
            $("#ApplicationID").change(ParentMenuNameBind);
        }
    };
}();

jQuery(document).ready(function () {
    modelCreate.init();
});