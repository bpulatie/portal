function init(module, params) {

    module.myColumns = [{ label: 'Job Code', width: '20%', field: 'job_code', class: 'key', link: '/workforce/pay-job/detail.htm', menu: 'Job: %%' },
                        { label: 'Company', width: '10%', field: 'company' },
                        { label: 'Name', width: '40%', field: 'name' },
                        { label: 'External Code', width: '20%', field: 'external_code' },
                        { label: 'Active', width: '10%', field: 'active_flag', type: 'check' }];

    module.myFilters = [{ name: 'myName', field: 'name', label: 'Name', criteria: 'contains' },
                        { name: 'myCode', field: 'job_code', label: 'Job Code', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Browse Job",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'job_id',
        height: 430,
        newBtn: 'Add New',
        newLocation: '/workforce/pay-job/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'job_id',
        webservice: '/services/pay_job_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}



