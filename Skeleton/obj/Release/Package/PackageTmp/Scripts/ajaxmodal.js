


var ajaxModal = function (modalContainer, link, data, method, callBack, closeEvents) {

   
	$.ajax({
	    url: link,
	    data: data,
	    type: method,
	    success: function (html) {
	        modalContainer.empty();
	        modalContainer.html(html);
	        modalContainer.modal();

	        if ($.isFunction(callBack)) {
	            callBack();
	        }
	    },
	    error: function (ex) {
	        ///fullback for error handling  --  close loader on erorr
	        console.log(ex);
	    }
	});
}

var showConfirmBox = function (message, buttons, className, callback) {
    bootbox.confirm({
        buttons: buttons,
        message: message,
        className: className,
        callback: callback
    });
}



