function init(module, params) {

    module.myColumns = [{ label: 'BU', width: '5%', field: 'bu_name'},
                        { label: 'Transaction Time', width: '15%', field: 'transaction_timestamp', type: 'datetime', format: 'datetime' },
                        { label: 'Shift', width: '5%', field: 'shift_id' },
                        { label: 'Employee', width: '20%', field: 'employee_name' },
                        { label: 'Item', width: '25%', field: 'item_name' },
                        { label: 'List Price', width: '10%', field: 'retail_price', type: 'numeric', format: 'money' },
                        { label: 'Sold Price', width: '10%', field: 'unit_price', type: 'numeric', format: 'money' },
                        { label: 'Quantity', width: '10%', field: 'sale_qty', type: 'numeric'}];

    module.myFilters = [{ name: 'myBU', field: 'bu_id', label: 'BU', criteria: 'contains' },
                        { name: 'myBUDate', field: 'bu_date', label: 'Date', criteria: 'lessthan' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Cashier Overrides",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'bu_id',
        height: 430
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'bu_id',
        webservice: '/services/custom_supplier_Services.asmx/GetOverridesByEmployee',
        params: '"bu_id": "' + params.bu_id + '", "bu_date": "' + params.bu_date + '", "employee_id": "' + params.employee_id + '" ',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

