

var videoTable = function () {



    var init = function () {
        $('#videoTable').DataTable({
            "ajax": "/video/getvideos",
            "columns": [
                { "data": "ID" },
                { "data": "Name" },
                { "data": "Director" },
                { "data": "Genre" },
                { "data": "Brief" },
                { "data": "Year" },
                { "data": "Thumb" },
                { "data": "ID" }
            ],
            "fnDrawCallback": function (oSettings) {
                
            },
            "aoColumnDefs": [
                {
                    "mRender": function (full, data, row) {
                        if (full != '' && full != null) {
                            return "<a href='/Images/Thumbs/" + row.ID + "/" + full + "' ><img src='/Images/Thumbs/" + row.ID + "/" + full + "' width='100px' height='100px'/></a>";
                        }
                        else {
                            return "";
                        }
                    },
                    "aTargets": [6]
                },
                {
                    "mRender": function (full, data, row) {
                        var editButton = "<div><button type='button' data-id='" + full + "' class='Edit btn btn-info' > Edit </button>";
                        var removeButton = "<button type='button' data-name='" + row.Name +"' data-id='" + full + "'  class='Delete btn btn-danger'  > Remove </button></div>";
                        return editButton + " " +  removeButton;
                    },
                    "aTargets": [7]
                }


            ]
        });


        bindEvents();
    };

    var bindEvents = function () {

        $(document).on("click", ".Edit", function () {
            var id = $(this).data("id");
            var model = {};
            model.Title = "Edit Video";
            model.ActionName = "GetVideo";
            model.ControllerName = "Video";
            model.ID = id;
            model.ModalType = "Large";
            var modCon = $('#ajax');
            ajaxModal(modCon, '/Modal/Index', model, 'GET', function () {
                //handleValidation($(mid + ' #invite-agent-form'), InviteAgent);
                //addValidation();
            },
			function () {

			});
        });

        $(document).on("click", ".Delete", function () {
            var id = $(this).data("id");
            var model = {};
            model.Title = "Delete Video";
            model.Title2 = "Are you sure you want to delete " + $(this).data("name") + "?";
            model.ActionName = "GetDeleteVideo";
            model.ControllerName = "Video";
            model.ID = id;
            model.ModalType = "Large";
            var modCon = $('#ajax');
            ajaxModal(modCon, '/Modal/Index', model, 'GET', function () {
                //handleValidation($(mid + ' #invite-agent-form'), InviteAgent);
                //addValidation();
            },
			function () {

			});
        });
    }
    return {
        init: init
    }
}();