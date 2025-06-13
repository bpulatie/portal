
function DateToString(value) {
    return (value.getFullYear() + '/' + (value.getMonth() + 1) + '/' + value.getDate());
}

function JSONDate(dateStr) {
    if (dateStr == null)
        return "";

    var m, day;
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (m + '/' + day + '/' + d.getFullYear())
}

function JSONDateWithTime(dateStr) {
    if (dateStr == null)
        return "";

    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var seconds = (d.getSeconds() < 10) ? "0" + d.getSeconds() : d.getSeconds();
    var formattedTime = hours + ":" + minutes + ":" + seconds;
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}

function formatLapTime(myTime) {
    var totalSeconds = Math.round(parseFloat(myTime) * 1000);
    var hh = Math.floor(totalSeconds / 3600000);
    totalSeconds = totalSeconds - (hh * 3600000);
    var mm = Math.floor(totalSeconds / 60000);
    totalSeconds = totalSeconds - (mm * 60000);
    var ss = Math.floor(totalSeconds / 1000);
    totalSeconds = totalSeconds - (ss * 1000);
    var lapTime = "";
    if (hh > 0) {
        lapTime = hh + ":";
    }
    if (mm < 10) {
        lapTime = lapTime + "0" + mm + ":";
    }
    else {
        lapTime = lapTime + mm + ":";
    }
    if (ss < 10) {
        lapTime = lapTime + "0" + ss + ".";
    }
    else {
        lapTime = lapTime + ss + ".";
    }
    if (totalSeconds < 10) {
        lapTime = lapTime + "00" + totalSeconds;
    }
    else {
        if (totalSeconds < 100) {
            lapTime = lapTime + "0" + totalSeconds;
        }
        else {
            lapTime = lapTime + totalSeconds;
        }
    }
    return lapTime;
}

function formatMoney(value) {
    if (value === null) {
        return value;
    }
    if (value === "####") {
        return "#.##";
    } 

    try
    {
        var money = parseFloat(value);
        return money.toFixed(2);
    }
    catch(e)
    {
        return "?.??"
    }
}

function formatDateWithTime(dateStr) {
    if (dateStr == null)
        return "";

    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var formattedTime = hours + ":" + minutes + ":" + d.getSeconds();
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}

function formatDate(dateStr) {
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return m + "/" + day + "/" + d.getFullYear();
}

function FormatDateWithTime(dateStr) {
    if (dateStr == null)
        return "";

    var d = new Date(dateStr.substring(0, 10) + "T" + dateStr.substring(11, 19));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var seconds = (d.getSeconds() < 10) ? "0" + d.getSeconds() : d.getSeconds();
    var formattedTime = hours + ":" + minutes + ":" + seconds;
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}

function FormatDate(dateStr) {
    if (dateStr == null)
        return "";

    var d = new Date(dateStr.substring(0, 10) + "T00:00:00");
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return m + "/" + day + "/" + d.getFullYear();
}

