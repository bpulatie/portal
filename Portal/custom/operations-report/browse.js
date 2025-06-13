function init(module, params) {

    module.myColumns = [{ label: 'BU', width: '3%', field: 'bu_name', class: 'key'},
                        { label: 'BU Date', width: '8%', field: 'bu_date', type: 'date', format: 'date' },
                        { label: 'EOD Time', width: '10%', field: 'eod_time', type: 'datetime', format: 'datatime' },
                        { label: 'Trx Qty', width: '4%', field: 'net_trans_qty', type: 'numeric' },
                        { label: 'Grs Sales', width: '5%', field: 'gross_sold_amt', type: 'numeric', format: 'money' },
                        { label: 'Net Sales', width: '5%', field: 'system_net_sold_amt', type: 'numeric', format: 'money' },
                        { label: 'Over Shrt', width: '5%', field: 'over_short_amt', type: 'numeric', format: 'money' },
                        { label: 'Ovr Amt', width: '5%', field: 'cashier_override_amt', type: 'numeric', format: 'money' },
                        { label: 'Ovr Qty', width: '5%', field: 'cashier_override_qty', type: 'numeric' },
                        { label: 'Cpn Amt', width: '5%', field: 'net_coupon_amt', type: 'numeric', format: 'money' },
                        { label: 'Dis Amt', width: '5%', field: 'net_discount_amt', type: 'numeric', format: 'money' },
                        { label: 'No Sales', width: '5%', field: 'no_sale_qty', type: 'numeric' },
                        { label: 'Refnd Amt', width: '5%', field: 'refund_amt', type: 'numeric', format: 'money' },
                        { label: 'POut Amt', width: '5%', field: 'payout_amt', type: 'numeric', format: 'money' },
                        { label: 'Itm X Qty', width: '5%', field: 'item_cancel_qty', type: 'numeric' },
                        { label: 'Itm X Amt', width: '5%', field: 'item_cancel_amt', type: 'numeric', format: 'money' },
                        { label: 'Trn X Qty', width: '5%', field: 'trans_cancel_qty', type: 'numeric' },
                        { label: 'Fuel Glln', width: '5%', field: 'fuel_gallons', type: 'numeric' },
                        { label: 'DOff Amt', width: '5%', field: 'drive_off_amt', type: 'numeric', format: 'money'}];

    module.myFilters = [{ name: 'myBU', field: 'bu_name', label: 'BU', criteria: 'contains' },
                        { name: 'myBUDate', field: 'bu_date', label: 'BU Date Before', type: 'date', criteria: 'lessthan' },
                        { name: 'myOverShort', field: 'over_short_amt', label: 'Over Short Greater Than', type: 'numeric', criteria: 'greaterthan' },
                        { name: 'myPayOut', field: 'payout_amt', label: 'Pay Outs Greater Than', type: 'numeric', criteria: 'greaterthan' },
                        { name: 'myRefunds', field: 'refund_amt', label: 'Refunds Greater Than', type: 'numeric', criteria: 'greaterthan' },
                        { name: 'myItemCan', field: 'item_cancel_amt', label: 'Item Cancel Greater Than', type: 'numeric', criteria: 'greaterthan' },
                        
                        
                        ];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Operations Report",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'bu_id',
        height: 430,
        onSelect: function (id) {
            var BuID = module.dsBrowse.GetCurrentRow().bu_id;
            var BuDate = module.dsBrowse.GetCurrentRow().bu_date;
            createModule("Employee Detail", "/custom/operations-report/detail.htm", { 'bu_id': BuID, 'bu_date': BuDate }, 0);
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'bu_id',
        webservice: '/services/custom_supplier_Services.asmx/GetOperationReport',
        params: '"bu_id": "' + params.bu_id + '", "bu_date": "' + params.bu_date + '" ',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

