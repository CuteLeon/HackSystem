var dragEvents = {
    dragData: null,
    dragTarget: null,
    mouseDragStart: function (e) {
        let $event = $.event.fix(e);
        if ($event.button === 0) {
            let $target = $($event.currentTarget);
            dragTarget = $target.parents($target.data('dragtarget'))[0];
            if (dragTarget === undefined || dragTarget === null) return;

            let currentPosition = { X: $(dragTarget).offset().left, Y: $(dragTarget).offset().top };
            dragData = {
                diffX: $event.clientX - currentPosition.X,
                diffY: $event.clientY - currentPosition.Y,
            };

            $(document).on('mousemove', dragEvents.mouseDragMove);
            $(document).one('mouseup', dragEvents.dragEnd);
        }
        else {
            dragData = null;
            dragTarget = null;
        }
    },
    touchDragStart: function (e) {
        let $event = $.event.fix(e);
        let $target = $($event.currentTarget);
        dragTarget = $target.parents($target.data('dragtarget'))[0];
        if (dragTarget === undefined || dragTarget === null) return;

        let clientX = $event.touches[0].clientX;
        let clientY = $event.touches[0].clientY;
        let currentPosition = { X: $(dragTarget).offset().left, Y: $(dragTarget).offset().top };
        dragData = {
            diffX: clientX - currentPosition.X,
            diffY: clientY - currentPosition.Y,
        };

        document.addEventListener('touchmove', dragEvents.touchDragMove);
        document.addEventListener('touchend', dragEvents.dragEnd);
    },
    mouseDragMove: function (e) {
        if (e.button === 0) {
            if (dragTarget !== undefined &&
                dragTarget !== null) {
                if (0 <= e.clientX && e.clientX <= document.documentElement.clientWidth)
                    $(dragTarget).css('left', e.clientX - dragData.diffX);
                if (0 <= e.clientY && e.clientY <= document.documentElement.clientHeight)
                    $(dragTarget).css('top', e.clientY - dragData.diffY);
            }
        }
    },
    touchDragMove: function (e) {
        let $event = $.event.fix(e);
        if (dragTarget !== undefined &&
            dragTarget !== null) {
            let clientX = $event.touches[0].clientX;
            let clientY = $event.touches[0].clientY;
            if (0 <= clientX && clientX <= document.documentElement.clientWidth)
                $(dragTarget).css('left', clientX - dragData.diffX);
            if (0 <= clientY && clientY <= document.documentElement.clientHeight)
                $(dragTarget).css('top', clientY - dragData.diffY);
        }
    },
    dragEnd: function (e) {
        dragData = null;
        dragTarget = null;
        $(document).off('mousemove', dragEvents.mouseDragMove);
        document.removeEventListener('touchmove', dragEvents.touchDragMove);
    }
};