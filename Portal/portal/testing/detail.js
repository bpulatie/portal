function init(module, params) {

    var ID = CONTEXT.user.user_id;

    module.dsUser = new spaDataset({
        id: 'dsUser',
        table: 'sys_user',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetProfileUser'
    });


    module.saveBtn_onclick = function() {
        if (module.validateForm() == true) {
           // module.dsUser.Save(function () {
           //     DisplayMessage(module, 'Save Successful');
           // });
        };
    }


    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }

    //Events
    module.myEventColumns = [{ label: 'Event Category', width: '45%', field: 'event_category_name'},
                             { label: 'Event', width: '50%', field: 'event_name' }];

    module.myEventFilters = [{ name: 'myEventCategory', field: 'event_category_name', label: 'Event Category', criteria: 'contains' },
                             { name: 'myEventName', field: 'event_name', label: 'Event Name', criteria: 'contains'}];

    module.eventGrid = new spaGrid({
        form: module,
        id: 'eventGrid',
        title: "Events",
        columns: module.myEventColumns,
        filters: module.myEventFilters,
        rowID: 'event_type_id',
        height: 270,
        newBtn: 'Subscribe To Event',
        newLocation: function () {
            module.showModal("eventModal");
            //createModule("Event Detail", "/portal/sys-event-category/event-detail.htm", { 'event_category_id': ID, 'id': EMPTY_GUID }, 0);
        }

    });

    module.dsBrowseEvents = new spaDataset({
        form: module,
        id: 'dsBrowseEvents',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetAllSubscribedEvents',
        params: '"user_id": "' + ID + '"',
        pageSize: 30
    });


    var states = [{ 'id': 'AL', 'name': 'Alabama' },
                      { 'id': 'AK', 'name': 'Alaska' },
                      { 'id': 'AZ', 'name': 'Arizona' },
                      { 'id': 'AR', 'name': 'Arkansas' },
                      { 'id': 'CA', 'name': 'California' },
                      { 'id': 'CO', 'name': 'Colorado' },
                      { 'id': 'CT', 'name': 'Connecticut' },
                      { 'id': 'DE', 'name': 'Delaware' },
                      { 'id': 'FL', 'name': 'Florida' },
                      { 'id': 'GA', 'name': 'Georgia' },
                      { 'id': 'HI', 'name': 'Hawaii' },
                      { 'id': 'ID', 'name': 'Idaho' },
                      { 'id': 'IL', 'name': 'Illinois' },
                      { 'id': 'IN', 'name': 'Indiana' },
                      { 'id': 'IA', 'name': 'Iowa' },
                      { 'id': 'KS', 'name': 'Kansas' },
                      { 'id': 'KY', 'name': 'Kentucky' },
                      { 'id': 'LA', 'name': 'Louisiana' },
                      { 'id': 'ME', 'name': 'Maine' },
                      { 'id': 'MD', 'name': 'Maryland' },
                      { 'id': 'MA', 'name': 'Massachusetts' },
                      { 'id': 'MI', 'name': 'Michigan' },
                      { 'id': 'MN', 'name': 'Minnesota' },
                      { 'id': 'MS', 'name': 'Mississippi' },
                      { 'id': 'MO', 'name': 'Missouri' },
                      { 'id': 'MT', 'name': 'Montana' },
                      { 'id': 'NE', 'name': 'Nebraska' },
                      { 'id': 'NV', 'name': 'Nevada' },
                      { 'id': 'NH', 'name': 'New Hampshire' },
                      { 'id': 'NJ', 'name': 'New Jersey' },
                      { 'id': 'NM', 'name': 'New Mexico' },
                      { 'id': 'NY', 'name': 'New York' },
                      { 'id': 'NC', 'name': 'North Carolina' },
                      { 'id': 'ND', 'name': 'North Dakota' },
                      { 'id': 'OH', 'name': 'Ohio' },
                      { 'id': 'OK', 'name': 'Oklahoma' },
                      { 'id': 'OR', 'name': 'Oregon' },
                      { 'id': 'PA', 'name': 'Pennsylvania' },
                      { 'id': 'RI', 'name': 'Rhode Island' },
                      { 'id': 'SC', 'name': 'South Carolina' },
                      { 'id': 'SD', 'name': 'South Dakota' },
                      { 'id': 'TN', 'name': 'Tennessee' },
                      { 'id': 'TX', 'name': 'Texas' },
                      { 'id': 'UT', 'name': 'Utah' },
                      { 'id': 'VT', 'name': 'Vermont' },
                      { 'id': 'VA', 'name': 'Virginia' },
                      { 'id': 'WA', 'name': 'Washington' },
                      { 'id': 'WV', 'name': 'West Virginia' },
                      { 'id': 'WI', 'name': 'Wisconsin' },
                      { 'id': 'WY', 'name': 'Wyoming'}];

    module.addAutoOptions('state', states);

}



