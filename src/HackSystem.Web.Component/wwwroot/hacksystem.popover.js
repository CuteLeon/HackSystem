export function setupPopovers(filter) {
    $(filter).popover();
};

export function setupPopover(popoverTargetId, title, html, content, trigger, placement, offset, showDelay, hideDelay, replaceContent = false) {
    let popoverTarget = $(`#${popoverTargetId}`);
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
    let replacementId = null;
    if (html) {
        options.html = true;
        if (replaceContent) {
            replacementId = `popoverPlacement_${$(`#${content}`).attr('id')}`;
            options.content = function () {
                return $("<div></div>").append($("<div></div>", { id: replacementId })).html();
            };
            popoverTarget.on('inserted.bs.popover', e => {
                refreshContent(popoverTargetId, replacementId, content);
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
            if ($(".popover:hover").length) {
                $(document).one('mouseleave', '.popover', function () {
                    popoverTarget.popover('hide');
                });
                return false;
            }
        });
    }
    return replacementId;
};

export function updatePopover(filter, action) {
    $(filter).popover(action);
};

export function refreshContent(popoverId, replacementId, originId) {
    if (!replacementId || !originId) return;
    let replacementTarget = $(`#${replacementId}`), originSource = $(`#${originId}`);
    if (!replacementTarget || !originSource) return;
    var cloneSource = originSource.children().clone(true);
    replacementTarget.empty().append(cloneSource);
    $(`#${popoverId}`).popover('update');
};