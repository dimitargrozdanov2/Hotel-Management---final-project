
$('.delete-user-form').on('submit', function (deleteUserEvent) {
    deleteUserEvent.preventDefault();
    confirm("Are you sure you want to delete this user?");
    var $this = $(this);

    var idOfUserToDelete = $this.find(".DeleteUserRecord").data('id');
    var tr = $(document).find("#user-tr-" + idOfUserToDelete);
    var antiForgery = ($('#anti-forgery-span').find('input'))[0];

    var postData = "id=" + idOfUserToDelete + "&" + antiForgery.name + "=" + antiForgery.value;

    //debugger;
    var url = "/Account/Delete/" + idOfUserToDelete
    $.post("/Account/Delete/", postData)
        .done(function (dataResponse) {
            toastr.options.onHidden = function () {
            };

            toastr.options.timeOut = 100;
            toastr.options.fadeOut = 100;
            toastr.success("User succesfully deleted!");

            $('#table').DataTable().row(tr).remove().draw();

        }).fail(function (dataResponse) {
            toastr.error("User deletion failed");

        });


});
