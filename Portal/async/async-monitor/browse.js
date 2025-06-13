function init(module, params) {

    module.aStatus = new Array(['', 'Show All'], ['q', 'Queued'], ['p', 'Processing'], ['c', 'Complete'], ['f', 'Failed']);

    module.myColumns = [{ label: 'Job Name', width: '20%', field: 'job_name', class: 'key', link: '/async/async-monitor/detail.htm', menu: 'Job: %%'},
                        { label: 'Process ID', width: '10%', field: 'process_id' },
                        { label: 'Queue', width: '10%', field: 'queue_name' },
                        { label: 'Queue Time', width: '15%', field: 'queued_time', type: 'datetime', format: 'datatime' },
                        { label: 'Start Time', width: '15%', field: 'start_time', type: 'datetime', format: 'datatime' },
                        { label: 'End Time', width: '15%', field: 'end_time', type: 'datetime', format: 'datatime' },
                        { label: 'Status', width: '15%', field: 'status_name'}];

    module.myFilters = [{ name: 'myJobName', field: 'job_name', label: 'Job Name', criteria: 'contains'},
                        { name: 'myQueueName', field: 'queue_name', label: 'Queue Name', criteria: 'contains'},
                        { name: 'myQueueTime', field: 'queued_time', label: 'Queued Date', type: 'date', format: 'date', criteria: 'greaterthan'},
                        { name: 'myStartTime', field: 'start_time', label: 'Start Date', type: 'date', format: 'date', criteria: 'greaterthan'},
                        { name: 'myStatusName', field: 'status_code', label: 'Status', type: 'array', values: module.aStatus, criteria: 'equals'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Async Queue Monitor",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'execution_id',
        height: 430,
        newBtn: "Refresh",
        newLocation: function () {
            module.dsBrowse.Reload();
        },
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'execution_id',
        webservice: '/services/async_queue_Services.asmx/GetAllExecution',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}





