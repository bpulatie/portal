function init(module, params) {

    module.myColumns = [{ label: 'Week No', width: '30%', field: 'week_no', class: 'key', link: '/workforce/pay-periods/detail.htm', menu: 'Pay Period: %%' },
                        { label: 'Start Date', width: '30%', field: 'start_date', type: 'date', format: 'date' },
                        { label: 'End Date', width: '30%', field: 'end_date', type: 'date', format: 'date' },
                        { label: 'Status', width: '10%', field: 'status_name' }];

    module.myFilters = [{ name: 'myName', field: 'start_date', label: 'Start Date Before', type: 'date', format: 'date', criteria: 'lessthan' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Browse Pay Period",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'pay_period_id',
        height: 430,
        newBtn: 'Add New',
        newLocation: '/workforce/pay-periods/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'pay_period_id',
        webservice: '/services/pay_period_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}



