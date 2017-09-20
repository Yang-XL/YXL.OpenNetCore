var organizationCreate = function () {

    var parentOrganizationSetting = {
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
            onClick: function (e, treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj($("#ParentOrganizationName").attr("treeid"));
                var nodes = zTree.getSelectedNodes();
                if (nodes.length <= 0) return;
                $("#ParentOrganizationName").val(nodes[0].name);
                $("#ParentOrganizationID").val(nodes[0].id);
                $("#ParentOrganizationName").trigger("hideTree");
            }
        }
    };

    var leaderSetting = {
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
            onClick: function (e, treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj($("#LeaderName").attr("treeid"));
                var nodes = zTree.getSelectedNodes();
                if (nodes.length <= 0) return;
                $("#LeaderName").val(nodes[0].name);
                $("#Leader").val(nodes[0].id);
                $("#LeaderName").trigger("hideTree");
            }
        }
    };

    function ParentOrganizationNameBind() {
        $("#ParentOrganizationID").val("");
        $("#ParentOrganizationName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Organization/ZtreeOrganization",
            success: function (data) {
                $("#ParentOrganizationName").ztreeSelect(parentOrganizationSetting, data);
            }
        });
    }


    function LeaderNameBind() {
        $("#Leader").val("");
        $("#ParentOrganizationName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/User/OrganizationZTreeUsers",
            success: function (data) {
                $("#LeaderName").ztreeSelect(leaderSetting, data);
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            ParentOrganizationNameBind();
            LeaderNameBind();
        }
    };
}();

jQuery(document).ready(function () {
    organizationCreate.init();
});