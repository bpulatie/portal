function init(module, params) {

    module.myColumns = [{ label: 'Client Name', width: '25%', field: 'name', class: 'key', link: '/portal/sys-client/detail.htm', menu: 'Edit Client: %%'},
                        { label: 'Status', width: '15%', field: 'status_name' },
                        { label: 'Contact', width: '25%', field: 'contact_name' },
                        { label: 'Phone', width: '15%', field: 'contact_phone' },
                        { label: 'Sites Count', width: '10%', field: 'site_count'},
                        { label: 'User Count', width: '10%', field: 'user_count'}];

    module.myFilters = [{ name: 'myName', field: 'name', label: 'Client Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Client Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'client_id',
        height: 430,
        newBtn: 'Add New Client',
        newLocation: '/portal/sys-client/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'client_id',
        webservice: '/services/sys_client_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

