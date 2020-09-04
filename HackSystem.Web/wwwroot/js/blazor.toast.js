window.toasts = {
    popToast: function (id, autohide = true, delay = 3000) {
        $(`#${id}`).toast({
            animation: true,
            autohide: autohide,
            delay: delay
        });
    }
};