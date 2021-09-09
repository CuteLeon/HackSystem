window.tooltips = {
    initTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip();
    },
    hideTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip('hide');
    },
};

window.submenus = {
    initSubMenus: function () {
        $('[data-submenu]').submenupicker();
    }
};