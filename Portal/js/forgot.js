
$(function () {


    $("#signin").on("click", function (e) {
        e.preventDefault();

        if ($("#inputEmail").val().length < 5) {
            displayMessage("Invalid Username or Password");
            return;
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/authenticate_Services.asmx/TemporaryPassword",
            data: "{ 'login_name': '" + $("#inputEmail").val() + "' }",
            dataType: "json",
            success: function (myResponse) {
                displayMessage(myResponse.message);
                if (myResponse.result == true) {
                    setTimeout(delay, 3000);
                }
            },
            error: ajaxFailed
        });
    });

});

function delay() {
    window.open("close.htm", "_self");
}

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