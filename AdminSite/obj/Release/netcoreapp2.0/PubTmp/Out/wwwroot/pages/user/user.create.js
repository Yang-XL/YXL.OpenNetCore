var userCreate = function () {

    

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
        $("#OrganizationID").val("");
        $("#OrganizationName").val("");
        $.ajax({
            type: "Post",
            datatype: "Json",
            url: "/Organization/ZtreeOrganization",
            success: function (data) {
                $("#OrganizationName").ztreeSelect(OrganizationSetting, data);
            },
            error: function (result) {
                alert("加载部门树错误："+result.status);
            }
        });
    }

    function RolesBind() {
        $("#RoleID").select2({
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
    userCreate.init();
});