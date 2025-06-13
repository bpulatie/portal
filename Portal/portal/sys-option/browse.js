function init(module, params) {

    module.myColumns = [{ label: 'Option Name', width: '100%', field: 'option_name', class: 'key', link: '/portal/sys-option/detail.htm', menu: 'Edit Option: %%'}];

    module.myFilters = [{ name: 'myName', field: 'option_name', label: 'Option Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "System Option Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'option_id',
        height: 430,
        newBtn: 'Add New Option',
        newLocation: '/portal/sys-option/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'option_id',
        webservice: '/services/sys_option_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

