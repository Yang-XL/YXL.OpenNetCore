var menus = function () {

    var NavItem_AddClass = function (li) {
        if (!$(li).hasClass("active")) {
            $(li).addClass("active");
        }
        if (!$(li).hasClass("open")) {
            $(li).addClass("open");
        }

        if ($(li).children("a.nav-link").children("span.selected").length === 0) {
            $(li).children("a.nav-link").append("<span class=\"selected\"></span>");
        }
        if ($(li).children("a.nav-link").children("span.arrow") === 0) {
            $(li).children("a.nav-link").append("<span class=\"arrow open\"></span>");
        } else {
            $(li).children("a.nav-link").children("span.arrow").addClass("open");
        }
    }
    /*
    * 链接菜单点击清除其他的CSS样式
    */
    var LinkMenu_OnClick_ClearClass = function () {
        $("ul.page-sidebar-menu").find("li.nav-item").each(function () {
            $(this).removeClass("active").removeClass("open");
            $(this).children("a.nav-link").children("span.arrow").removeClass("open");
            $(this).children("a.nav-link").children("span.selected").remove();
        });
    }
    /*
   * 当前链接添加选中样式
   */
    var LinkMenu_OnClick_AddClass = function (li) {
        NavItem_AddClass(li);
        $(li).parents("li.nav-item").each(function() {
            NavItem_AddClass(this);
        });
    };

    

    return {
        init: function () {
            $("ul.page-sidebar-menu").find("li.nav-item").each(function () {
                if ($(this).hasClass("active") && $(this).hasClass("open")) {
                    LinkMenu_OnClick_ClearClass();
                    LinkMenu_OnClick_AddClass(this);
                }
            });
        }
    }
}();
jQuery(document).ready(function () {
    menus.init();
});