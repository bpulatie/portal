

$(window).unload(function () {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: ROOT + "/services/authenticate_Services.asmx/BrowserClosed",
        data: "",
        dataType: "json"
    });
});


$(function () {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: ROOT + "/services/sys_session_Services.asmx/GetSessionContext",
        data: "",
        dataType: "json",
        success: function (myResponse) {
            if (myResponse.result == true) {
                CONTEXT = jQuery.parseJSON(myResponse.data);

                var userType = "Standard User";

                if (CONTEXT.user.user_type == "s")
                    userType = "System Administrator";
                if (CONTEXT.user.user_type == "c")
                    userType = "Client Administrator";

                var user = '<table>';
                user += '<tr><td width="110px"><strong>Application: </strong></td><td>' + CONTEXT.system.site_name + '</td></tr>';
                user += '<tr><td width="110px"><strong>Release: </strong></td><td>10/27/2017 21:30pm</td></tr>';
                user += '<tr><td><strong>Client Name: </strong></td><td>' + CONTEXT.client.client_name + '</td></tr>';
                user += '<tr><td><strong>User Name: </strong></td><td>' + CONTEXT.user.first_name + ' ' + CONTEXT.user.last_name + '</td></tr>';
                user += '<tr><td><strong>User Type: </strong></td><td>' + userType + '</td></tr>';
                user += '</table>';

                var logo = "<img src='images/rdm.png' style='padding-top:5px;width:40px' alt=''/>";


                $("#spa_identity").html(CONTEXT.client.client_name);
                $("#spa_identity").attr("data-content", user);
                $("#spa_identity").popover();

                PageSetup();
                createModule("Dashboard", "/portal/sys-dashboard/detail.htm", {}, "Edit");

                SPA_TID = setTimeout(spa_timer, 5000);

            } else {
                ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
            }
        },
        error: ajaxFailed
    });
});

function spa_timer() {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: ROOT + "/services/authenticate_Services.asmx/KeepSessionAlive",
        data: "",
        dataType: "json",
        success: function (myResponse) {
            if (myResponse.result == true) {
                var res = jQuery.parseJSON(myResponse.data);

                $(".allEvents").unbind('click');
                $(".spaalertitem").unbind('click');

                var html = $("#spa-alerts").html();
                var shtml = "";

                for (var x in res.events) {
                    var e = res.events[x];
                    shtml += "<li><a id='alertitem_" + e.event_id + "' class='spaalertitem' href='#'><div><small><strong>" + e.event_date + "</strong></small></div><div class='truncate'>" + e.event_summary + "</div></a></li>";
                }

                var foot = "<li class='divider'></li><li><a class='text-center allEvents' href='#'><strong>See All Events</strong><i class='fa fa-angle-right'></i></a></li>";

                $("#spa-alerts").html(shtml + foot);

                $(".allEvents").click(function () {
                    createModule("System Events", "/portal/sys-event/browse.htm", {}, "view");
                });

                $(".spaalertitem").click(function (e) {
                    e.preventDefault();
                    var x = $(this).attr("id");
                    x = x.substr(x.lastIndexOf("_") + 1);
                    createModule("System Event", "/portal/sys-event/detail.htm", { 'id': x }, "view");
                });

                SPA_TID = setTimeout(spa_timer, 5000);

            } else {
                ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
            }
        },
        error: ajaxFailed
    });    
}

function PageSetup() {
    var sheet = "default";
    if (CONTEXT.user.style_preference != null) {
        sheet = CONTEXT.user.style_preference;
    }

    SetTheme(sheet);
   
    if (CONTEXT.user.menu_location == "1") {
        $('.sidebar').remove();
        $('#page-wrapper').css('margin', '0 0 0 0');
        CreateMenu();
    } else {
        CreateSideMenu();
    }

    //$('#spa-body').html("&nbsp;");

    $("a[spamenuitem]").click(function (e) {
        e.preventDefault();
        var item = $(this).attr('spamenuitem');
        createModule(item, $(this).attr('href'), {}, $(this).attr('mode'));
    });

    $("#logoutBtn").click(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/authenticate_Services.asmx/SignOut",
            data: "",
            dataType: "json",
            success: function (data) {
                window.open("close.htm", "_self");
            },
            error: ajaxFailed
        });

        return false;
    });

    $("#themeBtn a").click(function () {
        SetTheme($(this).attr('data-theme'));
    });

    $(".allEvents").click(function () {
        createModule("System Events", "/portal/sys-event/browse.htm", {}, "view");        
    });

    MODULES = [];
}

function SetTheme(sheet) {
    $('link[title="spaStyle"]').attr('href', themes[sheet]);

    $('.theme-link').click(function () {
        var themeurl = themes[sheet];
    });
}

function CreateMenu() {

    var HTML = "";

    for (var x = 0; x < CONTEXT.menu.length; x++) {
        var menu = CONTEXT.menu[x];

        HTML += ""; // "<ul class='nav navbar-nav'>";

        if (menu.items === undefined) {
            if (menu.moniker === undefined) {
                if (menu.id === undefined) {
                    HTML += "    <li><a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menu.header + "</a></li>";
                } else {
                    HTML += "    <li><a id='" + menu.id + "' href='#'>" + menu.header + "</a></li>";
                }
            } else {
                HTML += "    <li><a spamenuitem='" + menu.header + "' href='" + menu.moniker + "' class='dropdown-toggle' data-toggle='dropdown'>" + menu.header + "</a></li>";
            }
        } else {
            HTML += "    <li class='dropdown'>" +
                    "        <a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menu.header + " <span class='caret'></span></a>";
            HTML += CreateLine(menu);
            HTML += "    </li>";
        }


        //HTML += "</ul>";
    };

    $('#spa-menu').prepend(HTML);

}

function CreateLine(menu) {
    var HTML = "";
    if (menu.items.length === 0) {
        HTML += "<ul id='" + menu.id + "' class='dropdown-menu'></ul>";
    } else {
        for (var y = 0; y < menu.items.length; y++) {
            var item = menu.items[y];

            if (y === 0) {
                HTML += "  <ul class='dropdown-menu'>";
            };

            if (item.name !== "My Profile") {
                if (item.id === undefined) {
                    if (item.moniker === undefined) {
                        HTML += "<li><a spamenuitem='" + item.name + "' href='#'>" + item.name + "</a></li>";
                    } else {
                        var mode = "edit";
                        if (item.mode == "1") {
                            mode = "view";
                        }
                        HTML += "<li><a spamenuitem='" + item.name + "' href='" + item.moniker + "' mode='" + mode + "'>" + item.name + "</a></li>";
                    }
                } else {
                    HTML += "<li><a spamenuitem='" + item.name + "' " + item.id + " href='" + item.moniker + "' >" + item.name + "</a></li>";
                }
            }

            if (y === (menu.items.length - 1)) {
                HTML += "  </ul>";
            };
        }
    }
    return HTML;
}

function CreateSideMenu() {

    var HTML = "";

    for (var x = 0; x < CONTEXT.menu.length; x++) {
        var menu = CONTEXT.menu[x];

        if (menu.items === undefined) {
            if (menu.moniker === undefined) {
                if (menu.id === undefined) {
                    HTML += "    <li><a href='#' data-toggle='dropdown'>" + menu.header + "</a></li>";
                } else {
                    HTML += "    <li><a id='" + menu.id + "' href='#'>" + menu.header + "</a></li>";
                }
            } else {
                HTML += "    <li><a spamenuitem='" + menu.header + "' href='" + menu.moniker + "' data-toggle='dropdown'>" + menu.header + "</a></li>";
            }
        } else {

            HTML += "    <li>" +
                    "        <a href='#' >" + menu.header + " <span class='fa fa-caret-left'></span></a>" +
                    "        <ul class='nav nav-second-level'>";

            if (menu.header !== 'Themes') {
                HTML += CreateSideMenuLine(menu);
            }

            HTML += "        </ul>";
            HTML += "    </li>";
        }
    };

    $('#side-menu').html(HTML);
    $('#side-menu').metisMenu();

    $("a[spamenuitem]").click(function (e) {
        e.preventDefault();
        var item = $(this).attr('spamenuitem');
        createModule(item, $(this).attr('href'), {}, $(this).attr('mode'));
    });

}

function CreateSideMenuLine(menu) {

    var HTML = "";
    for (var y = 0; y < menu.items.length; y++) {
        var item = menu.items[y];

        if (item.name !== "My Profile") {
            if (item.id === undefined) {
                if (item.moniker === undefined) {
                    HTML += "<li><a spamenuitem='" + item.name + "' href='#'>" + item.name + "</a></li>";
                } else {
                    var mode = "edit";
                    if (item.mode == "1") {
                        mode = "view";
                    }
                    HTML += "<li><a spamenuitem='" + item.name + "' href='" + item.moniker + "' mode='" + mode + "'>" + item.name + "</a></li>";
                }
            } else {
                HTML += "<li><a spamenuitem='" + item.name + "' " + item.id + " href='" + item.moniker + "' >" + item.name + "</a></li>";
            }
        }
    }
    return HTML;
}

function CreateThemes() {
    var HTML =  "  <ul id='themeBtn' class='dropdown-menu'>";
    HTML += "    <li><a href='#' data-theme='default' class='theme-link'>Default</a></li>";
    HTML += "    <li><a href='#' data-theme='amelia' class='theme-link'>Amelia</a></li>";
    HTML += "    <li><a href='#' data-theme='cerulean' class='theme-link'>Cerulean</a></li>";
    HTML += "    <li><a href='#' data-theme='cosmo' class='theme-link'>Cosmo</a></li>";
    HTML += "    <li><a href='#' data-theme='cyborg' class='theme-link'>Cyborg</a></li>";
    HTML += "    <li><a href='#' data-theme='darkly' class='theme-link'>Darkly</a></li>";
    HTML += "    <li><a href='#' data-theme='flatly' class='theme-link'>Flatly</a></li>";
    HTML += "    <li><a href='#' data-theme='journal' class='theme-link'>Journal</a></li>";
    HTML += "    <li><a href='#' data-theme='lumen' class='theme-link'>Lumen</a></li>";
    HTML += "    <li><a href='#' data-theme='paper' class='theme-link'>Paper</a></li>";
    HTML += "    <li><a href='#' data-theme='readable' class='theme-link'>Readable</a></li>";
    HTML += "    <li><a href='#' data-theme='sandstone' class='theme-link'>Sandstone</a></li>";
    HTML += "    <li><a href='#' data-theme='shamrock' class='theme-link'>Shamrock</a></li>";
    HTML += "    <li><a href='#' data-theme='simplex' class='theme-link'>Simplex</a></li>";
    HTML += "    <li><a href='#' data-theme='slate' class='theme-link'>Slate</a></li>";
    HTML += "    <li><a href='#' data-theme='spacelab' class='theme-link'>Spacelab</a></li>";
    HTML += "    <li><a href='#' data-theme='superhero' class='theme-link'>Superhero</a></li>";
    HTML += "    <li><a href='#' data-theme='united' class='theme-link'>United</a></li>";
    HTML += "    <li><a href='#' data-theme='yeti' class='theme-link'>Yeti</a></li>";
    HTML += "  </ul>";
    return HTML;
}

function createModule(title, name, params, mode) {
    for (var x = 0; x < MODULES.length; x++) {
        if (MODULES[x] != null) {
            if (MODULES[x].getTitle() == title) {
                MODULES[x].showModule();
                return;
            }
        }
    }

    var id = MODULES.length;
    var mod = new spaModule({
        id: id,
        title: title,
        name: ROOT + name,
        params: params,
        mode: mode
    });

    MODULES.push(mod);
}

function removeModule(i) {
    MODULES[i] = null;

    for (var x = MODSTACK.length; x > 0; x--) {
        for (var y = 0; y < MODULES.length; y++) {
            if (MODULES[y] != null) {
                if (MODULES[y].module_id == MODSTACK[x - 1]) {
                    MODULES[y].showModule();
                    return;
                }
            }
        }
    }        
}

function getModuleID() {
    if (CURRENT_MODULE < 0)
        return null;
    else
        return MODULES[CURRENT_MODULE].getModuleID();
}

function GetModuleRef(module_id) {
    for (var x = MODSTACK.length; x > 0; x--) {
        if (MODULES[x] != null) {
            if (MODULES[x].module_id == module_id) {
                return MODULES[x];
            }
        }
    }
    return null;
}

function GetDatasetRef(id, ds) {
    var therest = id.substr(10);
    var no = therest.substr(0, therest.indexOf('_'));
    var module_id = "spaModule_" + no;

    var mod = GetModuleRef(module_id);

    return eval("mod." + ds);
}

function GetUserAlerts() {
/*
    <li>
        <a href="#">
            <div>
                <i class="fa fa-comment fa-fw"></i> New Comment
                <span class="pull-right text-muted small">4 minutes ago</span>
            </div>
        </a>
    </li>
    <li class="divider"></li>
    <li>
        <a class="text-center" href="#">
            <strong>See All Alerts</strong>
            <i class="fa fa-angle-right"></i>
        </a>
    </li>
*/
}

function GetUserTasks() {
/*
    <li>
        <a href="#">
            <div>
                <p>
                    <strong>Task 4</strong>
                    <span class="pull-right text-muted">80% Complete</span>
                </p>
                <div class="progress progress-striped active">
                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                        <span class="sr-only">80% Complete (danger)</span>
                    </div>
                </div>
            </div>
        </a>
    </li>
    -->
    <li class="divider"></li>
    <li>
        <a class="text-center" href="#">
            <strong>See All Tasks</strong>
            <i class="fa fa-angle-right"></i>
        </a>
    </li>
*/
}

function GetUserMessages() {
/*
    <li>
        <a href="#">
            <div>
                <strong>John Smith</strong>
                <span class="pull-right text-muted">
                    <em>Yesterday</em>
                </span>
            </div>
            <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
        </a>
    </li>
    <li class="divider"></li>
    <li>
        <a class="text-center" href="#">
            <strong>Read All Messages</strong>
            <i class="fa fa-angle-right"></i>
        </a>
    </li>
*/
}

function GetUserDetails(myUserCookie) {
    dsUSER = new spaDataset({
        id: 'dsUSER',
        pk: 'user_id',
        webservice: '/services/system_user_Services.asmx/GetByID',
        params: '"user_id": "' + myUserCookie + '"'
    });
}

function DisplayMessage(e, x) {
    $("div[spaStatusLine]", $('#' + e.module_id)).html(x);
    setTimeout(function () {
        $("div[spaStatusLine]", $('#' + e.module_id)).html("&nbsp;");
    }, 3000);

}

function DisplayModalMessage(e, x) {
    $("div[spaModalStatusLine]", $('#' + e.module_id)).html(x);
    setTimeout(function () {
        $("div[spaModalStatusLine]", $('#' + e.module_id)).html("&nbsp;");
    }, 3000);
}
