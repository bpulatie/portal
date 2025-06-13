function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('sites');
        module.hideTab('roles');
        module.hideTab('image');
    }

    module.aGenders = new Array(['f', 'Female'], ['m', 'Male']);
    module.addOptions('gender', module.aGenders);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_user',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetByID',
        params: '"user_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.dsDetail.EditColumn("client_id", CONTEXT.client.client_id);
                module.dsDetail.EditColumn("user_type", "u");
             };

            var myPreview = '<img src="' + ROOT + '/images/default_avatar_male.jpg" alt="Your Avatar" style="width:160px"><h6 class="text-muted">Click to select</h6>';
            if (module.dsDetail.GetCurrentRow().image_id != null) {
                myPreview = '<img src="' + ROOT + '/handlers/GetImage.ashx?id=' + module.dsDetail.GetCurrentRow().image_id + '" alt="Your Avatar" style="width:160px"><h6 class="text-muted">Click to update</h6>';
            }

            $("#avatar-2").fileinput({
                uploadUrl: ROOT + "/services/image_Services.asmx/SaveFile",
                uploadExtraData: function () {
                    return { id: module.dsDetail.GetCurrentRow().image_id, user_id: ID };
                },
                uploadAsync: true,
                overwriteInitial: true,
                maxFileSize: 1500,
                showClose: false,
                showCaption: false,
                showBrowse: false,
                browseOnZoneClick: true,
                removeLabel: '',
                removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
                removeTitle: 'Cancel or reset changes',
                elErrorContainer: '#kv-avatar-errors-2',
                msgErrorClass: 'alert alert-block alert-danger',
                defaultPreviewContent: myPreview,
                layoutTemplates: { main2: '{preview} {remove} {browse}' },
                allowedFileExtensions: ["jpg", "png", "gif"]
            });

            $('#avatar-2').on('fileuploaded', function (event, data, previewId, index) {
                if (data.response.result != true) {
                    ShowSystemModal('Application Error', 'Message: ' + data.response.message);
                } else {
                    module.dsDetail.EditColumn("image_id", data.response.data);
                }
            });

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
                url: ROOT + "/services/sys_user_Services.asmx/UnassignSiteFromUser",
                data: "{ 'user_id': '" + ID + "', 'site_id': '" + id + "' }",
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
        webservice: '/services/sys_site_Services.asmx/GetAllByUserAssigned',
        params: '"user_id": "' + ID + '"',
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
                url: ROOT + "/services/sys_user_Services.asmx/AssignSiteToUser",
                data: "{ 'user_id': '" + ID + "', 'site_id': '" + id + "' }",
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
        webservice: '/services/sys_site_Services.asmx/GetAllByUserUnassigned',
        params: '"user_id": "' + ID + '"',
        pageSize: 30
    });


    // Browse Roles
    module.myRoleColumns = [{ label: 'Role Name', width: '35%', field: 'role_name' },
                            { label: 'External Name', width: '60%', field: 'external_name'}];
                           // { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myRoleFilters = [{ name: 'myRoleName', field: 'role_name', label: 'Role Name', criteria: 'contains' },
                            { name: 'myExternalName', field: 'external_name', label: 'External Name', criteria: 'contains'}];

    module.roleGrid = new spaGrid({
        form: module,
        id: 'roleGrid',
        title: "Assigned Roles",
        columns: module.myRoleColumns,
        filters: module.myRoleFilters,
        rowID: 'role_id',
        height: 210,
        newBtn: 'Assign Role',
        newLocation: function () {
            module.showModal('roleModal');
            module.dsBrowseRoles.Reload();
            module.dsSelectRole.Reload();
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_user_Services.asmx/UnassignRoleFromUser",
                data: "{ 'user_id': '" + ID + "', 'role_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseRoles.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseRoles = new spaDataset({
        form: module,
        id: 'dsBrowseRoles',
        pk: 'role_id',
        webservice: '/services/sys_role_Services.asmx/GetAllByUserAssigned',
        params: '"user_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Roles
    module.mySelectRoleColumns = [{ label: 'Role Name', width: '50%', field: 'role_name', class: 'key' },
                                  { label: 'External Name', width: '50%', field: 'external_name' }];

    module.mySelectRoleFilters = [{ name: 'myRoleName2', field: 'role_name', label: 'Role Name', criteria: 'contains' },
                                  { name: 'myExternalName2', field: 'external_name', label: 'External Name', criteria: 'contains'}];

    module.selectRoleGrid = new spaGrid({
        form: module,
        id: 'selectRoleGrid',
        title: "Select Roles",
        columns: module.mySelectRoleColumns,
        filters: module.mySelectRoleFilters,
        rowID: 'role_id',
        height: 210,
        onSelect: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_user_Services.asmx/AssignRoleToUser",
                data: "{ 'user_id': '" + ID + "', 'role_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseRoles.Reload();
                        module.dsSelectRole.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectRole = new spaDataset({
        form: module,
        id: 'dsSelectRole',
        pk: 'role_id',
        webservice: '/services/sys_role_Services.asmx/GetAllByUserUnassigned',
        params: '"client_id": "' + CONTEXT.client.client_id + '", "user_id": "' + ID + '"',
        pageSize: 30
    });

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.dsBrowseSites.settings.params = '"user_id": "' + ID + '"';
                module.dsBrowseRoles.settings.params = '"user_id": "' + ID + '"';
                module.showDeleteBtn();
                module.showTab('sites');
                module.showTab('roles');
                module.showTab('image');
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


/*

    $('#file_upload1').fileinput({
        uploadUrl: 'handlers/saveimage.ashx',
        allowedFileExtensions: ['jpg', 'png', 'gif']
    });

   // $('#file_upload1').bootstrapFileInput();

    $('#file_upload1').change(function () {
        $("#file_id").val(module.dsDetail.GetCurrentRow().image_id);
 //       $("#imageForm").submit();
        setTimeout(function () {
            var timestamp = new Date().getTime();
            $("#imageHolder").html("<img src='handlers/getimage.ashx?id=" + module.dsDetail.GetCurrentRow().image_id + "&timestamp=" + timestamp + "' />");
        }, 1500);

    });
*/
}


