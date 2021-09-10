// Focus first-top option in #connections-list-selection.
$("#connections-list-selection").val($("#connections-list-selection option:first").val()).focus();
let selectedDbType = $("#db-type-selection").find(":selected").text();

$("#db-type-selection").on("change", function () {
    resetForm();
    selectedDbType = $("#db-type-selection").find(":selected").text();
    let connectionView;

    switch (selectedDbType) {
        case "MongoDB":
            connectionView = $("#mongo-connection-view").html().trim();
            break;
        case "SqlServer":
            connectionView = $("#sqlserver-connection-view").html().trim();
            break;
        default:
            break;
    }

    $("#db-connection-view").html($(connectionView));
});
$("#db-connection-view :input").on("change keyup paste", function () {
    if (!$("#create-connection-btn").is("[disabled]")) {
        resetForm();
    }
});

function onClickCheckConnection() {
    $("#check-connection-btn").prop("disabled", true);
    resetForm();
    const spinnerView = $("#waiting-for-connection-spinner").html().trim();
    $("#check-connection-result").html($(spinnerView));

    $.ajax({
        url: "/TimeManagement/UtworzRaport/Index?handler=CheckConnection",
        type: "POST",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(getConnectionData())
    })
        .done(function (result) {
            checkConnectionResult(result.connectionFailure, result.dbNames);
        })
        .fail (function () {
            checkConnectionResult(true);
        })
}
function checkConnectionResult(connectionFailure, dbNames) {
    $("#check-connection-btn").prop("disabled", false);
    if (connectionFailure) {
        $("#check-connection-result").html("Połączenie z serwerem nieudane...");
        $("#check-connection-result").css("color", "red");
    }
    else {
        $("#check-connection-result").html("Pomyślne połączenie z serwerem.");
        $("#check-connection-result").css("color", "green");
        dbNames.forEach((value) => {
            let option = new Option(value, value);
            $(option).html(value);
            $("#db-name-selection").append(option);
        });
        $("#db-name-selection").prop("disabled", false);
        $("#create-connection-btn").prop("disabled", false);
        $("#db-name-label").css("color", "#212529");
    }
}
function onClickCreateConnection() {
    $.ajax({
        url: "/TimeManagement/UtworzRaport/Index?handler=CreateConnection",
        type: "POST",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: JSON.stringify(getConnectionData())
    })
        .done(function (result) {
            createConnectionResult(result);
        })
        .fail(function (result) {
            createConnectionResult(result);
        })
}
function createConnectionResult(result) {
    if (result.connectionFailure) {
        $("#create-connection-result").html("Połączenie z bazą nieudane...");
        $("#create-connection-result").css("color", "red");
    }
    else {
        const text = selectedDbType + " | " + $("#db-name-selection").find(":selected").text();
        const value = result.connectionId;
        const option = new Option(text, value);
        $(option).html(text);
        $("#connections-list-selection").prepend(option);
        $("#connections-list-selection").val(value).focus();
        $("#create-connection-btn").blur();
    }
}
function onClickCreateReport() {
    Swal.fire({
        title: "Nazwa Raportu",
        input: "text",
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        showCloseButton: true,
        confirmButtonText: "Utwórz raport",
        cancelButtonText: "Anuluj",
        confirmButtonColor: "#28A745"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/TimeManagement/UtworzRaport/Index?handler=CreateReport",
                type: "POST",
                traditional: true,
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                data: {
                    ReportName: result.value,
                    ConnectionId: $("#connections-list-selection").find(":selected").val()
                }
            })
                .done(function (result) {
                    if (result.wrongName) {
                        showError("Nazwa raportu jest już zajęta!",
                            `Raport ${result.newReportName} już istnieje. Wybierz inną nazwę.`);
                    } else {
                        window.location.href = "http://localhost:5000/TimeManagement/Kreator/" + result.reportId;            
                    }                 
                })
                .fail(function () {
                    showError("Coś poszło nie tak!",
                        "Podczas tworzenia raportu wystąpił błąd. Sprawdź połączenie z serwerem.");
                })
        }
    })
}

function getConnectionData() { 
    switch (selectedDbType) {
        case "MongoDB":
            return {
                "MongoConnectionViewModel": {
                    "Hostname": $("#hostname").val(),
                    "Port": $("#port").val()
                },
                "DbType": selectedDbType,
                "DbName": $("#db-name-selection").find(":selected").text()
            }
            break;
        case "SqlServer":
            return {
                "DbType": selectedDbType,
                "DbName": $("#db-name-selection").find(":selected").text()
            }
            break;
        case "Oracle":
            return {
                "DbType": selectedDbType,
                "DbName": $("#db-name-selection").find(":selected").text()
            }
            break;
        default:
            return {
                "DbType": selectedDbType,
                "DbName": $("#db-name-selection").find(":selected").text()
            }
            break;
    }
}
function resetForm() {
    $("#create-connection-btn").prop("disabled", true);
    $("#db-name-selection").prop("disabled", true);
    $("#db-name-selection").html("");
    $("#db-name-label").css("color", "#ccc");

    $("#check-connection-result").html("");
    $("#create-connection-result").html("");
    $("#create-report-result").html("");
}



