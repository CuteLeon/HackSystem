var drawerEvents = {
    mouseOverIcon: function (e) {
        let icon = $(e).parent().parent();
        let focused = icon.is(':focus');
        if (focused) return;

        icon.addClass('shadow');
        icon.css('background', 'rgba(255, 255, 255, 0.2)');
        icon.css('backdrop-filter', 'blur(5px)');
    },
    mouseOutIcon: function (e) {
        let icon = $(e).parent().parent();
        let focused = icon.is(':focus');
        if (focused) return;

        icon.removeClass('shadow');
        icon.css('background', 'rgba(255, 255, 255, 0)');
        icon.css('backdrop-filter', '');
    },
    mouseDownIcon: function (e) {
        let icon = $(e).parent().parent();
        icon.css('background', 'rgba(255, 255, 255, 0.5)');
    },
    mouseUpIcon: function (e) {
        let icon = $(e).parent().parent();
        icon.css('background', 'rgba(255, 255, 255, 0.2)');
    },
    focusIcon: function (e) {
        let icon = $(e);
        icon.css('background', 'rgba(255, 255, 255, 0.2)');
        icon.css('backdrop-filter', 'blur(5px)');
    },
    blurIcon: function (e) {
        let icon = $(e);
        icon.css('background', 'rgba(255, 255, 255, 0)');
        icon.css('backdrop-filter', '');
    }
};