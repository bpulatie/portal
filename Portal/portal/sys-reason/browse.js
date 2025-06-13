function init(module, params) {

    module.myColumns = [{ label: 'Reason Category', width: '20%', field: 'reason_category' },
                        { label: 'Reason Code', width: '80%', field: 'reason_code', class: 'key', link: '/portal/sys-reason/detail.htm', menu: 'Reason: %%' }]

    module.myFilters = [{ name: 'myReason', field: 'reason_code', label: 'Reason Code', criteria: 'contains' }]

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "System Reason Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'reason_id',
        height: 430,
        newBtn: 'Add New Reason',
        newLocation: '/portal/sys-reason/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'reason_id',
        webservice: '/services/sys_reason_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

