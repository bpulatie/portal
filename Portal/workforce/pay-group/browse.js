function init(module, params) {

    module.myColumns = [{ label: 'Pay Group', width: '20%', field: 'pay_group_code', class: 'key', link: '/workforce/pay-group/detail.htm', menu: 'Pay Group: %%' },
                        { label: 'Filter', width: '80%', field: 'filter_description' }]

    module.myFilters = [{ name: 'myCode', field: 'pay_group_code', label: 'Pay Group', criteria: 'contains' }]

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Pay Group Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'pay_group_id',
        height: 430,
        newBtn: 'Add New Pay Group',
        newLocation: '/workforce/pay-group/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'pay_group_id',
        webservice: '/services/pay_group_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

