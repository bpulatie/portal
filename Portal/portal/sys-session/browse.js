function init(module, params) {

    module.myColumns = [{ label: 'Last Name', width: '20%', field: 'last_name', class: 'key', link: '/portal/sys-session/detail.htm', menu: 'Session: %%'},
                        { label: 'First Name', width: '20%', field: 'first_name' },
                        { label: 'Session Start', width: '15%', field: 'start_time', type: 'datetime', format: 'datatime' },
                        { label: 'Last Activity', width: '15%', field: 'last_activity_time', type: 'datetime', format: 'datatime' },
                        { label: 'Session End', width: '15%', field: 'end_time', type: 'datetime', format: 'datatime' },
                        { label: 'Session Status', width: '15%', field: 'status_name'}];

    module.myFilters = [{ name: 'myLastName', field: 'last_name', label: 'Last Name', criteria: 'contains'},
                        { name: 'myFirstName', field: 'first_name', label: 'First Name', criteria: 'contains'},
                        { name: 'myStartTime', field: 'start_time', label: 'Date Before', type: 'date', format: 'date', criteria: 'lessthan'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Session Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'session_id',
        height: 430,
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'session_id',
        webservice: '/services/sys_session_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}





