function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'async_execution',
        pk: 'execution_id',
        webservice: '/services/async_queue_Services.asmx/GetByExecutionID',
        params: '"execution_id": "' + ID + '"'
    });

    // Browse Tasks
    module.myTaskColumns = [{ label: 'Task Name', width: '20%', field: 'task_name' },
                            { label: 'Moniker', width: '20%', field: 'moniker' },
                            { label: 'Execution Time', width: '15%', field: 'execution_time', type: 'datetime', format: 'datatime' },
                            { label: 'Status', width: '10%', field: 'status_name' },
                            { label: 'Message', width: '35%', field: 'status_message' }];

    module.myTaskFilters = [{ name: 'myTaskName', field: 'task_name', label: 'Task Name', criteria: 'contains' }];

    module.taskGrid = new spaGrid({
        form: module,
        id: 'taskGrid',
        title: "Task Detail",
        columns: module.myTaskColumns,
        filters: module.myTaskFilters,
        rowID: 'execution_detail_id',
        newBtn: "Refresh",
        newLocation: function () {
            module.dsBrowseTasks.Reload();
        },
        height: 280
    });

    module.dsBrowseTasks = new spaDataset({
        form: module,
        id: 'dsBrowseTasks',
        pk: 'execution_detail_id',
        webservice: '/services/async_queue_Services.asmx/GetAllTasksByExecutionID',
        params: '"execution_id": "' + ID + '"',
        pageSize: 30
    });

}








