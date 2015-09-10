var oLanguageData = {
    "sLengthMenu": undefined,
    "sZeroRecords": undefined,
    "sInfo": undefined,
    "sInfoEmpty": undefined,
    "sInfoFiltered": undefined,
    "sEmptyTable": undefined,
    "sLoadingRecords": undefined,
    "sSearch": undefined,
    "oPaginate": {
        "sFirst": undefined,
        "sLast": undefined,
        "sNext": undefined,
        "sPrevious": undefined
    }
};

jQuery.extend(jQuery.fn.dataTableExt.oSort,
{
    "date-pre": function (a) {
        
        if (a == null || a == "") {
            return 0; 
            
        }
        var ukDatea = a.split('/'); return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },
    "date-asc": function (a, b) {
        

         return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "date-desc":
        function (a, b) {
            

             return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        }
});

var generalDataTable = function (selector, formSelector, ajaxSource, columns, sortDefs, oLanguage, tableType, rowFunc) {
    this.Selector = selector;
    this.AjaxSource = ajaxSource;
    this.FormSelector = formSelector;
    this.SortDefs = sortDefs === undefined ? [] : sortDefs;
    this.Columns = columns !== undefined ? columns : [];
    this.dataTable = {};
    this.oLanguage = $.extend(oLanguageData, oLanguage);
    this.tableType = tableType;
    this.createdRow = rowFunc;
    this.InitDataTable = function (callback) {
        var selector = this.Selector;
        var formSelector = this.FormSelector;

        if (!$.isFunction(callback)) {
            callback = function () { };
        }

        this.dataTable = $(selector).dataTable({
            "autoWidth": false,
            "bProcessing": false,
            "bServerSide": true,
            "bSortCellsTop": true,
            "aaSorting": this.SortDefs,
            "sAjaxSource": this.AjaxSource,
            "aoColumnDefs": this.Columns,
            "oLanguage": this.oLanguage,
            "aLengthMenu": [
               [5, 10, 15, 20, 50, -1],
               [5, 10, 15, 20, 50, "All"]
            ],
            "iDisplayLength": 10,
            "fnServerData": function (sSource, aoData, fnCallback) {
                $.ajax({
                    "dataType": 'json',
                    "type": "POST",
                    "url": sSource,
                    "data": $.merge(aoData, $(formSelector).serializeArray()),
                    "success": fnCallback
                });
            },
            "createdRow": this.createdRow,
            "fnDrawCallback": callback,
            "bDestroy": true,
            "orderMulti": true,
        });

        this.bind(this);
        
    };

    this.ReDraw = function (beforeReDraw, afterReDraw) {
        
        if ($.isFunction(beforeReDraw)) {
            beforeReDraw();
        }
        this.dataTable.fnDraw();
        if ($.isFunction(afterReDraw)) {
            afterReDraw();
        }
    };

    // custom events
    this.bind = function (self) {
        if (self.tableType !== undefined) {
            $(document).on("refresh.DataTable", function (e, data) {
                if (typeof e.tableType !== 'undefined' && $.isArray(e.tableType) && $.inArray(self.tableType, e.tableType) >= 0) {
                    self.ReDraw();
                    if (typeof e.result === 'number')
                        e.result += 1;
                    else
                        e.result = 1;
                }
            });
        }

        if (self.tableType !== undefined) {
            $(document).on('draw.dt', function () {
                try {
                    Metronic.unblockUI($('body'));
                } catch (e) {
                    console.log(e);
                }
            });
        }

    };

};

var GeneralColumnsList = function () {
    this.Data = [];
    this.AddColumn = function (target, searchable, render, bVisible, sTitle, iDataSort,propertyname) {

        if (iDataSort === undefined) {
            iDataSort = target[0];
        }

        if (bVisible === undefined) {
            bVisible = true;
        }
        var newColumn = {
            aTargets: target,
            bSearchable: searchable,
            mRender: render,
            bVisible: bVisible,
            iDataSort: iDataSort,
            data: propertyname
        };

        if (sTitle !== undefined) {
            newColumn.sTitle = sTitle;
        }

        this.Data.push(newColumn);
    };

};