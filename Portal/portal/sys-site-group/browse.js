function init(module, params) {

    module.myColumns = [{ label: 'Group Name', width: '50%', field: 'name', class: 'key', link: '/portal/sys-site-group/detail.htm', menu: 'Edit Group: %%' },
                        { label: 'Group Code', width: '50%', field: 'site_group_code'}];

    module.myFilters = [{ name: 'mylast', field: 'name', label: 'Group Name', criteria: 'contains' },
                { name: 'myfirst', field: 'site_group_code', label: 'Group Code', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Site Group Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'site_group_id',
        height: 430,
        newBtn: 'Add New Site Group',
        newLocation: '/portal/sys-site-group/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'site_group_id',
        webservice: '/services/sys_site_group_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

