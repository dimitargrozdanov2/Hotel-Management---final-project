$("#formRegister").on("submit", function (event) {

    event.preventDefault();
    var myformdata = $("#formRegister").serialize();

    if (!$(this).valid()) {
        return;
    }

    $.post("/Account/RegisterUser", myformdata)
        .done(function (dataResponse) {
            //toastr.options.onHidden = function () {
            //    //window.location.reload();
            //};
            //toastr.options.timeOut = 100;
            //toastr.options.fadeOut = 100;
            toastr.success('User succesfully registered');

            $("#CreateUserModal").modal('hide');
            //$("#table").prepend(dataResponse);

            var buttons = `
    <form class="promote-user-form">
                <a data-target="#PromoteUserModal" data-toggle="modal" att="${dataResponse.id}" class="open-promote-user"
                   href="/Administration/Admin/PromoteUser/${dataResponse.id}">
                          <input type="submit" class="btn btn-success btn-sm promote-toggler" value="&#9998;Promote User" data-id="${dataResponse.id}" />
                </a>
    </form>
    <form class="delete-user-form>
    <div class="col-sm-2" style="margin-top:5%"></div>
    <input type="submit" class="btn btn-danger btn-sm delete-user-button DeleteUserRecord" value="&#x1F5D1;Delete User" data-id="${dataResponse.id}"/>
    </form>`;

            $('#table').DataTable().row.add([dataResponse.userName, dataResponse.email, "", buttons]).draw();



        }).fail(function (dataResponse) {

            for (var key in dataResponse.responseJSON) {

                var result = (dataResponse.responseJSON[key].description).toString();
                toastr.error(result);

            }
        });

})
