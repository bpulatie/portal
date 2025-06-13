function spaModule(options) {
    this.defaultOptions = {
        id: null,
        title: null,
        name: null,
        params: null,
        mode: "edit"
    };

    this.settings = $.extend({}, this.defaultOptions, options);
    this.module_id = "spaModule_" + this.settings.id;
    
    CURRENT_MODULE_ID = this.module_id;

    // Private Methods
    this._init = function () {
        var base = this;

        var url = base.settings.name;
        var code = url.substr(0, url.lastIndexOf(".")) + ".js";

        $.get(url, function (data) {
            var html = "<div id='" + base.module_id + "' class='spaModule' style='margin-top:15px;'>" + data + "</div>";
            $('#spa-body').append(html);

            $('#' + base.module_id).spaControls({ form: base });

            $.get(code, function (data) {
                init(base, base.settings.params);
                base.showModule(true);
            });
        });

/*
            <li class="divider"></li>
            <li>
                <a class="text-center" href="#">
                    <strong>See All Alerts</strong>
                    <i class="fa fa-angle-right"></i>
                </a>
            </li>
*/

        var html = $("#spa-open-modules").html();
        var shtml = "<li><a id='openitem_" + base.module_id + "' class='spaopenitem' href='#'>" + base.settings.title + "</a></li>" + html;
        $("#spa-open-modules").html(shtml);

        $(".spaopenitem").click(function (e) {
            e.preventDefault();
            var x = $(this).attr("id");
            x = x.substr(x.lastIndexOf("_") + 1);
            MODULES[x].showModule();
        });
    }

    this._init();

    //Public methods
    spaModule.prototype.showModule = function (firstTime) {
        if (firstTime != true) {
            if (typeof (this.refresh) === 'function') {
                this.refresh()
            };
        };

        MODSTACK.push(this.module_id);

        $(".spaModule").hide();
        $("#" + this.module_id).show();
    },

    spaModule.prototype.hideModule = function () {

    },

    spaModule.prototype.getModuleID = function () {
        return this.module_id;
    }

    spaModule.prototype.getTitle = function () {
        return this.settings.title;
    }

    spaModule.prototype.hideModal = function (modal) {
        $("#" + this.module_id + "_modal_" + modal).modal('hide');
    }

    spaModule.prototype.showModal = function (modal) {
        $("#" + this.module_id + "_modal_" + modal).modal('show');
    }

    spaModule.prototype.hideModal = function (modal) {
        $("#" + this.module_id + "_modal_" + modal).modal('hide');
    }
        
    spaModule.prototype.hideTab = function (tab) {
        $("#" + this.module_id + "_tab_" + tab).hide();
    }

    spaModule.prototype.showTab = function (tab) {
        $("#" + this.module_id + "_tab_" + tab).show();
    }

    spaModule.prototype.hideDeleteBtn = function () {
        $("#" + this.module_id + "_deleteBtn").hide();
    }

    spaModule.prototype.showDeleteBtn = function () {
        $("#" + this.module_id + "_deleteBtn").show();
    }

    spaModule.prototype.show = function (id) {
        $("#" + this.module_id + "_" + id).show();
    }

    spaModule.prototype.hide = function (id) {
        $("#" + this.module_id + "_" + id).hide();
    }

    spaModule.prototype.getItem = function (e) {
        return $("#" + this.module_id + "_" + e);
    }

    spaModule.prototype.getValue = function (e) {
        if ($("#" + this.module_id + "_" + e).hasClass("spaSelect")) {
            return $('body').data('plugin_spaControls').getSelectValue(this.module_id, e);
        } else {
            if ($("#" + this.module_id + "_" + e).hasClass("spaCheck")) {
                if ($("#" + this.module_id + "_" + e).is(':checked') == true) {
                    return "y";
                } else {
                    return "n";
                }
            } else {
                return $("#" + this.module_id + "_" + e).val();
            }
        }
    }

    spaModule.prototype.addOptions = function (e, options) {
        $('body').data('plugin_spaControls').addSelectOptions(this, e, options);
    }

    spaModule.prototype.addAutoOptions = function (e, options) {
        $('body').data('plugin_spaControls').addAutoOptions(this, e, options);
    }


    spaModule.prototype.setValue = function (e, value) {
        if ($("#" + this.module_id + "_" + e).hasClass("spaSelect")) {
            $('body').data('plugin_spaControls').setSelectValue(this.module_id, e, value);
        } else {
            if ($("#" + this.module_id + "_" + e).hasClass("spaCheck")) {
                $('body').data('plugin_spaControls').setCheckValue(this.module_id, e, value);
            } else {
                $("#" + this.module_id + "_" + e).val(value);
            }
        }
    }

    spaModule.prototype.setPanelTitle = function (e, value) {
        $("#" + this.module_id + "_panel_" + e)[0].innerText = value;
    }

    spaModule.prototype.showStatusMessage = function (message) {
        $("#" + this.module_id + "_statusline").html("<span class='alert'>" + message + "</span>");
        setTimeout(function (x) {
            $("#" + x + "_statusline").html("&nbsp;");
        }, 3000, this.module_id);

    }

    spaModule.prototype.closeModule = function (e) {
        var base = this;
        var id;
        if (e === undefined) {
            id = base.module_id;
        } else {
            id = e.module_id;
        }

        $('#openitem_' + id).parent().remove();
        $('#' + id).remove();

        var x = id;
        x = x.substr(x.lastIndexOf("_") + 1);

        if (e === undefined) {
            base.settings.title = 'removed';
        } else {
            e.settings.title = 'removed';
        }

        removeModule(x);
    }

};
