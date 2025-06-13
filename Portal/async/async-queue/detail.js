function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } 

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'async_queue',
        pk: 'queue_id',
        webservice: '/services/async_queue_Services.asmx/GetByID',
        params: '"queue_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
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








