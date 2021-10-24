var programDock = {
    programDockReference: null,
    initialProgramDock: function (reference) {
        programDockReference = reference;
    },
    windowClick: function (nativeTarget) {
        let windowRef = programDock.getWindowReferenceId(nativeTarget);
        programDockReference.invokeMethodAsync('OnWindowClick', windowRef.programId, windowRef.processId, windowRef.windowId);
    },
    windowStickyTop: function (nativeTarget) {
        let windowRef = programDock.getWindowReferenceId(nativeTarget);
        programDockReference.invokeMethodAsync('OnWindowStickyTop', windowRef.programId, windowRef.processId, windowRef.windowId);
    },
    windowClose: function (nativeTarget) {
        let windowRef = programDock.getWindowReferenceId(nativeTarget);
        programDockReference.invokeMethodAsync('OnWindowClose', windowRef.programId, windowRef.processId, windowRef.windowId);
    },
    getWindowReferenceId: function (nativeTarget) {
        let target = $(nativeTarget).parent(".btn-group");
        return {
            programId: target.data("programid"),
            processId: target.data("processid"),
            windowId: target.data("windowid"),
        };
    }
};
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