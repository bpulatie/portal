function init(module, params) {


    module.myColumns = [{ label: 'Week No', width: '25%', field: 'week_no', class: 'key', link: '/workforce/pay-specialist/detail.htm', menu: 'Pay Period: %%' },
                        { label: 'Status', width: '15%', field: 'status_name' },
                        { label: 'Start Date', width: '15%', field: 'start_date', type: 'date', format: 'date' },
                        { label: 'End Date', width: '15%', field: 'end_date', type: 'date', format: 'date' },
                        { label: 'Open Exceptions', width: '15%', field: 'open_exceptions', type: 'numeric' },
                        { label: 'Closed Exceptions', width: '15%', field: 'closed_exceptions', type: 'numeric'}];

    module.aOpen = new Array(['o', 'Open Pay Periods'], ['c', 'Closed Pay Periods']);

    module.myFilters = [{ name: 'myStatus', field: 'status_code', label: 'Status', type: 'array', values: module.aOpen, criteria: 'equals'},
                        { name: 'myName', field: 'start_date', label: 'Start Date', type: 'date', criteria: 'greaterthan' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Pay Periods",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'pay_period_id',
        height: 430,
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'pay_period_id',
        filter: '[ { "column": "status_code", "comparison": "equals", "value": "o" } ]',
        webservice: '/services/pay_period_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}



