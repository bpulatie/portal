function init(module, params) {

    module.myColumns = [{ label: 'Job Name', width: '50%', field: 'job_name', class: 'key', link: '/async/async-job/detail.htm', menu: 'Async Job: %%' },
                        { label: 'Job Type', width: '50%', field: 'schedule_code_name'}];

    module.myFilters = [{ name: 'myJobName', field: 'job_name', label: 'Job Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Async Job Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'job_id',
        height: 430,
        newBtn: 'Add New Job',
        newLocation: '/async/async-job/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'job_id',
        webservice: '/services/async_job_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}





