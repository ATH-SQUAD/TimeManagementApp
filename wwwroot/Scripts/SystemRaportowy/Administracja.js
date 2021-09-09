   $(document).ready(function () {
       $('#myTable').DataTable({
           select: true
       });
   });


$('#select_all').click(function () {
    $('.Check').prop('checked', this.checked)
});


function Activate(elem) {
    var isActive = $(elem).is('EmailConfirmed');
    var UId = $(elem).data('data_userId');
    $.ajax({
        type: 'POST',
        url: "/TimeManagement/Administracja/Uzytkownicy?handler=OnPostActivate",
        traditional: true,
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: { isChecked: isActive, userId: UId }
    })
        .done(function (result) {
            activateRaw = result.activateRaw;
        })
}

$('#ActivateUser').click(function () {
    $('.Check').prop('checked', this.checked)
});