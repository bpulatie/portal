function init(module, params) {

    module.myColumns = [{ label: 'Group Name', width: '100%', field: 'group_name', class: 'key', link: '/portal/sys-value/detail.htm', menu: 'Edit Group: %%'}]

    module.myFilters = [{ name: 'myName', field: 'group_name', label: 'Group Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "System Value Group Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'group_id',
        height: 430,
        newBtn: 'Add New Value Group',
        newLocation: '/portal/sys-value/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'group_id',
        webservice: '/services/sys_value_Services.asmx/GetAllGroups',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

