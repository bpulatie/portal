function init(module, params) {

    module.myColumns = [{ label: 'Access Feature', width: '100%', field: 'access_name', class: 'key', link: '/portal/sys-access/detail.htm', menu: 'Edit Access Feature: %%' }];

    module.myFilters = [{ name: 'myName', field: 'access_name', label: 'Access Feature Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Access Feature Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'access_id',
        height: 430,
        newBtn: 'Add New Access Feature',
        newLocation: '/portal/sys-access/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'access_id',
        webservice: '/services/sys_access_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };

}

