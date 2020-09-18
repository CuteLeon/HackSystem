window.tooltips = {
    initTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip();
    },
    hideTooltips: function () {
        $('[data-toggle="tooltip"]').tooltip('hide');
    }
};