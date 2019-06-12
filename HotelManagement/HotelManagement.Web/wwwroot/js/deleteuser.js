
$('.delete-user-button').on('click', function (deleteUserEvent) {
    confirm("Are you sure you want to delete this user?");
    var $this = $(this);

    var idOfUserToDelete = $this.attr('att');

    var antiForgery = ($('#anti-forgery-span').find('input'))[0];

    var postData = "userId=" + idOfUserToDelete + "&" + antiForgery.name + "=" + antiForgery.value;

    debugger;

    $.post("/Account/Delete", postData)
        .done(function (dataResponse) {
            toastr.options.onHidden = function () {
                //  window.location.reload();
            };

            toastr.options.timeOut = 100;
            toastr.options.fadeOut = 100;

            toastr.success(dataResponse);
            $("#table").remove(dataResponse);

        }).fail(function (dataResponse) {
            debugger;
            console.log('we got an error');

            toastr.error("User deletion failed");

        });


});
