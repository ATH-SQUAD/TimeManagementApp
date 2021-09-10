$(document).ready(function () {
    $(".ttp").tooltip({
        delay: {
            show: 500
        }
    });
});

// Shows sweet-alert error.
function showError(errorTitle, errorMessage) {
    Swal.fire({
        icon: "error",
        title: errorTitle,
        text: errorMessage,
        showCloseButton: true,
        focusConfirm: true
    });
}

// Shows sweet-alert confirmation alert.
function showDeleteConfirmation(confirmTitle, confirmMessage, resultFunc) {
    Swal.fire({
        title: `${confirmTitle}`,
        text: `${confirmMessage}`,
        icon: "warning",
        showCancelButton: true,
        cancelButtonColor: "#3085d6",
        confirmButtonColor: "#d33",
        confirmButtonText: "Tak, usuń go",
        cancelButtonText: "Nie usuwaj",
        reverseButtons: true,
        showCloseButton: true,
        focusCancel: true
    }).then(resultFunc);
}