function init(module, params) {

    module.myColumns = [{ label: 'Application Name', width: '100%', field: 'application_name', class: 'key', link: '/portal/sys-application/detail.htm', menu: 'Edit Application: %%' }];
    module.myFilters = [{ name: 'myName', field: 'application_name', label: 'Application Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Application Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'application_id',
        height: 430,
        newBtn: 'Add New Application',
        newLocation: '/portal/sys-application/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'group_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllApplications',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

