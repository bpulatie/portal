function init(module, params) {

    module.myColumns = [{ label: 'Role', width: '50%', field: 'role_name', class: 'key', link: '/portal/sys-role/detail.htm', menu: 'Edit Role: %%' },
                        { label: 'External Name', width: '50%', field: 'external_name'}];

    module.myFilters = [{ name: 'myName', field: 'role_name', label: 'Role Name', criteria: 'contains' },
                        { name: 'myExternal', field: 'external_name', label: 'External Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Role Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'role_id',
        height: 430,
        newBtn: 'Add New Role',
        newLocation: '/portal/sys-role/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'role_id',
        webservice: '/services/sys_role_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };

}

