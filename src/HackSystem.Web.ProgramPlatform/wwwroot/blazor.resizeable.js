let resizeData = null;
let resizeTarget = null;
let mouseResizeMoveMethod = null;
let touchResizeMoveMethod = null;
let interopReference = null;
let isMouseMode = null;

export function resizeStart(interop, isMouse, startX, startY, resizeTargetId, cursor) {
    interopReference = interop;
    isMouseMode = isMouse;
    resizeTarget = $(`#${resizeTargetId}`);
    if (resizeTarget === undefined || resizeTarget === null) return;

    let currentOffSet = resizeTarget.offset();
    resizeData = {
        direction: cursor,
        left: currentOffSet.left,
        top: currentOffSet.top,
        right: currentOffSet.left + resizeTarget.outerWidth(),
        bottom: currentOffSet.top + resizeTarget.outerHeight(),
    };

    if (interopReference != null) {
        let left = currentOffSet.left;
        let top = currentOffSet.top;
        let width = resizeTarget.outerWidth();
        let height = resizeTarget.outerHeight();
        interopReference.invokeMethodAsync('UpdateSize', left, top, width, height);
    }

    switch (resizeData.direction) {
        case 'nw-resize':
            {
                resizeData.diffX = startX - resizeData.left;
                resizeData.diffY = startY - resizeData.top;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_NW_Resize;
                else
                    touchResizeMoveMethod = touch_NW_Resize;
                break;
            }
        case 'n-resize':
            {
                resizeData.diffY = startY - resizeData.top;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_N_Resize;
                else
                    touchResizeMoveMethod = touch_N_Resize;
                break;
            }
        case 'ne-resize':
            {
                resizeData.diffX = resizeData.right - startX;
                resizeData.diffY = startY - resizeData.top;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_NE_Resize;
                else
                    touchResizeMoveMethod = touch_NE_Resize;
                break;
            }
        case 'w-resize':
            {
                resizeData.diffX = startX - resizeData.left;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_W_Resize;
                else
                    touchResizeMoveMethod = touch_W_Resize;
                break;
            }
        case 'e-resize':
            {
                resizeData.diffX = resizeData.right - startX;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_E_Resize;
                else
                    touchResizeMoveMethod = touch_E_Resize;
                break;
            }
        case 'sw-resize':
            {
                resizeData.diffX = startX - resizeData.left;
                resizeData.diffY = resizeData.bottom - startY;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_SW_Resize;
                else
                    touchResizeMoveMethod = touch_SW_Resize;
                break;
            }
        case 's-resize':
            {
                resizeData.diffY = resizeData.bottom - startY;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_S_Resize;
                else
                    touchResizeMoveMethod = touch_S_Resize;
                break;
            }
        case 'se-resize':
            {
                resizeData.diffX = resizeData.right - startX;
                resizeData.diffY = resizeData.bottom - startY;
                if (isMouseMode)
                    mouseResizeMoveMethod = mouse_SE_Resize;
                else
                    touchResizeMoveMethod = touch_SE_Resize;
                break;
            }
        default: return;
    }

    if (isMouseMode) {
        $(document).on('mousemove', mouseResizeMoveMethod);
        $(document).one('mouseup', resizeEnd);
    } else {
        document.addEventListener('touchmove', touchResizeMoveMethod);
        document.addEventListener('touchend', resizeEnd);
    }
};

export function mouse_N_Resize(e) {
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        let top = e.clientY - resizeData.diffY;
        if (0 <= e.clientY && e.clientY <= resizeData.bottom)
            $(resizeTarget).css('top', top);
        $(resizeTarget).css('height', resizeData.bottom - top);
    }
};
export function mouse_W_Resize(e) {
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        let left = e.clientX - resizeData.diffX;
        if (0 <= e.clientX && e.clientX <= resizeData.right)
            $(resizeTarget).css('left', left);
        $(resizeTarget).css('width', resizeData.right - left);
    }
};
export function mouse_E_Resize(e) {
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        if (resizeData.left <= e.clientX && e.clientX <= document.documentElement.clientWidth)
            $(resizeTarget).css('width', e.clientX - resizeData.left + resizeData.diffX);
    }
};
export function mouse_S_Resize(e) {
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        if (resizeData.top <= e.clientY && e.clientY <= document.documentElement.clientHeight)
            $(resizeTarget).css('height', e.clientY - resizeData.top + resizeData.diffY);
    }
};
export function mouse_NW_Resize(e) {
    mouse_N_Resize(e);
    mouse_W_Resize(e);
};
export function mouse_NE_Resize(e) {
    mouse_N_Resize(e);
    mouse_E_Resize(e);
};
export function mouse_SW_Resize(e) {
    mouse_S_Resize(e);
    mouse_W_Resize(e);
};
export function mouse_SE_Resize(e) {
    mouse_S_Resize(e);
    mouse_E_Resize(e);
};
export function touch_N_Resize(e) {
    let $event = $.event.fix(e);
    let clientY = $event.touches[0].clientY;
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        let top = clientY - resizeData.diffY;
        if (0 <= clientY && clientY <= resizeData.bottom)
            $(resizeTarget).css('top', top);
        $(resizeTarget).css('height', resizeData.bottom - top);
    }
};
export function touch_W_Resize(e) {
    let $event = $.event.fix(e);
    let clientX = $event.touches[0].clientX;
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        let left = clientX - resizeData.diffX;
        if (0 <= clientX && clientX <= resizeData.right)
            $(resizeTarget).css('left', left);
        $(resizeTarget).css('width', resizeData.right - left);
    }
};
export function touch_E_Resize(e) {
    let $event = $.event.fix(e);
    let clientX = $event.touches[0].clientX;
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        if (resizeData.left <= clientX && clientX <= document.documentElement.clientWidth)
            $(resizeTarget).css('width', clientX - resizeData.left + resizeData.diffX);
    }
};
export function touch_S_Resize(e) {
    let $event = $.event.fix(e);
    let clientY = $event.touches[0].clientY;
    if (resizeTarget !== undefined &&
        resizeTarget !== null) {
        if (resizeData.top <= clientY && clientY <= document.documentElement.clientHeight)
            $(resizeTarget).css('height', clientY - resizeData.top + resizeData.diffY);
    }
};
export function touch_NW_Resize(e) {
    touch_N_Resize(e);
    touch_W_Resize(e);
};
export function touch_NE_Resize(e) {
    touch_N_Resize(e);
    touch_E_Resize(e);
};
export function touch_SW_Resize(e) {
    touch_S_Resize(e);
    touch_W_Resize(e);
};
export function touch_SE_Resize(e) {
    touch_S_Resize(e);
    touch_E_Resize(e);
};

export function resizeEnd(e) {
    if (isMouseMode) {
        $(document).off('mousemove', mouseResizeMoveMethod);
    } else {
        document.removeEventListener('touchmove', touchResizeMoveMethod);
        document.removeEventListener('touchend', resizeEnd);
    }

    if (interopReference != null) {
        let left = resizeTarget.offset().left;
        let top = resizeTarget.offset().top;
        let width = resizeTarget.outerWidth();
        let height = resizeTarget.outerHeight();
        interopReference.invokeMethodAsync('UpdateSize', left, top, width, height);
    }

    resizeData = null;
    resizeTarget = null;
    mouseResizeMoveMethod = null;
    touchResizeMoveMethod = null;
    interopReference = null;
    isMouseMode = null;
};