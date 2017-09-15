$.fn.extend({
    ztreeSelect:
    function (setting, zNodes) {

        var input_id = $(this).attr("id");
        var zTree_Content_id = input_id + "_ZTreeContent";
        var zTree_id = input_id + "_ZTree";
        $(this).attr("treeid", zTree_id)

        function hideParentMenu() {
            $("#" + zTree_Content_id).fadeOut("fast");
            $("body").unbind("mousedown", onBodyDown);
        }
        function showParentMenu() {
            $("#" + zTree_Content_id).slideDown("fast");
            $("body").bind("mousedown", onBodyDown);
        }

        function onBodyDown(event) {
            if (!(event.target.id === zTree_Content_id || $(event.target).parents("#" + zTree_Content_id).length > 0)) {
                hideParentMenu();
            }
        }

        $(this).off("hideTree").on("hideTree", hideParentMenu)
        $(this).off("showTree").on("showTree", showParentMenu)
        
        $(this).off("click").on("click",function () {
            var tb = $(this);
            var tbOffset = $(this).offset();
            $("#" + zTree_Content_id).each(function () {
                this.style.left = tbOffset.left;
                this.style.width = tb[0].offsetWidth + "px";
                this.style.top = tbOffset.top + tb.height() + 5;
            });
            if ($("#" + zTree_Content_id).is(":hidden")) {
                showParentMenu();
            } else {
                hideParentMenu();
            }
        });
        $.fn.zTree.init($("#" + zTree_id), setting, zNodes);
    }
});




