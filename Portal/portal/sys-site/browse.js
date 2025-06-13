function init(module, params) {

    module.myColumns = [{ label: 'Site', width: '50%', field: 'site_code', class: 'key', link: '/portal/sys-site/detail.htm', menu: 'Site: %%' },
                        { label: 'Name', width: '50%', field: 'name'}];

    module.myFilters = [{ name: 'myName', field: 'name', label: 'Name', criteria: 'contains' },
                        { name: 'myCode', field: 'site_code', label: 'Site Code', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Browse Site",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'site_id',
        height: 430,
        newBtn: 'Add New',
        newLocation: '/portal/sys-site/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'site_id',
        webservice: '/services/sys_site_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}



