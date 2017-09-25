var organizationCreate = function () {



    var leaderSetting = {
        view: {
            dblClickExpand: false
        },
        check: {
            enable: true,
            chkStyle: "radio"
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                return !treeNode.chkDisabled;
            }, onClick: function (e, treeId, treeNode) {
                $("#LeaderName").val(treeNode.name);
                $("#Leader").val(treeNode.id);
                $("#LeaderName").trigger("hideTree");
            }
        }
    };
    function LeaderNameBind() {
        $("#Leader").val("");
        $("#LeaderName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            contentType: "application/json;charset=utf-8", 
            url: "/User/OrganizationZTreeUsers",
            success: function (data) {
                $("#LeaderName").ztreeSelect(leaderSetting, data);
            },
            error: function (result) {
                alert(result.status + "：" + result.statusText);
            }
        });
    }


    var parentOrganizationSetting = {
        view: {
            dblClickExpand: false
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            onClick: function(e, treeId, treeNode) {
                $("#ParentOrganizationName").val(treeNode.name);
                $("#ParentOrganizationID").val(treeNode.id);
                $("#ParentOrganizationName").trigger("hideTree");
            }
        }
    };
    function ParentOrganizationNameBind() {
        $("#ParentOrganizationID").val("");
        $("#ParentOrganizationName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            contentType: "application/json;charset=utf-8", 
            url: "/Organization/ZtreeOrganization",
            success: function (data) {
                $("#ParentOrganizationName").ztreeSelect(parentOrganizationSetting, data);
            },
            error: function (result) {
                alert(result.status + "：" + result.statusText);
            }
        });
    }




    return {
        //main function to initiate the module
        init: function () {
            LeaderNameBind();
            ParentOrganizationNameBind();
        }
    };
}();

jQuery(document).ready(function () {
    organizationCreate.init();
});