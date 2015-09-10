

var videoTable = function () {



    var init = function () {
        var table = $('#videoTable').DataTable({
            "ajax": "/video/getvideos",
            "columns": [
                { "data": "Name" },
                { "data": "Director" },
                { "data": "Genre" },
                { "data": "Year" },
                { "data": "Thumb" },
                { "data": "ID" }
            ],

            "order": [], 
         
            "fnDrawCallback": function (oSettings) {
                $('#videoTable input[type=checkbox]').bootstrapSwitch();
            },
            "aoColumnDefs": [
                {
                    "mRender": function (full, data, row) {
                        if (full != '' && full != null) {
                            return "<a href='/Images/Thumbs/" + row.ID + "/" + full + "' ><img src='/Images/Thumbs/" + row.ID + "/" + full + "' width='100px' height='100px' /></a>";

                        }
                        else {
                            return "";
                        }
                    },
                    "aTargets": [4]
                },

                {
                    "mRender": function (full, data, row) {
                        var canRent = $('#userCanRent').val();
                        if (canRent == 1) {
                            var string = "<div style='{opacity=0;}'> </div>" +  "<input type='checkbox' data-id='" + full + "' " + (row.IsRented ? "checked='checked' disabled='disabled'" : "") + " data-on-text='Rented' data-on-color='danger'  data-off-color='success' data-off-text='Available' />";
                        }
                        else if ($('#userVideoID').val() == full) {
                            var string = "<span style='{opacity=0;}'> </span>" + "<input type='checkbox' data-id='" + full + "' " + (row.IsRented ? "checked='checked' " : "") + " data-on-text='Rented' data-on-color='danger'  data-off-color='success' data-off-text='Available' />";

                        }
                        else {
                            var string = "<span style='{opacity=0;}'> </span>" + "<input type='checkbox' data-id='" + full + "' " + (row.IsRented ? "checked='checked' " : "") + "disabled='disabled' data-on-text='Rented' data-on-color='danger'  data-off-color='success' data-off-text='Available' />";

                        }

                        return   string;
                    },
                    "aTargets": [5]
                }
            ]
        });

        var tabl = $('table').dataTable().columnFilter();

        bindEvents();
    };

    var bindEvents = function () {

        var reload = function () { //reloading table
            var t = $('table').eq(0).DataTable();
            t.ajax.reload();
        }

       

        $(document).on("click", "#returnBook", function () {

            var userid = $('#userId').val();

            $.ajax({
                url: "/User/ReturnUserVideo?userid=" + userid,
                type: "GET",
                success: function (data) {
                    videoReturned();
                },
                error: function (ex) {

                    console.log(ex);
                }
            });
        });

        $('table').on('switchChange.bootstrapSwitch', 'input[type=checkbox]', function (event, state) {
            if (state) {
                //rent video
                var userid = $('#userId').val();
                var videoid = $(this).data("id");
                $.ajax({
                    url: "/User/RentVideo",
                    data: { UserID: userid, VideoID: videoid },
                    type: "POST",
                    success: function (data) {
                        videoTaken(videoid);
                    },
                    error: function (ex) {

                        console.log(ex);
                    }
                });
            }
            else {
                //return video
                var userid = $('#userId').val();
                var videoid = $('#userVideoID').val();
                $.ajax({
                    url: "/User/ReturnVideo",
                    type: "POST",
                    data: { UserID: userid, VideoID: videoid },
                    success: function (data) {
                        videoReturned();
                    },
                    error: function (ex) {

                        console.log(ex);
                    }
                });
            }
        });

        
        var videoReturned = function () { //actions to be made after video returned
            $('#userCanRent').val("1");
            $("#returnBook").attr("disabled", true);
            $('#userVideoID').val(0);
            reload();
        }

        var videoTaken = function (uservideoid) {//actions to be made after video taken by user
            $('#userCanRent').val("0");
            $("#returnBook").attr("disabled", false);
            $('#userVideoID').val(uservideoid);
            reload();
        }
    }
    return {
        init: init
    }
}();