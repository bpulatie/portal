function init(module, params) {

    module.myColumns = [{ label: 'Name', width: '30%', field: 'metric_name', class: 'key', link: '/metric/config/detail.htm', menu: 'Metric: %%'},
                        { label: 'Description', width: '70%', field: 'metric_detail' }];


    module.aEvents = new Array(['', 'Show All'], ['e', 'Error'], ['w', 'Warning'], ['i', 'Information']);

    module.myFilters = [{ name: 'myName', field: 'metric_name', label: 'Name', criteria: 'contains' },
                        { name: 'myDescription', field: 'event_detail', label: 'Description', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Metric Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'metric_id',
        height: 430,
        newBtn: 'Add New Metric',
        newLocation: '/metric/config/detail.htm', 
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'metric_id',
        webservice: '/services/metric_Services.asmx/GetAllMetrics',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

