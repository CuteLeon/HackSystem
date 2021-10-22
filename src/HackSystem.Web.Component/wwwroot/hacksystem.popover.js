export let popovers = {
    setupPopover: function (filter) {
        $(filter).popover();
    },
    setupPopover: function (popoverTargetFilter, title, html, content, trigger, placement, offset, showDelay, hideDelay, replaceContent = false) {
        let popoverTarget = $(popoverTargetFilter);
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
            options.content = function () {
                if (!replaceContent) return $(content).html();
                else {
                    replacementId = `popoverPlacement_${Math.round(performance.now() * 1000000000000)}_${Math.round(Math.random() * 100000000000000000)}`
                    return $("<div></div>").append($("<div></div>", { id: replacementId })).html();
                }
            };
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
        if (html && replaceContent) {
            popoverTarget.on('inserted.bs.popover', e => {
                var replacement = $(`#${replacementId}`);
                var origin = $(content).children().clone(true);
                replacement.replaceWith(origin);
            });
        }
    },
    updatePopover: function (filter, action) {
        $(filter).popover(action);
    }
};