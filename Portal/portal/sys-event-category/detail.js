function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab("events");
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_event_category',
        pk: 'event_category_id',
        webservice: '/services/sys_event_Services.asmx/GetByCategoryID',
        params: '"category_id": "' + ID + '"',
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
                module.dsBrowseEvents.settings.params = '"event_category_id": "' + ID + '"';
                module.dsBrowseEvents.Reload();
                module.showDeleteBtn();
                module.showTab("events");
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


    // Events
    module.myEventColumns = [{ label: 'Event Type', width: '100%', field: 'event_name', class: 'key', link: '/portal/sys-event-category/event-detail.htm', menu: 'Event: %%'}];
    module.myEventFilters = [{ name: 'myEventName', field: 'event_name', label: 'Event Type', criteria: 'contains'}];

    module.eventGrid = new spaGrid({
        form: module,
        id: 'eventGrid',
        title: "Events",
        columns: module.myEventColumns,
        filters: module.myEventFilters,
        rowID: 'event_type_id',
        height: 270,
        newBtn: 'Add Event Type',
        newLocation: function () {
            createModule("Event Detail", "/portal/sys-event-category/event-detail.htm", { 'event_category_id': ID, 'id': EMPTY_GUID }, 0);
        }
    });

    module.dsBrowseEvents = new spaDataset({
        form: module,
        id: 'dsBrowseEvents',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetAllEventsByCategory',
        params: '"event_category_id": "' + ID + '"',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowseEvents.Reload();
    };

}




