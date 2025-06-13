function init(module, params) {

    module.myColumns = [{ label: 'Event Date', width: '20%', field: 'event_date', type: 'datetime', format: 'date', class: 'key', link: '/portal/sys-event/detail.htm', menu: 'Event: %%'},
                        { label: 'Event Type', width: '20%', field: 'event_type_name' },
                        { label: 'Category', width: '20%', field: 'event_category' },
                        { label: 'Details', width: '40%', field: 'event_summary'}];


    module.aEvents = new Array(['', 'Show All'], ['e', 'Error'], ['w', 'Warning'], ['i', 'Information']);

    module.myFilters = [{ name: 'myCategory', field: 'event_category', label: 'Category', criteria: 'contains' },
                        { name: 'myType', field: 'event_type', label: 'Event Type', type: 'array', values: module.aEvents, criteria: 'equals' },
                        { name: 'myDate', field: 'event_date', label: 'Event Date Before', type: 'date', criteria: 'lessthan'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "System Event Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'event_id',
        height: 430,
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'event_id',
        webservice: '/services/sys_event_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

