var videoForm = function () {

    var spinner = new Spinner();
    var init = function () {

        dropzoneHandler();


        formValidation();

    }

    var dropzoneHandler = function () {
        //initilize dropzone
       
        var dropzone = new Dropzone('.dropzone', {
            previewTemplate: document.querySelector('#preview-template').innerHTML,
          
            autoProcessQueue: false,
            addRemoveLinks: true,
            parallelUploads: 1,
            thumbnailHeight: 120,
            thumbnailWidth: 120,
            maxFilesize: 3,
            filesizeBase: 1000,
            thumbnail: function (file, dataUrl) {
                if (file.previewElement) {
                    file.previewElement.classList.remove("dz-file-preview");
                    var images = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
                    for (var i = 0; i < images.length; i++) {
                        var thumbnailElement = images[i];
                        thumbnailElement.alt = file.name;
                        thumbnailElement.src = dataUrl;
                    }
                    setTimeout(function () { file.previewElement.classList.add("dz-image-preview"); }, 1);
                }
            },
            init: function () {
                //set the existing picture in the dropzone container
                var mockFile = { name: $('#videoThumb').val(), size: 99999};
                this.options.addedfile.call(this, mockFile);
                debugger;
                this.options.thumbnail.call(this, mockFile, "/Images/Thumbs/" + $('#videoId').val() + "/" + $('#videoThumb').val());
                $('.dz-image img').css("width", "120px").css("height", "120px");

                //when returning from server side after saving
                this.on("success", function (file, result) {
                    if (result.Success) {
                        $('.alert-success').text(result.Message).show();
                        var t = $('table').eq(0).DataTable();
                        t.ajax.reload();
                        setTimeout(function () {
                            $('.alert-success').hide();
                            $('.modal').modal('hide');
                        },2000)
                    }
                    else {
                        $('.alert-danger').text(result.Message).show();
                        setTimeout(function () {
                            dropzone.removeAllFiles();
                            $('.alert-danger').hide();
                        }, 2000)
                    }
                });

                //before sending to server side - add video id
                this.on("sending", function (file, xhr, formData) {
                    formData.append("videoId", $('#videoId').val());
                });

                //remove previous loaded pictures (custom way to verify only one file is loading)
                this.on("addedfile", function (file) {
                    $('.dz-preview.dz-image-preview').remove();
                })
            }

        });
        $(document).on("click", "#saveThumb", function (e) {
            e.preventDefault();
            e.stopPropagation();
            dropzone.processQueue(); // Tell Dropzone to process all queued files.
        });

       



    }

    var formValidation = function () {

        var form = $('videoForm');
        var error = $('.alert-danger');
        var success = $('.alert-success');

        $('#videoForm').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input

            rules: {
                Year: {
                    digits: true,
                    range: [1900, 2016],
                    required: true
                },
                Name: {
                    required: true
                },
                Director: {
                    required: true
                },
                Genre: {
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
                debugger;
                success.show();
                error.hide();
                spinner.spin();
                form.appendChild(spinner.el);
                $.ajax({
                    url: "/Video/EditVideo",
                    data: $(form).serialize(),
                    type: "POST",

                    success: function (result) {
                        spinner.stop();
                        if (result.Success) {
                            error.hide();
                            success.text(result.Message).show();

                            //reloading table
                            var t = $('table').eq(0).DataTable();
                            t.ajax.reload();

                            //closing modal
                            setTimeout(function () {
                                $('.modal').modal('hide');
                            }, 3000);
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

    return {
        init: init
    }
}();