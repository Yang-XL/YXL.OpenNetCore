var organizationCreate = function () {

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
            onClick: onClick
        }
    };

    function onClick(e, treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj($("#ParentMenuName").attr("treeid"));
       var nodes = zTree.getSelectedNodes();
       if (nodes.length <= 0) return ;
        $("#ParentMenuName").val(nodes[0].name);
        $("#ParentID").val(nodes[0].id);
        $("#ParentMenuName").trigger("hideTree");
    }

    function ParentMenuNameBind() {
        $("#ParentID").val("");
        $("#ParentMenuName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Menu/QueryJson",
            data: { applicationId: $("#ApplicationID").val(),isNav:true },
            success: function (data) {
                    $("#ParentMenuName").ztreeSelect(setting, data);
            }
        });
    }



    return {
        //main function to initiate the module
        init: function () {
            ParentMenuNameBind();
          
        }
    };
}();

jQuery(document).ready(function () {
    organizationCreate.init();
});