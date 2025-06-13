; (function ($, window, document, undefined) {

    // Create the defaults once
    var pluginName = "spaControls",
                                defaults = {
                                    form: null
                                };

    // The actual plugin constructor
    function Plugin(element, options) {
        this.element = element;
        this.settings = $.extend({}, defaults, options);
        this._defaults = defaults;
        this._name = pluginName;
        this.dataFormat = "default";
        this.module_id = CURRENT_MODULE_ID;

        this.init();
    }

    Plugin.prototype = {
        init: function () {
            base = this;

            // Header
            //****************************************************************************************** Render Text Input
            $('div[spaHeader]', '#' + base.module_id).each(function () {
                var mode = base.settings.form.settings.mode;
                var txt = $(this).attr('spaHeader');
                var sButtons = $(this).html();
                
                var sHTML = "<div class='row'>" +
                            "   <div class='col-sm-6'>" +
                            "       <h3>" + txt + "</h3>" +
                            "   </div>" +
                            "   <div class='col-sm-6'>" +
                            "       <div class='pull-right spa-top space-bottom'>" +
                            sButtons +
                            "           <span spaClass='btn-sm btn-primary' mode='all' spaButton='spaclose'>Close</span>" +
                            "       </div>" +
                            "   </div>" +
                            "</div>";

                $(this).html(sHTML);

            });


            // Footer
            //****************************************************************************************** Render Text Input
            $('div[spaFooter]', '#' + base.module_id).each(function () {
                var txt = $(this).attr('spaFooter');
                var sButtons = $(this).html();
                var id = base.module_id + '_statusline';

                var sHTML = "<div id=" + id + " spaStatusLine class='well text-danger'>&nbsp;</div>";

                $(this).html(sHTML);

            });


            // Modal
            //****************************************************************************************** Render Text Input
            $('div[spaModal]', '#' + base.module_id).each(function () {
                var id = base.module_id + '_modal_' + $(this).attr('spaModal');
                $(this).attr('id', id); 
                $(this).addClass('modal');
                $(this).addClass('fade');
            });


            // Panel
            //****************************************************************************************** Render Text Input
            $('div[spaPanel]', '#' + base.module_id).each(function () {
                var panel = this;
                var id = base.module_id + '_panel_' + $(this).attr('spaPanel');
                var panelName = $(this).attr('spaPanel');
                var panelTitle = $(this).attr('title');
                var panelContent = $(this).html();

                var sHTML =  "<div class='panel panel-info spapanel'>";
                    sHTML += "    <div class='panel-heading'>";
                    sHTML += "        <h3 id='" + id + "' class='panel-title'>" + panelTitle + "</h3>";
                    sHTML += "    </div>";
                    sHTML += "    <br />";
                    sHTML += "    <div class='panel-body'>";
                    sHTML += "        <form class='form-horizontal' role='form'>";
                    sHTML += panelContent;
                    sHTML += "        </form>";
                    sHTML += "    </div>";
                    sHTML += "</div>";
                
                $(panel).html(sHTML);

            });

            // Tabs
            //****************************************************************************************** Render Text Input
            $('div[spaTabPanel]', '#' + base.module_id).each(function () {
                var tab = this;
                var bFirst = true;

                // Tabs
                var sHTML = "<ul class='nav nav-tabs' role='tablist'>";

                $('div[spaTab]', $(tab)).each( function() {
                    var tabName = $(this).attr('spaTab');
                    var tabLabel = $(this).attr('label');
                    if (bFirst) {
                        bFirst = false;
                        sHTML += "    <li id='" + base.module_id + "_tab_" + tabName + "' role='presentation' class='active'>" +
                                 "        <a href='#tab_" + base.module_id + "_" + tabName + "' aria-controls='tab_" + base.module_id + "_" + tabName + "' role='tab' data-toggle='tab'>" + tabLabel + "</a>" +
                                 "    </li>";
                    } else {
                        sHTML += "    <li id='" + base.module_id + "_tab_" + tabName + "' role='presentation'>" +
                                 "        <a href='#tab_" + base.module_id + "_" + tabName + "' aria-controls='tab_" + base.module_id + "_" + tabName + "' role='tab' data-toggle='tab'>" + tabLabel + "</a>" +
                                 "    </li>";
                    }
                });

                sHTML += "</ul>";

                sHTML += "        <div class='form-group'>" +
                            "            <div class='panel-body'>" +
                            "                <form class='form-horizontal' role='form'>" +
                            "                    <div class='tab-content'>";

                bFirst = true;

                $('div[spaTab]', $(tab)).each( function() {
                    var tabName = $(this).attr('spaTab');
                    var tabContent = $(this).html();

                    if (bFirst) {
                        bFirst = false;
                        sHTML += "                        <div role='tabpanel' class='tab-pane active' id='tab_" + base.module_id + "_" + tabName + "'>" +
                                 tabContent +
                                 "                        </div>";
                    } else {
                        sHTML += "                        <div role='tabpanel' class='tab-pane' id='tab_" + base.module_id + "_" + tabName + "'>" +
                                 tabContent +
                                 "                        </div>";
                    }
                });

                sHTML += "                       </div>" +                            
                         "                   </form>" +
                         "              </div>" +
                         "          </div>";

                $(tab).html(sHTML);

            });


            // Buttons
            //****************************************************************************************** Render Text Input
            $('span[spaButton]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var btn = $(this).attr('spaButton');
                var id = base.module_id + '_' + btn;
                var cls = $(this).attr('spaClass');
                var af = $(this).attr('accessFeature');
                var msg = $(this).attr('spaConfirm');
                var btnMode = $(this).attr('mode');
                var name = $(this).html();
                var temp = id.substr(10);
                var i = temp.substr(0, temp.indexOf("_"));

                if (cls === undefined) {
                    cls = '';
                }

                if (msg === undefined) {
                    if (btn == 'deleteBtn') {
                        msg = "Are you sure?";
                    }
                }

                if (af !== undefined) {
                    if (hasAccessFeature(af) == false) {
                        $(this).html('');
                        return;
                    }
                }
                                      
                if (mode === 'view') {
                    if (btnMode != 'all') {
                        $(this).html('');
                        return;
                    }
                } 
                
                var sHTML = "<button id='" + id + "' class='btn " + cls + "' type='button'>" + name + "</button>";

                //$(this).addClass('form-group');
                $(this).html(sHTML);

                $("#" + id).click(function () {
                    var funcName = "";
                    if (id == MODULES[i].module_id + '_spaclose') {
                        funcName = "MODULES[" + i + "].closeModule";
                    } else {                               
                        funcName = "MODULES[" + i + "]." + btn + "_onclick";
                    }
                    spaConfirmationModal(msg, funcName, MODULES[i]);
                    return;
                });
            });


            // Form load render controls
            //****************************************************************************************** Render Text Input
            $('div[spaInput]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + "_" + $(this).attr('spaInput');
                var label = $(this).attr('label');
                var type = 'text';
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var dataFormat = $(this).attr('dataFormat');
                var readOnly = $(this).attr('readonly');

                if (mode === "view") {
                    readOnly = "readonly";
                } 

                if ($(this).attr('type') == 'password') {
                    type = 'password';
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                                "<div class='col-sm-8'><input id='" + id + "' class='form-control spaInput' autocomplete='off' type='" + type + "' " + readOnly + " placeholder='" + label + "'/></div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                if (dataFormat == "numeric") {
                    $('#' + id).keypress(function (event) {
                        //var controlKeys = [8, 9, 13, 35, 36, 37, 39, 110, 190];
                        var controlKeys = [8, 9, 13, 46];
                        var isControlKey = controlKeys.join(",").match(new RegExp(event.which));
                        if (!event.which || // Control keys in most browsers. e.g. Firefox tab is 0
                            (49 <= event.which && event.which <= 57) || // Always 1 through 9
                            (48 == event.which && $(this).attr("value")) || // No 0 first digit
                            isControlKey) { // Opera assigns values for control keys.
                            return;
                        } else {
                            event.preventDefault();
                        }
                    });
                }

                if (ds != null && readOnly != "readOnly") {
                    $('#' + id).change(function () {
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, $(this).val());
                    });
                }

            });


            // Form load render controls
            //****************************************************************************************** Render Text Input
            $('div[spaDate]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaDate');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');

                if (mode === "view") {
                    readOnly = "readonly";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                            "<div class='col-sm-8'>" +
                                "<div class='input-group date spaDate' id='" + id + "'>" +
                                    "<input type='text' class='form-control' " + readOnly + " placeholder='" + label + "'/>" +
                                    "<span class='input-group-addon'>" +
                                        "<i class='fa fa-calendar' aria-hidden='true'></i>" +
                                    "</span>" +
                                "</div>" +
                            "</div>";


                $(this).addClass('form-group');
                $(this).html(sHTML);

                $('#' + id).datetimepicker({
                    format: 'MM/DD/YYYY',
                    showClose: true
                });

                if (ds != null && readOnly != "readOnly") {
                    $('#' + id).on("dp.change", function (e) {
                        var dte = e.date.format();
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, dte);
                    });
                }

            });


            // Form load render controls
            //****************************************************************************************** Render Text Input
            $('div[spaTime]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaTime');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');

                if (mode === "view") {
                    readOnly = "readonly";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                            "<div class='col-sm-8'>" +
                                "<div class='input-group date spaTime' id='" + id + "'>" +
                                    "<input type='text' class='form-control' " + readOnly + " placeholder='" + label + "'/>" +
                                    "<span class='input-group-addon'>" +
                                        "<i class='fa fa-clock-o' aria-hidden='true'></i>" +
                                    "</span>" +
                                "</div>" +
                            "</div>";
                                
                                
                $(this).addClass('form-group');
                $(this).html(sHTML);

                $('#' + id).datetimepicker({
                                            format: 'LT',
                                            showClose: true
                                            });
                
                if (ds != null && readOnly != "readOnly") {
                    $('#' + id).on("dp.change", function (e) {
                        var dte = e.date.format();
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, dte);
                    });
                }
                
            });


            //****************************************************************************************** Render Checkbox
            $('div[spaCheck]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaCheck');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');

                if (mode === "view") {
                    readOnly = "readonly";
                }

                var sHTML = "<div class='col-sm-offset-4 col-sm-8'>" + 
                                "<div class='checkbox'>" +
                                    "<label>" +
                                        "<input id='" + id + "' class='spaCheck' " + readOnly + " type='checkbox'>" + label +
                                    "</label>" +
                                "</div>" +
                            "</div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                if (ds != null && readOnly != "readOnly") {
                    $('#' + id).change(function () {
                        var dset = GetDatasetRef(this.id, ds);
                        if ($(this).is(':checked') == true) {
                            dset.EditColumn(col, 'y');
                        } else {
                            dset.EditColumn(col, 'n');
                        }
                    });
                }
            });

            //****************************************************************************************** Render Memo Field
            $('div[spaText]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaText');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');
                var rows = $(this).attr('rows');

                if (rows == undefined) {
                    rows = "5";
                }

                if (mode === "view") {
                    readOnly = "readonly";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                                "<div class='col-sm-8'><textarea id='" + id + "' " + readOnly + " class='form-control spaText' rows='" + rows + "' placeholder='" + label + "'/></div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                if (ds != null && readOnly != "readOnly") {
                    $('#' + id).change(function () {
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, $(this).val());
                    });
                }
            });

        //****************************************************************************************** Render Memo Field
            $('div[spaBlob]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaBlob');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');

                if (mode === "view") {
                    readOnly = "readonly";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                                "<div class='col-sm-8'><div id='" + id + "' " + readOnly + " class='spaBlob panel panel-default'/></div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

            });

            //****************************************************************************************** Render Combobox
            $('div[spaSelect]', '#' + base.module_id).each(function () {

                var mode = base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaSelect');
                var col_id = $(this).attr('spaSelect');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');
             
                var disabled = "";
                if (mode === "view") {
                    disabled = "disabled";
                }

                if (readOnly === "readonly") {
                    disabled = "disabled";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                            "<div class='col-sm-8'>" +
                                "<input id='" + id + "_hidden' type='hidden' />" +
                                "<div class='input-group'>" +
                                    "<input id='" + id + "' type='text' class='form-control select spaSelect' readOnly placeholder='" + label + "'/>" +
                                    "<div class='input-group-btn'>" +
                                        "<button class='btn btn-default dropdown-toggle' data-toggle='dropdown' " + disabled + " type='button'>" +
                                            "&nbsp;<i class='fa fa-caret-down fa-lg'></i>" +
                                        "</button>" +
                                        "<ul id='" + id + "_dropdown' class='dropdown-menu pull-right' />" +
                                    "</div>" +
                                "</div>" +
                            "</div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                $('#' + id + "_hidden").change(function () {
                    var val = $('body').data('plugin_spaControls').getSelectValue(base.module_id, col_id)
                    if (ds != null && readOnly != "readOnly") {
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, val);
                    }
                    var mod = GetModuleRef(base.module_id);
                    var cmd = "mod." + col_id + "_change";
                    if (typeof (eval(cmd)) === 'function') {
                        cmd += "('" + val + "')";
                        eval(cmd);
                    };
                });
            });

            //****************************************************************************************** Render Combobox
            $('div[spaAuto]').each(function () {

                var mode = 0; //base.settings.form.settings.mode;
                var id = base.module_id + '_' + $(this).attr('spaAuto');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var readOnly = $(this).attr('readonly');
             
                var disabled = "";
                if (mode === "view") {
                    disabled = "disabled";
                }

                if (readOnly === "readonly") {
                    disabled = "disabled";
                }

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                            "<div class='col-sm-8'>" +
                                "<input id='" + id + "_hidden' type='hidden' />" +
                                "<div class='input-group'>" +
                                    "<input id='" + id + "' type='text' autocomplete='off' class='form-control spaAuto' " + disabled + "placeholder='" + label + "'/>" +
                                    "<div class='input-group-addon'><i class='fa fa-search' aria-hidden='true'></i></div>" +
                                "</div>" +
                            "</div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                if (ds != null && readOnly != "readOnly") {
                    $('#' + id + '_hidden').change( function () {
                        var val = $('#' + id + '_hidden').val();
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, val);
                    });
                }
            });

            //****************************************************************************************** Incremental Search
            $('div[spaLookup]', '#' + base.module_id).each(function () {

                var id = base.module_id + '_' + $(this).attr('spaLookup');
                var label = $(this).attr('label');
                var ds = $(this).attr('dataset');
                var col = $(this).attr('column');
                var lu_ds = "base.settings.form." + $(this).attr('lookup_ds');
                var lu_col = $(this).attr('lookup_col');

                var sHTML = "<label for='" + id + "' class='col-sm-4 control-label'>" + label + "</label>" +
                            "<div class='col-sm-8'>" +
                                "<input id='" + id + "_hidden' type='hidden' />" +
                                "<input id='" + id + "' type='text' class='typeahead form-control spaLookup' placeholder='" + label + "'/>" +
                            "</div>";

                $(this).addClass('form-group');
                $(this).html(sHTML);

                $('#' + id).typeahead({
                    minLength:2, 
                    source: function(query, process) {
                            var filter = "[{ 'column': '" + lu_col + "', 'comparison': 'contains', 'value': '" + query + "' }]";
                            var lu = eval(lu_ds);
                            lu.Reload(filter);

                            var matches = [];
                            $.each(lu.dataset.Rows, function (x, item) {
                                var i = 0;
                                var row = [, ];
                                $.each(item, function (key, value) {
                                    if (i < 2) {
                                        row[i] = item[key];
                                        i++;
                                    }
                                });

                                matches.push(row[1]);
                            });
                            return process(matches);
                    },
                    updater: function(selected_item) {
                        var lu = eval(lu_ds);
                        $.each(lu.dataset.Rows, function (x, item) {
                            var i = 0;
                            var row = [, ];
                            $.each(item, function (key, value) {
                                if (i < 2) {
                                    row[i] = item[key];
                                    i++;
                                }
                            });

                            if (row[1] == selected_item) {
                                $('#' + id + "_hidden").val(row[0]);
                            }
                        });
                        return selected_item;
                    }
                });

                if (ds != null) {
                    $('#' + id).change(function () {
                        var dset = GetDatasetRef(this.id, ds);
                        dset.EditColumn(col, $('#' + id + "_hidden").val());
                    });
                }
            });
        },


        // Add Combobox options
        //****************************************************************************************** Populate combobox data
        addSelectOptions: function (module, id, items) {
            var sHTML = "";

            $.each(items, function (x, item) {

                switch ($.type(item)) {
                    case 'string':
                        sHTML += "<li><a id='" + module.module_id + "_" + id + "_" + item + "' href='#'>" + item + "</a></li>";
                        break;

                    case 'array':
                        sHTML += "<li><a id='" + module.module_id + "_" + id + "_" + item[0] + "' href='#'>" + item[1] + "</a></li>";
                        break;

                    case 'object':
                        var i = 0;
                        var row = [, ];
                        $.each(item, function (key, value) {
                            if (i < 2) {
                                row[i] = item[key];
                                i++;
                            }
                        });
                        sHTML += "<li><a id='" + module.module_id + "_" + id + "_" + row[0] + "' href='#'>" + row[1] + "</a></li>";
                        break;

                    default:
                }
            });

            $("#" + module.module_id + "_" + id + "_dropdown").html(sHTML);

            $("#" + module.module_id + "_" + id + "_dropdown li a").click(function (e) {
                $("#" + module.module_id + "_" + id).val($(this).text());
                $("#" + module.module_id + "_" + id + "_hidden").val($(this).attr("id")).trigger('change');
            });
        },

        // Add Combobox options
        //****************************************************************************************** Populate combobox data
        addAutoOptions: function (module, id, items) {
            $('#' + module.module_id + '_' + id).typeahead({ 
                source: items, 
                afterSelect: function(e) {
                    $('#' + module.module_id + '_' + id + '_hidden').val(e.id).trigger('change');
                }
            });

        },

        //****************************************************************************************** Populate textbox data
        setInputValue: function (module, id, value) {
            var myValue = value;
            var dataFormat = $("div[spaInput='" + id + "']", '#' + module).attr('dataFormat');
            var dataType = $("div[spaInput='" + id + "']", '#' + module).attr('dataType');
            var af = $("div[spaInput='" + id + "']", '#' + module).attr('accessFeature');

            if (af != undefined) {
                if (hasAccessFeature(af) == false) {
                    value = "####";
                }
            }

            switch (dataFormat) {
                case "datetime":
                    myValue = FormatDateWithTime(value);     
                    break;
                case "date":
                    myValue = FormatDate(value);     
                    break;
                case "numeric":
                    switch (dataType) {
                        case "money":
                            myValue = formatMoney(value);
                            break;
                        default:
                            myValue = value;
                    }
                    break;
                default:
                    myValue = value;
            }
            $("#" + module + "_" + id).val(myValue);
        },

        //****************************************************************************************** Populate textbox data
        setDateValue: function (module, id, value) {
            if (value != null) {
                var myValue = new Date(value.substring(0, 10) + " 00:00:00");
                $("#" + module + "_" + id).data("DateTimePicker").date(myValue);
            }
        },

        //****************************************************************************************** Populate textbox data
        setTimeValue: function (module, id, value) {
            if (value != null) {
                var myValue = value.substring(11, 16);
                $("#" + module + "_" + id).data("DateTimePicker").date(myValue);
            }
        },

        //****************************************************************************************** Populate memo data
        setTextValue: function (module, id, value) {
            $("#" + module + "_" + id).val(value);
        },

        //****************************************************************************************** Populate memo data
        setBlobValue: function (module, id, value) {
            $("#" + module + "_" + id).html(value);
        },

        //****************************************************************************************** Populate checkbox data
        setCheckValue: function (module, id, value) {
            if (value == "y") {
                $("#" + module + "_" + id).prop("checked", true);
            } else {
                $("#" + module + "_" + id).prop("checked", false);
            }
        },

        //****************************************************************************************** Populate combobox data
        setSelectValue: function (module, id, value) {
            $("#" + module + "_" + id + "_hidden").val(module + "_" + id + "_" + value);
            $("#" + module + "_" + id).val($("#" + module + "_" + id + "_" + value).text());
        },

        //****************************************************************************************** Populate combobox data
        setAutoValue: function (module, id, value) {
            $("#" + module + "_" + id + "_hidden").val(module + "_" + id + "_" + value);
            $("#" + module + "_" + id).typeahead('setValue', value);
        },

        //****************************************************************************************** Populate combobox data
        getSelectValue: function (module, id) {
            var result = $("#" + module + "_" + id + "_hidden").val();
            return result.substring(module.length + 1 + id.length + 1);
        },

        //****************************************************************************************** Populate memo data
        setLookupValue: function (module, id, value) {
            $("#" + module + "_" + id).val(value);
        }

    };

    // A really lightweight plugin wrapper around the constructor,
    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName, new Plugin(this, options));
            }
        });
    };

})(jQuery, window, document);
