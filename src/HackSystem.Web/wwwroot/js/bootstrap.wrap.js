window.tooltips = {
    initTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip();
    },
    hideTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip('hide');
    },
};

window.popover = {
    initPopover: function (popoverFilter) {
        $(popoverFilter).popover();
    },
};

window.submenus = {
    initSubMenus: function () {
        $('[data-submenu]').submenupicker();
    }
};