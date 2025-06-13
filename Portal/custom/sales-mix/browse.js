function init(module, params) {

    module.myColumns = [{ label: 'BU', width: '5%', field: 'bu_name'},
                        { label: 'BU Date', width: '10%', field: 'bu_date', type: 'date', format: 'date' },
                        { label: 'Sub Category', width: '25%', field: 'sub_category' },
                        { label: 'Item Name', width: '25%', field: 'item_name' },
                        { label: 'Gross Qty', width: '7%', field: 'gross_sold_qty', type: 'numeric' },
                        { label: 'Gross Amt', width: '7%', field: 'gross_sold_amt', type: 'numeric', format: 'money' },
                        { label: 'Refund Amt', width: '7%', field: 'refund_amt', type: 'numeric', format: 'money' },
                        { label: 'Reduction Amt', width: '7%', field: 'reduction_amt', type: 'numeric', format: 'money' },
                        { label: 'Net Amt', width: '7%', field: 'net_sold_amt', type: 'numeric', format: 'money' }];

    module.myFilters = [{ name: 'myBU', field: 'bu_name', label: 'BU', criteria: 'contains' },
                        { name: 'myBUDate', field: 'bu_date', label: 'BU Date', criteria: 'lessthan' },
                        { name: 'mySub', field: 'sub_category', label: 'Sub Category', criteria: 'contains' },
                        { name: 'myItem', field: 'item_name', label: 'Item Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Sales Mix Report",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'bu_id',
        height: 430
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'bu_id',
        webservice: '/services/custom_supplier_Services.asmx/GetSalesMixReport',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

