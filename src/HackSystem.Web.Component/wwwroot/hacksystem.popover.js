export let popovers = {
    setupPopover: function (filter) {
        $(filter).popover();
    },
    setupPopover: function (filter, title, html, content, trigger, placement, offset, showDelay, hideDelay) {
        let popovers = $(filter);
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
            options.content = function () {
                let htmlContent = $(content).html();
                return htmlContent;
            };
        }
        else {
            Option.content = content;
        }
        let res = popovers.popover(options);
        if (trigger === 'hover') {
            res.on('hide.bs.popover', function () {
                if ($(".popover:hover").length) {
                    $(document).one('mouseleave', '.popover', function () {
                        popovers.popover('hide');
                    });
                    return false;
                }
            });
        }
    },
    updatePopover: function (filter, action) {
        $(filter).popover(action);
    }
};