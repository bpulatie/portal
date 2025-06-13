function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }


    module.aNotify = new Array();
    module.aNotify.push(['e', 'Email'], ['s', 'Scheduled Email']);
    module.addOptions('notification_type', module.aNotify);

    module.dsCategory = new spaDataset({
        form: module,
        id: 'dsCategory',
        table: 'sys_event_category',
        pk: 'event_category_id_id',
        webservice: '/services/sys_event_Services.asmx/GetAllCategories',
        onLoad: function () {
            module.aCategory = new Array();

            for (var x = 0; x < module.dsCategory.RowCount(); x++) {
                var row = module.dsCategory.GetRow(x);
                module.aCategory.push([row.event_category_id, row.event_category_name]);
            }

            module.addOptions('event_category_id', module.aCategory);
        }
    });

    module.dsType = new spaDataset({
        form: module,
        id: 'dsType',
        table: 'sys_event_type',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetAllEventsByCategory',
        params: '"event_category_id": "' + EMPTY_GUID + '"',
        onLoad: function () {
            module.aType = new Array();

            for (var x = 0; x < module.dsType.RowCount(); x++) {
                var row = module.dsType.GetRow(x);
                module.aType.push([row.event_type_id, row.event_name]);
            }

            module.addOptions('event_type_id', module.aType);
        }
    });

    module.dsName = new spaDataset({
        form: module,
        id: 'dsName',
        table: 'sys_event_type',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetByEventID',
        params: '"event_type_id": "' + EMPTY_GUID + '"',
        onLoad: function () {
            if (ID != EMPTY_GUID) {
                module.setValue("event_type_id", module.dsDetail.GetCurrentRow().event_type_id);
            }
        }
    });

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_event_subscription',
        pk: 'event_subscription_id',
        webservice: '/services/sys_event_Services.asmx/GetBySubscriptionID',
        params: '"event_subscription_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.setValue("email", CONTEXT.user.email);
                module.setValue("notification_type", 'e');
                module.dsDetail.EditColumn("user_id", CONTEXT.user.user_id);
                module.dsDetail.EditColumn("notification_type", "e");
                module.dsDetail.EditColumn("email", CONTEXT.user.email);
            } else {
                module.dsType.settings.params = '"event_category_id": "' + module.dsDetail.GetCurrentRow().event_category_id + '"';
                module.dsType.Reload();
                module.dsName.settings.params = '"event_type_id": "' + module.dsDetail.GetCurrentRow().event_type_id + '"';
                module.dsName.Reload();
            }
        }
    });

    module.event_category_id_change = function (id) {
        module.dsType.settings.params = '"event_category_id": "' + id + '"';
        module.dsType.Reload();
    }

    module.event_type_id_change = function (id) {
        module.dsName.settings.params = '"event_type_id": "' + id + '"';
        module.dsName.Reload();
        module.dsDetail.EditColumn("event_type_id", id);
    }

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.showDeleteBtn();
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


