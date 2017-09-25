$.fn.extend({
    ztreeSelect:
    function (setting, zNodes) {

        var input_id = $(this).attr("id");
        var zTree_Content_id = input_id + "_ZTreeContent";
        var zTree_id = input_id + "_ZTree";
        $(this).attr("treeid", zTree_id)

        function hideContent() {
            $("#" + zTree_Content_id).fadeOut("fast");
            $("body").unbind("mousedown", onBodyDown);
        }
        function showContent() {
            $("#" + zTree_Content_id).slideDown("fast");
            $("body").bind("mousedown", onBodyDown);
        }

        function onBodyDown(event) {
            if (!(event.target.id === zTree_Content_id || $(event.target).parents("#" + zTree_Content_id).length > 0)) {
                hideContent();
            }
        }

        $(this).off("hideTree").on("hideTree", hideContent)
        $(this).off("showTree").on("showTree", showContent)
        
        $(this).off("click").on("click",function () {
            var tb = $(this);
            var tbOffset = $(this).offset();
            $("#" + zTree_Content_id).each(function () {
                this.style.left = tbOffset.left;
                this.style.width = tb[0].offsetWidth + "px";
                this.style.top = tbOffset.top + tb.height() + 5;
            });
            if ($("#" + zTree_Content_id).is(":hidden")) {
                showContent();
            } else {
                hideContent();
            }
        });
        $.fn.zTree.init($("#" + zTree_id), setting, zNodes);
    }
});




