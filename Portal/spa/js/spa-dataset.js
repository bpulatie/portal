function spaDataset(options) {
    
    this.defaultOptions = {
        form: null,
        id: null,
        table: null,
        pk: null,
        webservice: null,
        params: null,
        filter: null,
        lazyload: false,
        paged: true,
        pageSize: 25,
        scroll: false,
        grid: null,
        onLoad: null,
        onSave: null
    };

    this.settings = $.extend({}, this.defaultOptions, options); /// <reference path="spa-dataset.js" />

    this.dataset = null;
    this.pageNo = 1;
    this.changes = [];
    this.moduleID = CURRENT_MODULE_ID;

    // Private Methods
    this._getData = function () {
        var base = this;

        var params = "";
        if (base.settings.params != null) {
            params = base.settings.params + ", ";
        }

        var filter = "";
        if (base.settings.filter != null) {
            filter += "'filter': " + base.settings.filter;
        } else {
            filter += "'filter': []";
        }

        var paging = "";
        if (base.settings.paged == true) {
            paging = ", 'pageNo': '" + this.pageNo + "', 'rows': '" + this.settings.pageSize + "'";
        }

        var myParams = "{ " + params + filter + paging + " }";

        cl.show(); // $('.spa-loading-image').show();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + base.settings.webservice,
            data: myParams,
            dataType: "json",
            success: function (myResponse) {
                cl.hide(); // $('.spa-loading-image').hide();
                if (myResponse.result == true) {
                    if (base.settings.scroll != true) {
                        base.dataset = jQuery.parseJSON(myResponse.data);
                    } else {
                        // Add data rows to existing data - pass start row??
                        var myData = jQuery.parseJSON(myResponse.data);
                        if (base.dataset === null) {
                            base.dataset = myData;
                        } else {
                            for (var row in myData.Rows) {
                                base.dataset.Rows.push(row);
                            }

                        }
                    }

                    base._processData();

                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    },

    this._processData = function () {
        var base = this;

        base._processMultiRowControls();
        base._processSingleRowControls();

        if (typeof base.settings.onLoad === 'function')
            base.settings.onLoad();
    }

    this._processMultiRowControls = function () {
        var base = this;

        // Update controls associated with this dataset with data 

        // Multi Row controls - Grid, TreeGrid and combo box
        $('div[spaGrid][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            var form = base.settings.form;
            var cmd = "form." + $(this).attr('spaGrid') + ".DisplayGridData(base.dataset, base.settings.scroll)";
            eval(cmd);
        });

        $('div[spaTreeGrid][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            var form = base.settings.form;
            var cmd = "form." + $(this).attr('spaTreeGrid') + ".DisplayTreeGridData(base.dataset, base.settings.scroll)";
            eval(cmd);
        });

        $('div[spaSelect][lookup_ds="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').addSelectOptions(base.settings.form, $(this).attr("spaSelect"), base.dataset.Rows);
        });

    }

    // Maybe implement a start row for scrolling datasets
    this._processSingleRowControls = function () {
        var base = this;

        if (base.dataset.CurrentRow < 0) {
            return;
        }

        // Single Row controls - use first row
        $('div[spaSelect][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setSelectValue(base.moduleID, $(this).attr("spaSelect"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaAuto][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setAutoValue(base.moduleID, $(this).attr("spaAuto"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaInput][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setInputValue(base.moduleID, $(this).attr("spaInput"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaDate][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setDateValue(base.moduleID, $(this).attr("spaDate"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaTime][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setTimeValue(base.moduleID, $(this).attr("spaTime"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaCheck][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setCheckValue(base.moduleID, $(this).attr("spaCheck"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaText][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setTextValue(base.moduleID, $(this).attr("spaText"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaBlob][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setBlobValue(base.moduleID, $(this).attr("spaBlob"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("column")]);
        });

        $('div[spaLookup][dataset="' + base.settings.id + '"]', '#' + base.moduleID).each(function () {
            $('body').data('plugin_spaControls').setLookupValue(base.moduleID, $(this).attr("spaLookup"), base.dataset.Rows[base.dataset.CurrentRow][$(this).attr("lookup_col")]);
        });
    }

    if (this.settings.lazyload == false) {
        this._getData();
    }

    //Public methods
    spaDataset.prototype.Reload = function (filters) {
        if (filters != null) {
            this.settings.filter = filters;
            this.pageNo = 1;
        }

        //this.PageNo = 1;
        this._getData();
    }

    spaDataset.prototype.Page = function (page) {
        if ((page > 0) && (page < (this.dataset.PageCount + 1))) {
            this.pageNo = page;
            this._getData();
        }
    }

    spaDataset.prototype.NextPage = function () {
        if (this.pageNo < this.dataset.PageCount) {
            this.pageNo++;
            this._getData();
        }
    }

    spaDataset.prototype.PreviousPage = function () {
        if (this.pageNo > 1) {
            this.pageNo--;
            this._getData();
        }
    }

    spaDataset.prototype.Save = function (success) {
        var base = this;

        if (base.changes.length == 0) {
            var mod = GetModuleRef(base.moduleID);
            DisplayMessage(mod, 'No changes Pending!');
            return;
        }

        $.ajax({
            async: false,
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/dataset_Services.asmx/GenericUpdate",
            data: "{ 'Updates': " + JSON.stringify(base.changes) + "}",
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == false) {
                    ShowSystemModal('Application Error', myResponse.message);
                }
                else {
                    base.changes = [];
                    base.settings.params = '"' + base.settings.pk + '": "' + myResponse.data + '"';
                    base.dataset.Rows[base.dataset.CurrentRow][base.settings.pk] = myResponse.data;
                    success(myResponse.data);
                }
            },
            error: ajaxFailed
        });
    }

    spaDataset.prototype.AddRow = function () {
        var Action = "Add";
        var TableName = this.settings.table;
        var KeyName = this.settings.pk;
        var KeyValue = "00000000-0000-0000-0000-000000000000";

        this.dataset.Rows.push(this.dataset.EmptyRow);
        this.dataset.CurrentRow = this.dataset.Rows.length - 1;
        this.dataset.Rows[this.dataset.CurrentRow][KeyName] = KeyValue;
        var RowNo = this.dataset.CurrentRow;
        this.changes.push({ "TableName": TableName, "KeyName": KeyName, "KeyValue": KeyValue, "RowNo": RowNo, "Action": Action, "Columns": [] });
        this._processSingleRowControls();
    }

    spaDataset.prototype.EditColumn = function (column, value) {
        if (this.dataset.CurrentRow < 0) {
            ShowSystemModal("Application Error", "Invalid Edit - No current Row");
            return;
        };

        var Action = "Edit";
        var TableName = this.settings.table;
        var KeyName = this.settings.pk;
        var KeyValue = this.dataset.Rows[this.dataset.CurrentRow][KeyName];
        var RowNo = this.dataset.CurrentRow;

        this.dataset.Rows[this.dataset.CurrentRow][column] = value;

        var NewEntry = true;
        for (var x = 0; x < this.changes.length; x++) {
            if (this.changes[x].TableName == TableName &&
                this.changes[x].KeyName == KeyName &&
                this.changes[x].KeyValue == KeyValue &&
                this.changes[x].RowNo == this.dataset.CurrentRow) {
                this.changes[x].Columns.push({ "Column": column, "Value": value });
                NewEntry = false;
                break;
            };
        };

        if (NewEntry) {
            this.changes.push({ "TableName": TableName, "KeyName": KeyName, "KeyValue": KeyValue, "RowNo": RowNo, "Action": Action, "Columns": [{ "Column": column, "Value": value}] });
        };
    }

    spaDataset.prototype.DeleteRow = function () {
        if (this.dataset.CurrentRow < 0) {
            ShowSystemModal("Application Error", "Invalid Delete - No current Row");
            return;
        };

        var Action = "Delete";
        var TableName = this.settings.table;
        var KeyName = this.settings.pk;
        var KeyValue = this.dataset.Rows[this.dataset.CurrentRow][KeyName];
        var RowNo = this.dataset.CurrentRow;

        //ToDo Delete and update controls set row
        //this.dataset.Rows.push(this.dataset.EmptyRow);
        //this.dataset.CurrentRow = this.dataset.Rows - 1;
        //var RowNo = this.dataset.CurrentRow;
        this.changes = [];
        this.changes.push({ "TableName": TableName, "KeyName": KeyName, "KeyValue": KeyValue, "RowNo": RowNo, "Action": Action, "Columns": [] });
    }

    spaDataset.prototype.RowNo = function () {
        return this.dataset.CurrentRow;
    }

    spaDataset.prototype.RowCount = function () {
        return this.dataset.Rows.length;
    }

    spaDataset.prototype.GetRow = function (x) {
        if (this.dataset.Rows.length > 0) {
            this.dataset.CurrentRow = x;
            return this.dataset.Rows[x];
        } else {
            ShowSystemModal("Application Error", "GetRow - Bad Row");
        }
    }

    spaDataset.prototype.GetCurrentRow = function () {
        if (this.dataset.Rows.length > 0) {
            return this.dataset.Rows[this.dataset.CurrentRow];
        } else {
            ShowSystemModal("Application Error", "GetCurrentRow - Bad Row");
        }
    }

    spaDataset.prototype.MoveFirst = function () {
        if (this.dataset.Rows.length > 0) {
            this.dataset.CurrentRow = 0;
            this._processSingleRowControls();
            return true;
        } else {
            return false;
        }
    }

    spaDataset.prototype.MoveNext = function () {
        if (this.dataset.CurrentRow + 1 < this.dataset.Rows.length) {
            this.dataset.CurrentRow++;
            this._processSingleRowControls();
            return true;
        } else {
            return false;
        }
    }

    spaDataset.prototype.MoveTo = function (x) {
        if ((x < 0) || (x > this.dataset.Rows.length - 1)) {
            ShowSystemModal("Application Error", "MoveTo - Invalid Row");
            return;
        }
        this.dataset.CurrentRow = x;
        this._processSingleRowControls();
    }

};

