function init(module, params) {

    module.myColumns = [{ label: 'Last Name', width: '25%', field: 'last_name', class: 'key', link: '/portal/sys-user/detail.htm', menu: 'Edit User: %%' },
                { label: 'First Name', width: '25%', field: 'first_name' },
                { label: 'Login Name', width: '25%', field: 'login_name' },
                { label: 'Email', width: '25%', field: 'email'}];

    module.myFilters = [{ name: 'mylast', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                { name: 'myfirst', field: 'first_name', label: 'First Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "User Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'user_id',
        height: 430,
        newBtn: 'Add New User',
        newLocation: '/portal/sys-user/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetUsers',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

