function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('menu');
        module.hideTab('access');
    }

    module.aAccess = new Array(['0', 'Update'], ['1', 'View']);
    module.addOptions('menu_mode', module.aAccess);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_role',
        pk: 'role_id',
        webservice: '/services/sys_role_Services.asmx/GetByID',
        params: '"id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.dsBrowseMenus.settings.params = '"role_id": "' + ID + '"';
                module.dsBrowseMenus.Reload();
                module.dsBrowseAccess.settings.params = '"role_id": "' + ID + '"';
                module.dsBrowseAccess.Reload();
                module.showDeleteBtn();
                module.showTab('menu');
                module.showTab('access');
                DisplayMessage(module, 'Save Successful');
            });
        }
    }

    module.deleteBtn_onclick = function () {
        module.dsDetail.DeleteRow();
        module.dsDetail.Save(function () {
            module.closeModule();
            return false;
        });
    }

    module.validateForm = function () {
        return true;
    }


    // Browse Menus
    module.myMenuColumns = [{ label: 'Menu', width: '95%', field: 'menu_name' },
                            { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myMenuFilters = [{ name: 'myMenuName', field: 'menu_header', label: 'Menu Name', criteria: 'contains' }];

    module.menuGrid = new spaGrid({
        form: module,
        id: 'menuGrid',
        title: "Select Menus",
        columns: module.myMenuColumns,
        filters: module.myMenuFilters,
        rowID: 'role_feature_id',
        height: 210,
        newBtn: 'Select Menu',
        newLocation: function () {
            module.showModal('menuModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_menu_Services.asmx/UnassignMenu",
                data: "{ 'role_id': '" + ID + "', 'menu_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseMenus.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseMenus = new spaDataset({
        form: module,
        id: 'dsBrowseMenus',
        pk: 'menu_id',
        webservice: '/services/sys_menu_Services.asmx/GetAllAssigned',
        params: '"role_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Menu
    module.mySelectMenuColumns = [{ label: 'Menu', width: '50%', field: 'menu_name', class: 'key'}];
    module.mySelectMenuFilters = [{ name: 'myName', field: 'menu_name', label: 'Menu', criteria: 'contains'}];

    module.selectMenuGrid = new spaGrid({
        form: module,
        id: 'selectMenuGrid',
        title: "Select Menu",
        columns: module.mySelectMenuColumns,
        filters: module.mySelectMenuFilters,
        rowID: 'menu_id',
        height: 210,
        onSelect: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_menu_Services.asmx/AssignMenu",
                data: "{ 'role_id': '" + ID + "', 'menu_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseMenus.Reload();
                        module.hideModal('menuModal');
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectMenu = new spaDataset({
        form: module,
        id: 'dsSelectMenu',
        pk: 'menu_id',
        webservice: '/services/sys_menu_Services.asmx/GetAllUnassigned',
        params: '"role_id": "' + ID + '"',
        pageSize: 30
    });

    // Browse Access Features
    module.myAccessColumns = [{ label: 'Feature Name', width: '95%', field: 'access_name' },
                              { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myAccessFilters = [{ name: 'myFeatureName', field: 'access_name', label: 'Feature Name', criteria: 'contains'}];

    module.accessGrid = new spaGrid({
        form: module,
        id: 'accessGrid',
        title: "Assigned Features",
        columns: module.myAccessColumns,
        filters: module.myAccessFilters,
        rowID: 'access_id',
        height: 210,
        newBtn: 'Assign Feature',
        newLocation: function () {
            module.dsSelectAccess.Reload();
            module.showModal('accessModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_access_Services.asmx/DeleteAccessForRole",
                data: "{ 'role_id': '" + ID + "', 'access_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseAccess.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseAccess = new spaDataset({
        form: module,
        id: 'dsBrowseAccess',
        pk: 'access_id',
        webservice: '/services/sys_access_Services.asmx/GetAllByRoleAssigned',
        params: '"role_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Feature
    module.mySelectAccessColumns = [{ label: 'Feature Name', width: '50%', field: 'access_name', class: 'key'}];

    module.mySelectAccessFilters = [{ name: 'myName', field: 'access_name', label: 'Feature Name', criteria: 'contains'}];

    module.selectAccessGrid = new spaGrid({
        form: module,
        id: 'selectAccessGrid',
        title: "Select Feature",
        columns: module.mySelectAccessColumns,
        filters: module.mySelectAccessFilters,
        rowID: 'access_id',
        height: 210,
        onSelect: function (id) {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_access_Services.asmx/AddAccessForRole",
                data: "{ 'role_id': '" + ID + "', 'access_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseAccess.Reload();
                        module.hideModal('accessModal');
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectAccess = new spaDataset({
        form: module,
        id: 'dsSelectAccess',
        pk: 'access_id',
        webservice: '/services/sys_access_Services.asmx/GetAllByRoleUnassigned',
        params: '"role_id": "' + ID + '"',
        pageSize: 30
    });


}


