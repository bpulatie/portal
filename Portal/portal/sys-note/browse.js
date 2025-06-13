function init(module, params) {

    module.myColumns = [{ label: 'Client Name', width: '30%', field: 'name'},
                        { label: 'Status', width: '10%', field: 'status_name' },
                        { label: 'Date', width: '15%', field: 'creation_date', type: 'date', format: 'date'},
                        { label: 'Follow Up', width: '15%', field: 'follow_up_date', type: 'date', format: 'date' },
                        { label: 'Note', width: '30%', field: 'summary', class: 'key'}];

    module.myFilters = [{ name: 'myName', field: 'name', label: 'Client Name', criteria: 'contains' },
                        { name: 'myDate', field: 'creation_date', label: 'Date', criteria: 'greaterthan', type: 'date', format: 'date' },
                        { name: 'myNote', field: 'summary', label: 'Note', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Note Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'client_note_id',
        height: 430,
        newBtn: 'Add New Note',
        newLocation: '/portal/sys-note/detail.htm',
        newBtn: 'Add New Note',
        newLocation: function (id) {
            createModule("Note Detail", "/portal/sys-note/Detail.htm", { 'id': EMPTY_GUID }, 0);
        },
        onLoad: function () {
        },
        onSelect: function (id) {
            createModule("Note Detail", "/portal/sys-note/Detail.htm", { 'id': id }, 0);
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'client_note_id',
        webservice: '/services/sys_client_Services.asmx/GetAllNotes',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}

