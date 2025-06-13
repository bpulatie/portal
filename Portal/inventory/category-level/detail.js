function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } 

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'inv_category_level',
        pk: 'category_level_id',
        webservice: '/services/inv_category_level_Services.asmx/GetByID',
        params: '"category_level_id": "' + ID + '"',
        onLoad: function () {
        } 
    });

    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function () {
                module.showDeleteBtn();
                DisplayMessage(module, 'Save Successful');
            });
        }
    }

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }

}




