function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('features'); 
        module.hideTab('users');
    }

    module.aStatus = new Array(['o', 'Open'],
                               ['p', 'Prospect'],
                               ['s', 'Suspended'],
                               ['c', 'Closed']);

    module.addOptions('status_code', module.aStatus);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_client',
        pk: 'client_id',
        webservice: '/services/sys_client_Services.asmx/GetByID',
        params: '"client_id": "' + ID + '"',
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
                module.dsDetail.settings.params = '"client_id": "' + ID + '"';
                module.dsBrowseFeatures.settings.params = '"client_id": "' + ID + '"';
                module.dsBrowseFeatures.Reload();
                module.dsBrowseUsers.settings.params = '"client_id": "' + ID + '"';
                module.dsBrowseUsers.Reload();
                module.showDeleteBtn();
                module.showTab('features');
                module.showTab('users');
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

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }


    // Browse Features
    module.myFeatureColumns = [{ label: 'Application', width: '45%', field: 'application_name' },
                               { label: 'Feature', width: '50%', field: 'feature_name' },
                               { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myFeatureFilters = [{ name: 'myFeatureName', field: 'feature_name', label: 'Feature Name', criteria: 'contains'}];

    module.featureGrid = new spaGrid({
        form: module,
        id: 'featureGrid',
        title: "Assigned Features",
        columns: module.myFeatureColumns,
        filters: module.myFeatureFilters,
        rowID: 'feature_id',
        height: 270,
        newBtn: 'Assign Feature',
        newLocation: function () {
            module.dsSelectFeature.Reload();
            module.showModal('featureModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_feature_Services.asmx/UnassignFeatureFromClient",
                data: "{ 'client_id': '" + ID + "', 'feature_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseFeatures.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseFeatures = new spaDataset({
        form: module,
        id: 'dsBrowseFeatures',
        pk: 'client_feature_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllAssignedClientFeatures',
        params: '"client_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Feature
    module.aApps = new Array(); ;

    module.dsApplication = new spaDataset({
        form: module,
        id: 'dsApplication',
        table: 'sys_application',
        pk: 'application_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllApplications',
        onLoad: function () {

            module.aApps.push(['', 'Show All']);

            for (var x = 0; x < module.dsApplication.RowCount(); x++) {
                var row = module.dsApplication.GetRow(x);
                module.aApps.push([row.application_id, row.application_name]);
            }

            module.mySelectFeatureColumns = [{ label: 'Application', width: '50%', field: 'application_name' },
                                             { label: 'Feature', width: '50%', field: 'feature_name', class: 'key'}];

            module.mySelectFeatureFilters = [{ name: 'myGroup', field: 'f.application_id', label: 'Application', type: 'array', values: module.aApps, criteria: 'equals' },
                                             { name: 'myName', field: 'feature_name', label: 'Feature', criteria: 'contains'}];

            module.selectFeatureGrid = new spaGrid({
                form: module,
                id: 'selectFeatureGrid',
                title: "Select Feature",
                columns: module.mySelectFeatureColumns,
                filters: module.mySelectFeatureFilters,
                rowID: 'feature_id',
                height: 350,
                onSelect: function (id) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: ROOT + "/services/sys_feature_Services.asmx/AssignClientFeature",
                        data: "{ 'client_id': '" + ID + "', 'feature_id': '" + id + "' }",
                        dataType: "json",
                        success: function (myResponse) {
                            if (myResponse.result == true) {
                                module.dsBrowseFeatures.Reload();
                                module.hideModal('featureModal');
                            } else {
                                ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                            }
                        },
                        error: ajaxFailed
                    });
                }
            });

            module.dsSelectFeature = new spaDataset({
                form: module,
                id: 'dsSelectFeature',
                pk: 'feature_id',
                webservice: '/services/sys_feature_Services.asmx/GetAllUnassignedClientFeatures',
                params: '"client_id": "' + ID + '"',
                pageSize: 30
            });
        }
    });

    // Users
    module.myUserColumns = [{ label: 'Last Name', width: '20%', field: 'last_name', class: 'key' },
                            { label: 'First Name', width: '20%', field: 'first_name' },
                            { label: 'User Type', width: '20%', field: 'user_type_name' },
                            { label: 'Login Name', width: '20%', field: 'login_name' },
                            { label: 'Email', width: '20%', field: 'email'}];

    module.myUserFilters = [{ name: 'myUserlast', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                            { name: 'myUserfirst', field: 'first_name', label: 'First Name', criteria: 'contains'}];

    module.userGrid = new spaGrid({
        form: module,
        id: 'userGrid',
        title: "User Browse",
        columns: module.myUserColumns,
        filters: module.myUserFilters,
        rowID: 'user_id',
        height: 270,
        newBtn: 'Add New User',
        newLocation: function (id) {
            createModule("User Detail", "/portal/sys-client/userDetail.htm", { 'client_id': ID, 'id': EMPTY_GUID }, 0);
        },
        onLoad: function () {
        },
        onSelect: function (id) {
            createModule("User Detail", "/portal/sys-client/userDetail.htm", { 'client_id': ID, 'id': id }, 0);
        }

    });

    module.dsBrowseUsers = new spaDataset({
        form: module,
        id: 'dsBrowseUsers',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetClientUsers',
        params: '"client_id": "' + ID + '"',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowseUsers.Reload();
    };

    // Notes
    module.myNoteColumns = [{ label: 'Date', width: '20%', field: 'creation_date', type: 'date', format: 'date' },
                            { label: 'Follow Up', width: '20%', field: 'follow_up_date', type: 'date', format: 'date' },
                            { label: 'Note', width: '60%', field: 'summary', class: 'key'}];

    module.myNoteFilters = [{ name: 'myDate', field: 'creation_date', label: 'Date', criteria: 'greaterthan', type: 'date', format: 'date' },
                            { name: 'myNote', field: 'summary', label: 'Note', criteria: 'contains'}];

    module.noteGrid = new spaGrid({
        form: module,
        id: 'noteGrid',
        title: "Notes",
        columns: module.myNoteColumns,
        filters: module.myNoteFilters,
        rowID: 'client_note_id',
        height: 270,
        newBtn: 'Add New Note',
        newLocation: function (id) {
            createModule("Note Detail", "/portal/sys-client/noteDetail.htm", { 'client_id': ID, 'id': EMPTY_GUID }, 0);
        },
        onLoad: function () {
        },
        onSelect: function (id) {
            createModule("Note Detail", "/portal/sys-client/noteDetail.htm", { 'client_id': ID, 'id': id }, 0);
        }

    });

    module.dsBrowseNotes = new spaDataset({
        form: module,
        id: 'dsBrowseNotes',
        pk: 'client_note_id',
        webservice: '/services/sys_client_Services.asmx/GetAllNotesByClient',
        params: '"client_id": "' + ID + '"',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowseNotes.Reload();
    };
}




