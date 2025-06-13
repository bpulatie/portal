function init(module, params) {

    module.aGroups = new Array(); ;

    module.dsGroups = new spaDataset({
        form: module,
        id: 'dsGroup',
        table: 'sys_feature_group',
        pk: 'group_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllFeatureGroups',
        onLoad: function () {

            module.aGroups.push(['', 'Show All']);

            for (var x = 0; x < module.dsGroups.RowCount(); x++) {
                var row = module.dsGroups.GetRow(x);
                module.aGroups.push([row.group_id, row.group_name]);
            }

            module.myColumns = [{ label: 'Feature Group Name', width: '30%', field: 'group_name' },
                        { label: 'Feature Name', width: '30%', field: 'feature_name', class: 'key', link: '/portal/sys-feature/detail.htm', menu: 'Edit Feature: %%' },
                        { label: 'Moniker', width: '40%', field: 'moniker'}];

            module.myFilters = [{ name: 'myGroup', field: 'f.group_id', label: 'Feature Group Name', type: 'array', values: module.aGroups, criteria: 'equals'},
                        { name: 'myName', field: 'feature_name', label: 'Feature Name', criteria: 'contains'}];

            module.myGrid = new spaGrid({
                form: module,
                id: 'myGrid',
                title: "Feature Browse",
                columns: module.myColumns,
                filters: module.myFilters,
                rowID: 'feature_id',
                height: 430,
                newBtn: 'Add New Feature',
                newLocation: '/portal/sys-feature/detail.htm',
                onLoad: function () {
                }
            });

            module.dsBrowse = new spaDataset({
                form: module,
                id: 'dsBrowse',
                pk: 'feature_id',
                webservice: '/services/sys_feature_Services.asmx/GetAll',
                pageSize: 30
            });

        }
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

