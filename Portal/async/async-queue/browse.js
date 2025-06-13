function init(module, params) {

    module.myColumns = [{ label: 'Queue Name', width: '40%', field: 'queue_name', class: 'key', link: '/async/async-queue/detail.htm', menu: 'Queue: %%' },
                        { label: 'Thread Count', width: '20%', field: 'thread_count' },
                        { label: 'Queue Count', width: '20%', field: 'queue_count' },
                        { label: 'Processing Count', width: '20%', field: 'process_count' },
                        ];

    module.myFilters = [{ name: 'myQueueName', field: 'queue_name', label: 'Queue Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Async Queue Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'queue_id',
        height: 430,
        newBtn: 'Add New Queue',
        newLocation: '/async/async-queue/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'queue_id',
        webservice: '/services/async_queue_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}





