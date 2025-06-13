function init(module, params) {

    module.myColumns = [{ label: 'Event Category', width: '20%', field: 'event_category_name', class: 'key', link: '/portal/sys-event-category/detail.htm', menu: 'Event Category: %%'}];
    module.myFilters = [{ name: 'myCategory', field: 'event_category', label: 'Category', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Event Category Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'event_category_id',
        height: 430,
        newBtn: 'Add New Event Category',
        newLocation: '/portal/sys-event-category/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'event_category_id',
        webservice: '/services/sys_event_Services.asmx/GetAllCategories',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

