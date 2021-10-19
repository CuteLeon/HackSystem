var dockEvents = {
    mouseOverIcon: function (e) {
        $(e).prev().animate({
            width: '100px',
            height: '100px',
            margin: '-25px 0 0 0',
        }, 150);
    },
    mouseOutIcon: function (e) {
        $(e).prev().animate({
            width: '75px',
            height: '75px',
            margin: '0px 0 0 0',
        }, 150);
    },
    mouseDownIcon: function (e) {
        $(e).prev().animate({
            opacity: 1,
            margin: '-50px 0 0 0',
        }, 150);
    },
    mouseUpIcon: function (e) {
        $(e).prev().animate({
            opacity: 1,
            margin: '-25px 0 0 0',
        }, 150);
    }
};