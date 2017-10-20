var userModify = function () {

    function LoadOrganizatioData() {
        var organizationID = $("#OrganizationID").val();
        var zTree = $.fn.zTree.getZTreeObj($("#OrganizationName").attr("treeid"));
        var nodes = zTree.getNodeByParam("id", organizationID, null);
        zTree.selectNode(nodes);
        $("#OrganizationName").val(nodes.name);
    }
    var OrganizationSetting = {
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
                $("#OrganizationName").val(treeNode.name);
                $("#OrganizationID").val(treeNode.id);
                $("#OrganizationName").trigger("hideTree");
            }
        }
    };

    function OrganizationNameBind() {
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Organization/ZtreeOrganization",
            success: function (data) {
                $("#OrganizationName").ztreeSelect(OrganizationSetting, data);
                LoadOrganizatioData();
            },
            error: function (result) {
                alert("加载部门树错误："+result.status);
            }
        });
    }

    function RolesBind() {
        var ctl = $("#RoleID");
        ctl.select2({
            theme: "bootstrap",
            language: "zh-CN", //设置 提示语言
            width: "100%", //设置下拉框的宽度
            placeholder: "请选择角色",
            tags: true,
            separator: ",",
            multiple: true
        });

    }
  
    return {
        init: function () {
            OrganizationNameBind();
            RolesBind();

        }
    };
}();

jQuery(document).ready(function () {
    userModify.init();
});