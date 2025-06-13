function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('sites');
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_site_group',
        pk: 'site_group_id',
        webservice: '/services/sys_site_group_Services.asmx/GetByID',
        params: '"site_group_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.dsDetail.EditColumn("client_id", CONTEXT.client.client_id);
            };
        }
    });

    // Browse Sites
    module.mySiteColumns = [{ label: 'Site Code', width: '35%', field: 'site_code' },
                            { label: 'Site Name', width: '60%', field: 'name' },
                            { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.mySiteFilters = [{ name: 'mySiteCode', field: 'site_code', label: 'Site Code', criteria: 'contains' },
                            { name: 'mySiteName', field: 'name', label: 'Site Name', criteria: 'contains'}];

    module.siteGrid = new spaGrid({
        form: module,
        id: 'siteGrid',
        title: "Assigned Sites",
        columns: module.mySiteColumns,
        filters: module.mySiteFilters,
        rowID: 'site_id',
        height: 210,
        newBtn: 'Select Site',
        newLocation: function () {
            module.showModal('siteModal');
            module.dsBrowseSites.Reload();
            module.dsSelectSite.Reload();
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_site_group_Services.asmx/UnassignGroupSite",
                data: "{ 'site_group_id': '" + ID + "', 'site_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseSites.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseSites = new spaDataset({
        form: module,
        id: 'dsBrowseSites',
        pk: 'site_id',
        webservice: '/services/sys_site_group_Services.asmx/GetAssignedGroupSites',
        params: '"site_group_id": "' + ID + '"',
        pageSize: 30
    });

    
    // SELECT Sites
    module.mySelectSiteColumns = [{ label: 'Site Code', width: '50%', field: 'site_code', class: 'key' },
                                  { label: 'Site Name', width: '50%', field: 'name'}];

    module.mySelectSiteFilters = [{ name: 'mySiteCode2', field: 'site_code', label: 'Site Code', criteria: 'contains' },
                                  { name: 'mySiteName2', field: 'name', label: 'Site Name', criteria: 'contains'}];

    module.selectSiteGrid = new spaGrid({
        form: module,
        id: 'selectSiteGrid',
        title: "Select Sites",
        columns: module.mySelectSiteColumns,
        filters: module.mySelectSiteFilters,
        rowID: 'site_id',
        height: 210,
        onSelect: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_site_group_Services.asmx/AssignSiteToGroup",
                data: "{ 'site_group_id': '" + ID + "', 'site_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseSites.Reload();
                        module.dsSelectSite.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectSite = new spaDataset({
        form: module,
        id: 'dsSelectSite',
        pk: 'site_id',
        webservice: '/services/sys_site_group_Services.asmx/GetUnassignedGroupSites',
        params: '"site_group_id": "' + ID + '"',
        pageSize: 30
    });



    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.dsDetail.settings.params = '"site_group_id": "' + ID + '"';
                module.dsBrowseSites.settings.params = '"site_group_id": "' + ID + '"';
                module.dsBrowseSites.Reload();
                module.showDeleteBtn();
                module.showTab('sites');
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

}


