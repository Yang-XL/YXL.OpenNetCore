var modelModify = function () {

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
        { id: "00000000-0000-0000-0000-000000000000", pId: "00000000-0000-0000-0000-000000000000", name: "--根目录--" }
    ];

    function beforeClick(treeId, treeNode) {
        return treeNode.id !== 1;
    }

    function onClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj($("#ParentMenuName").attr("treeid"));
       var nodes = zTree.getSelectedNodes();
       if (nodes.length <= 0) return ;
        $("#ParentMenuName").val(nodes[0].name);
        $("#ParentID").val(nodes[0].id);
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
                loadEditData();
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

    function loadEditData() {
        var pid = $("#ParentID").val();
        var zTree = $.fn.zTree.getZTreeObj($("#ParentMenuName").attr("treeid"));
        var nodes = zTree.getNodeByParam("id", pid, null);
        zTree.selectNode(nodes);
        $("#ParentMenuName").val(nodes.name);

        var menuType = $("#MenuType").val();

        $('input[name="MenuType"]').each(function() {
            if ($(this).val() === menuType) {
                $(this).attr("checked", 'checked');
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            ParentMenuNameBind();
            menuTypeClick();
            $('input[name="MenuType"]').change(menuTypeClick);
            $("#ApplicationID").change(function() {
                $("#ParentID").val("");
                $("#ParentMenuName").val("");
                ParentMenuNameBind();
            });
        }
    };
}();

jQuery(document).ready(function () {
    modelModify.init();
});