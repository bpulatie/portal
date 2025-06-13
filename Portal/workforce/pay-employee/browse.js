function init(module, params, mode) {

    module.myColumns = [{ label: 'Employee ID', width: '20%', field: 'employee_no', class: 'key', link: '/workforce/pay-employee/detail.htm', menu: 'Edit Employee: %%' },
                        { label: 'Last Name', width: '40%', field: 'last_name'},
                        { label: 'First Name', width: '40%', field: 'first_name' }];

    module.myFilters = [{ name: 'mylast', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                        { name: 'myfirst', field: 'first_name', label: 'First Name', criteria: 'contains' },
                        { name: 'myNo', field: 'employee_no', label: 'Employee No', criteria: 'contains' },
                        { name: 'myBU', field: 'site_code', label: 'Site Code', criteria: 'equals' } ];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Employee Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'employee_id',
        height: 430,
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'employee_id',
        webservice: '/services/pay_employee_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}