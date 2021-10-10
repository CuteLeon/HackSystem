let resizeEvents = {
	resizeData: null,
	resizeTarget: null,
	mouseResizeMoveMethod: null,
	touchResizeMoveMethod: null,

	mouseResizeStart: function (e) {
		let $event = $.event.fix(e);
		if ($event.button === 0) {
			let $target = $($event.currentTarget);
			resizeTarget = $target.parents($target.data('resizetarget'))[0];
			if (resizeTarget === undefined || resizeTarget === null) return;

			let currentOffSet = $(resizeTarget).offset();
			resizeData = {
				direction: $($event.currentTarget).css('cursor'),
				left: currentOffSet.left,
				top: currentOffSet.top,
				right: currentOffSet.left + $(resizeTarget).outerWidth(),
				bottom: currentOffSet.top + $(resizeTarget).outerHeight(),
			};
			switch (resizeData.direction) {
				case 'nw-resize':
					{
						resizeData.diffX = $event.clientX - resizeData.left;
						resizeData.diffY = $event.clientY - resizeData.top;
						mouseResizeMoveMethod = resizeEvents.mouse_NW_Resize;
						break;
					}
				case 'n-resize':
					{
						resizeData.diffY = $event.clientY - resizeData.top;
						mouseResizeMoveMethod = resizeEvents.mouse_N_Resize;
						break;
					}
				case 'ne-resize':
					{
						resizeData.diffX = resizeData.right - $event.clientX;
						resizeData.diffY = $event.clientY - resizeData.top;
						mouseResizeMoveMethod = resizeEvents.mouse_NE_Resize;
						break;
					}
				case 'w-resize':
					{
						resizeData.diffX = $event.clientX - resizeData.left;
						mouseResizeMoveMethod = resizeEvents.mouse_W_Resize;
						break;
					}
				case 'e-resize':
					{
						resizeData.diffX = resizeData.right - $event.clientX;
						mouseResizeMoveMethod = resizeEvents.mouse_E_Resize;
						break;
					}
				case 'sw-resize':
					{
						resizeData.diffX = $event.clientX - resizeData.left;
						resizeData.diffY = resizeData.bottom - $event.clientY;
						mouseResizeMoveMethod = resizeEvents.mouse_SW_Resize;
						break;
					}
				case 's-resize':
					{
						resizeData.diffY = resizeData.bottom - $event.clientY;
						mouseResizeMoveMethod = resizeEvents.mouse_S_Resize;
						break;
					}
				case 'se-resize':
					{
						resizeData.diffX = resizeData.right - $event.clientX;
						resizeData.diffY = resizeData.bottom - $event.clientY;
						mouseResizeMoveMethod = resizeEvents.mouse_SE_Resize;
						break;
					}
				default: return;
			}

			$(document).on('mousemove', mouseResizeMoveMethod);
			$(document).one('mouseup', resizeEvents.mouseResizeEnd);
		}
		else {
			resizeData = null;
			resizeTarget = null;
			mouseResizeMoveMethod = null;
			touchResizeMoveMethod = null;
		}
	},
	mouse_N_Resize: function (e) {
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			let top = e.clientY - resizeData.diffY;
			if (0 <= e.clientY && e.clientY <= resizeData.bottom)
				$(resizeTarget).css('top', top);
			$(resizeTarget).css('height', resizeData.bottom - top);
		}
	},
	mouse_W_Resize: function (e) {
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			let left = e.clientX - resizeData.diffX;
			if (0 <= e.clientX && e.clientX <= resizeData.right)
				$(resizeTarget).css('left', left);
			$(resizeTarget).css('width', resizeData.right - left);
		}
	},
	mouse_E_Resize: function (e) {
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			if (resizeData.left <= e.clientX && e.clientX <= document.documentElement.clientWidth)
				$(resizeTarget).css('width', e.clientX - resizeData.left + resizeData.diffX);
		}
	},
	mouse_S_Resize: function (e) {
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			if (resizeData.top <= e.clientY && e.clientY <= document.documentElement.clientHeight)
				$(resizeTarget).css('height', e.clientY - resizeData.top + resizeData.diffY);
		}
	},
	mouse_NW_Resize: function (e) {
		resizeEvents.mouse_N_Resize(e);
		resizeEvents.mouse_W_Resize(e);
	},
	mouse_NE_Resize: function (e) {
		resizeEvents.mouse_N_Resize(e);
		resizeEvents.mouse_E_Resize(e);
	},
	mouse_SW_Resize: function (e) {
		resizeEvents.mouse_S_Resize(e);
		resizeEvents.mouse_W_Resize(e);
	},
	mouse_SE_Resize: function (e) {
		resizeEvents.mouse_S_Resize(e);
		resizeEvents.mouse_E_Resize(e);
	},

	touchResizeStart: function (e) {
		let $event = $.event.fix(e);
		let $target = $($event.currentTarget);
		resizeTarget = $target.parents($target.data('resizetarget'))[0];
		if (resizeTarget === undefined || resizeTarget === null) return;

		let currentOffSet = $(resizeTarget).offset();
		resizeData = {
			direction: $($event.currentTarget).css('cursor'),
			left: currentOffSet.left,
			top: currentOffSet.top,
			right: currentOffSet.left + $(resizeTarget).outerWidth(),
			bottom: currentOffSet.top + $(resizeTarget).outerHeight(),
		};

		let clientX = $event.touches[0].clientX;
		let clientY = $event.touches[0].clientY;
		switch (resizeData.direction) {
			case 'nw-resize':
				{
					resizeData.diffX = clientX - resizeData.left;
					resizeData.diffY = clientY - resizeData.top;
					touchResizeMoveMethod = resizeEvents.touch_NW_Resize;
					break;
				}
			case 'n-resize':
				{
					resizeData.diffY = clientY - resizeData.top;
					touchResizeMoveMethod = resizeEvents.touch_N_Resize;
					break;
				}
			case 'ne-resize':
				{
					resizeData.diffX = resizeData.right - clientX;
					resizeData.diffY = clientY - resizeData.top;
					touchResizeMoveMethod = resizeEvents.touch_NE_Resize;
					break;
				}
			case 'w-resize':
				{
					resizeData.diffX = clientX - resizeData.left;
					touchResizeMoveMethod = resizeEvents.touch_W_Resize;
					break;
				}
			case 'e-resize':
				{
					resizeData.diffX = resizeData.right - clientX;
					touchResizeMoveMethod = resizeEvents.touch_E_Resize;
					break;
				}
			case 'sw-resize':
				{
					resizeData.diffX = clientX - resizeData.left;
					resizeData.diffY = resizeData.bottom - clientY;
					touchResizeMoveMethod = resizeEvents.touch_SW_Resize;
					break;
				}
			case 's-resize':
				{
					resizeData.diffY = resizeData.bottom - clientY;
					touchResizeMoveMethod = resizeEvents.touch_S_Resize;
					break;
				}
			case 'se-resize':
				{
					resizeData.diffX = resizeData.right - clientX;
					resizeData.diffY = resizeData.bottom - clientY;
					touchResizeMoveMethod = resizeEvents.touch_SE_Resize;
					break;
				}
			default: return;
		}

		document.addEventListener('touchmove', touchResizeMoveMethod);
		document.addEventListener('touchend', resizeEvents.touchResizeEnd);
	},
	touch_N_Resize: function (e) {
		let $event = $.event.fix(e);
		let clientY = $event.touches[0].clientY;
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			let top = clientY - resizeData.diffY;
			if (0 <= clientY && clientY <= resizeData.bottom)
				$(resizeTarget).css('top', top);
			$(resizeTarget).css('height', resizeData.bottom - top);
		}
	},
	touch_W_Resize: function (e) {
		let $event = $.event.fix(e);
		let clientX = $event.touches[0].clientX;
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			let left = clientX - resizeData.diffX;
			if (0 <= clientX && clientX <= resizeData.right)
				$(resizeTarget).css('left', left);
			$(resizeTarget).css('width', resizeData.right - left);
		}
	},
	touch_E_Resize: function (e) {
		let $event = $.event.fix(e);
		let clientX = $event.touches[0].clientX;
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			if (resizeData.left <= clientX && clientX <= document.documentElement.clientWidth)
				$(resizeTarget).css('width', clientX - resizeData.left + resizeData.diffX);
		}
	},
	touch_S_Resize: function (e) {
		let $event = $.event.fix(e);
		let clientY = $event.touches[0].clientY;
		if (resizeTarget !== undefined &&
			resizeTarget !== null) {
			if (resizeData.top <= clientY && clientY <= document.documentElement.clientHeight)
				$(resizeTarget).css('height', clientY - resizeData.top + resizeData.diffY);
		}
	},
	touch_NW_Resize: function (e) {
		resizeEvents.touch_N_Resize(e);
		resizeEvents.touch_W_Resize(e);
	},
	touch_NE_Resize: function (e) {
		resizeEvents.touch_N_Resize(e);
		resizeEvents.touch_E_Resize(e);
	},
	touch_SW_Resize: function (e) {
		resizeEvents.touch_S_Resize(e);
		resizeEvents.touch_W_Resize(e);
	},
	touch_SE_Resize: function (e) {
		resizeEvents.touch_S_Resize(e);
		resizeEvents.touch_E_Resize(e);
	},

	mouseResizeEnd: function (e) {
		$(document).off('mousemove', mouseResizeMoveMethod);
		resizeEvents.resizeEnd(e);
	},
	touchResizeEnd: function (e) {
		document.removeEventListener('touchmove', touchResizeMoveMethod);
		document.removeEventListener('touchend', resizeEvents.touchResizeEnd);
		resizeEvents.resizeEnd(e);
	},
	resizeEnd: function (e) {
		resizeData = null;
		resizeTarget = null;
		mouseResizeMoveMethod = null;
		touchResizeMoveMethod = null;
	}
};