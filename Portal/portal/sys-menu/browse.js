function init(module, params) {

    module.myColumns = [{ label: 'Menu', width: '50%', field: 'menu_name', class: 'key', link: '/portal/sys-menu/detail.htm', menu: 'Menu: %%'},
                        { label: 'Sort_order', width: '50%', field: 'sort_order' }];

    module.myFilters = [{ name: 'myHeader', field: 'menu_name', label: 'Menu', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Menu Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'menu_id',
        height: 430,
        newBtn: 'Add New Menu',
        newLocation: '/portal/sys-menu/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'menu_id',
        webservice: '/services/sys_menu_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

