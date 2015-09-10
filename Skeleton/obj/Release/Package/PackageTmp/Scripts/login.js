var login = function () {

    var init = function () {

        var error = $('.alert-danger');
        var success = $('.alert-success');
        var form = $('.login-form');
        form.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input

            rules: {
                id: {
                    digits: true,
                    rangelength: [8, 10],
                    required: true
                },
                password: {
                    required: true
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit              
                success.hide();
                error.show();

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element)
                    .closest('.form-group').removeClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label
                    .closest('.form-group').removeClass('has-error'); // set success class to the control group
            },

            submitHandler: function (form) {
                success.show();
                error.hide();
                spinner.spin();
                form.appendChild(spinner.el);
                $.ajax({
                    url: "/Login/Login",
                    data: $(form).serialize(),
                    type: "POST",

                    success: function (result) {
                        spinner.stop();
                        if (result.Success) {
                            error.hide();
                            success.text(result.Message).show();
                            setTimeout( function () {
                                window.location.href = "/" + result.Data ;
                            },3000);
                        }
                        else {
                            spinner.stop();

                            success.hide();
                            error.text(result.Message).show();
                        }
                    },
                    error: function (err) {

                    }
                });

            }


        });
    }

    return { init: init };
}();

login.init();