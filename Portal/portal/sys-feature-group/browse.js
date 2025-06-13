function init(module, params) {

    module.myColumns = [{ label: 'Application Name', width: '100%', field: 'group_name', class: 'key', link: '/portal/sys-feature-group/detail.htm', menu: 'Edit Application: %%' }];
    module.myFilters = [{ name: 'myName', field: 'group_name', label: 'Application Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Application Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'group_id',
        height: 430,
        newBtn: 'Add New Application',
        newLocation: '/portal/sys-feature-group/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'group_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllFeatureGroups',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

