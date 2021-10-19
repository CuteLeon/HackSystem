let dragData = null;
let dragTarget = null;
let interopReference = null;
let isMouseMode = null;

export function dragStart(interop, isMouse, startX, startY, dragTargetId) {
    interopReference = interop;
    isMouseMode = isMouse;
    dragTarget = $(`#${dragTargetId}`);
    if (dragTarget === undefined || dragTarget === null) return;

    let currentPosition = { X: dragTarget.offset().left, Y: dragTarget.offset().top };
    dragData = {
        diffX: startX - currentPosition.X,
        diffY: startY - currentPosition.Y,
    };

    if (interopReference != null) {
        let left = startX - dragData.diffX;
        let top = startY - dragData.diffY;
        interopReference.invokeMethodAsync('UpdatePosition', left, top);
    }

    if (isMouseMode) {
        $(document).on('mousemove', mouseDragMove);
        $(document).one('mouseup', dragEnd);
    } else {
        document.addEventListener('touchmove', touchDragMove);
        document.addEventListener('touchend', dragEnd);
    }
};

export function mouseDragMove(e) {
    if (e.button === 0) {
        if (dragTarget !== undefined &&
            dragTarget !== null) {
            if (0 <= e.clientX && e.clientX <= document.documentElement.clientWidth)
                $(dragTarget).css('left', e.clientX - dragData.diffX);
            if (0 <= e.clientY && e.clientY <= document.documentElement.clientHeight)
                $(dragTarget).css('top', e.clientY - dragData.diffY);
        }
    }
};

export function touchDragMove(e) {
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
};

export function dragEnd(e) {
    if (interopReference != null) {
        let left = 0, top = 0;
        if (isMouseMode) {
            left = e.clientX - dragData.diffX;
            top = e.clientY - dragData.diffY;
        } else {
            left = e.changedTouches[0].clientX - dragData.diffX;
            top = e.changedTouches[0].clientY - dragData.diffY;
        }
        interopReference.invokeMethodAsync('UpdatePosition', left, top);
    }

    dragData = null;
    dragTarget = null;
    interopReference = null;
    isMouseMode = null;
    $(document).off('mousemove', mouseDragMove);
    document.removeEventListener('touchmove', touchDragMove);
    document.removeEventListener('touchend', dragEnd);
};