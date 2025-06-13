function init(module, params) {

    module.myColumns = [{ label: 'Task Name', width: '30%', field: 'task_name', class: 'key', link: '/async/async-task/detail.htm', menu: 'Async Task: %%' },
                        { label: 'Moniker', width: '70%', field: 'moniker' }];

    module.myFilters = [{ name: 'myJobName', field: 'job_name', label: 'Job Name', criteria: 'contains' },
                        { name: 'myMoniker', field: 'moniker', label: 'Moniker', criteria: 'contains' } ];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Async Task Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'task_id',
        height: 430,
        newBtn: 'Add New Task',
        newLocation: '/async/async-task/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'task_id',
        webservice: '/services/async_task_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}





