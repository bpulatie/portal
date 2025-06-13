
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
            } else {
                ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
            }
        },
        error: ajaxFailed
    });

    $("#signin").on("click", function (e) {
        e.preventDefault();

        if ($("#inputPassword1").val().length < 5) {
            displayMessage("Invalid Password");
            return;
        }

        if ($("#inputPassword1").val() !== $("#inputPassword2").val()) {
            displayMessage("Passwords do not match");
            return;
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/authenticate_Services.asmx/UpdatePassword",
            data: "{ 'user_id': '" + CONTEXT.user.user_id + "', 'password': '" + $("#inputPassword1").val() + "' }",
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                        window.open("main.htm", "_self");
                } else {
                    displayMessage("Password not saved - Please contact support")
                    return;
                }
            },
            error: ajaxFailed
        });
    });

});


function displayMessage(message) {

    $("#statusline").html("<span class='alert'>" + message + "</span>");
    setTimeout(function (x) {
        $("#statusline").html("&nbsp;");
    }, 
    5000);
}

function ShowSystemModal(title, message) {
    if (title != null) {
        $('#spa_system_modal_title').html(title);
    } else {
        $('#spa_system_modal_title').html("System Message");
    }

    if (message != null) {
        $('#spa_system_modal_message').html(message);
    } else {
        $('#spa_system_modal_title').html("Unknown Message");
    }

    $('#spa_system_modal').modal();
}

function ajaxSuccess(xmlRequest) {

}

function ajaxFailed(xmlRequest, textStatus) {
    if (typeof(cl) !== "undefined") {
        cl.hide();
    }

    ShowSystemModal('System Error', '<table>' +
                                    '<tr><td width="150px"><b>Request Status: </b></td><td>' + xmlRequest.status + '</td></tr>' +
                                    '<tr><td><b>Status Text: </b></td><td>' + xmlRequest.statusText + '</td></tr>' +
                                    '<tr><td><b>Response Text: </b></td><td>' + xmlRequest.responseText + '</td></tr>' +
                                    '<tr><td><b>Text Status: </b></td><td>' + textStatus + '</td></tr>' +
                                    '</table>');
}