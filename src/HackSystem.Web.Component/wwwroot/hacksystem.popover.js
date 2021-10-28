export function setupPopovers(filter) {
    $(filter).popover();
};

export function setupPopover(popoverId, title, html, content, trigger, placement, offset,
    showDelay, hideDelay, contentSourceId, headerSourceId) {
    let popoverTarget = $(`#${popoverId}`);
    let options = {
        title: title,
        trigger: trigger,
        placement: placement,
        offset: offset,
        delay: {
            show: showDelay,
            hide: hideDelay
        },
    };
    if (html) {
        options.html = true;
        if (contentSourceId) {
            options.content = function () {
                return $("<div></div>").append($("<div></div>", { id: getContentRepleacementId(contentSourceId) })).html();
            };
            popoverTarget.on('inserted.bs.popover', e => {
                refreshReplacement(popoverId, contentSourceId, headerSourceId);
            });
        } else {
            options.content = function () { return $(`#${content}`).html(); };
        }
    }
    else {
        Option.content = content;
    }
    let res = popoverTarget.popover(options);
    if (trigger === 'hover') {
        res.on('hide.bs.popover', function () {
            let hoverPopovers = $(".popover:hover");
            if (hoverPopovers.length) {
                hoverPopovers.one('mouseleave', function () {
                    hoverPopovers.popover('hide');
                });
                return false;
            }
        });
    }
};

function getContentRepleacementId(sourceId) {
    return getRepleacementId(sourceId, "Content");
};

function getRepleacementId(sourceId, prefix) {
    return `popover${prefix}Placement_${sourceId}`;
};

export function updatePopover(filter, action) {
    $(filter).popover(action);
};

export function refreshReplacement(popoverId, contentSourceId, headerSourceId) {
    if (!contentSourceId) return;

    let contentSourceElement = $(`#${contentSourceId}`);
    let contentReplacementElement = $(`#${getContentRepleacementId(contentSourceId)}`);
    let cloneContentSourceElement = contentSourceElement.children().clone(true);
    contentReplacementElement.empty().append(cloneContentSourceElement);

    if (headerSourceId) {
        let headerSourceElement = $(`#${headerSourceId}`);
        var cloneHeaderSourceElement = headerSourceElement.children().clone(true);
        contentReplacementElement.parents(".popover").children(".popover-header").replaceWith(cloneHeaderSourceElement);
    }

    if (popoverId) {
        $(`#${popoverId}`).popover('update');
    }
};
