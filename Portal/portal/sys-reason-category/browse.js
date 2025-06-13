function init(module, params) {

    module.myColumns = [{ label: 'Reason Category', width: '100%', field: 'reason_category', class: 'key', link: '/portal/sys-reason-category/detail.htm', menu: 'Reason Category: %%' } ];

    module.myFilters = [{ name: 'myReason', field: 'reason_category', label: 'Reason Category', criteria: 'contains' }]

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Reason Category Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'reason_category_id',
        height: 430,
        newBtn: 'Add New Category',
        newLocation: '/portal/sys-reason-category/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'reason_category_id',
        webservice: '/services/sys_reason_Services.asmx/GetAllCategories',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

