window.toasts = {
    popToast: function (id, autohide = true, delay = 3000) {
        let toast = $(`#${id}`);
        toast.toast({
            animation: true,
            autohide: autohide,
            delay: delay
        });
        toast.toast('show');
    }
};