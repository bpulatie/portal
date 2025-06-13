function init(module, params) {

    // Tree Grid
    module.myColumns = [{ label: 'BU Date', width: '7%', field: 'bu_date', type: 'date', format: 'date', level: "1" },
                        { label: 'BU', width: '3%', field: 'bu_name', level: "2" },
                        { label: 'Department', width: '15%', field: 'department', level: "3" },
                        { label: 'Category', width: '15%', field: 'category', level: "4" },
                        { label: 'Sub Category', width: '15%', field: 'sub_category', level: "5" },
                        { label: 'Item Name', width: '20%', field: 'item_name' },
                        { label: 'Gross Qty', width: '5%', field: 'gross_sold_qty', type: 'numeric' },
                        { label: 'Gross Amt', width: '5%', field: 'gross_sold_amt', type: 'numeric', format: 'money' },
                        { label: 'Net Amt', width: '5%', field: 'net_sold_amt', type: 'numeric', format: 'money' },
                        { label: 'Refund Amt', width: '5%', field: 'refund_amt', type: 'numeric', format: 'money' },
                        { label: 'Reduct Amt', width: '5%', field: 'reduction_amt', type: 'numeric', format: 'money' }];

    module.myGrid = new spaTreeGrid({
        form: module,
        id: 'myGrid',
        title: "Sales Mix Report",
        columns: module.myColumns,
        rowID: 'bu_id',
        height: 380,
        onExpand: function (g) {
            var id = $(this).attr('level');

            if ($("#" + id)[0].childElementCount < 1) {

                var row = jQuery.parseJSON($(this).parent().parent().attr("row"));

                level = parseInt(row.level, 10) + 1;
                var params = '"level": "' + level + '", "p1": "' + row['p1'] + '", "p2": "' + row['p2'] + '", "p3": "' + row['p3'] + '", "p4": "' + row['p4'] + '", "p5": "' + row['p5'] + '", "p6": "' + row['p6'] + '"';

                module.dsBrowse.settings.params = params;
                module.dsBrowse.Reload();
            }

            $("#" + id).show();
            $(this).removeClass('glyphicon-triangle-right');
            $(this).addClass('glyphicon-triangle-bottom');

            $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).unbind('click');
            $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).unbind('click');

            if (typeof g.settings.onExpand === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).on('click', $.proxy(g.settings.onExpand, null, g));
            }

            if (typeof g.settings.onCollapse === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).on('click', $.proxy(g.settings.onCollapse, null, g));
            }

        },
        onCollapse: function (g) {
            var id = $(this).attr('level');
            $("#" + id).hide();
            $(this).removeClass('glyphicon-triangle-bottom');
            $(this).addClass('glyphicon-triangle-right');

            $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).unbind('click');
            $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).unbind('click');

            if (typeof g.settings.onExpand === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).on('click', $.proxy(g.settings.onExpand, null, g));
            }

            if (typeof g.settings.onCollapse === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).on('click', $.proxy(g.settings.onCollapse, null, g));
            }
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        webservice: '/services/custom_supplier_Services.asmx/GetSalesMixTreeGridData',
        params: '"level": "1", "p1": "", "p2": "", "p3": "", "p4": "", "p5": "", "p6": ""'
    });

}

