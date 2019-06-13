$("#formCreateBusiness").submit(function (event) {

    event.preventDefault();
    var myformdata = $("#formCreateBusiness").serialize();

    if (!$(this).valid()) {
        return;
    }

    $.post("/Administration/Admin/CreateBusiness", myformdata)
        .done(function (dataResponse) {
            console.log(dataResponse);
            //toastr.options.onHidden = function () {
            //    //window.location.reload();
            //};
            toastr.success("Created business succesfully");

            $("#CreateBusinessModal").modal('hide');
            //$("#table").prepend(dataResponse);
            console.log(dataResponse);

            var buttons = `
<a class="btn btn-info btn-sm"
href="Administration/Admin/AllLogbooksForBusiness/${dataResponse.Name}">
            <i class="fas fa-pencil-alt"></i>
            Manage Logbooks
        </a>
        <a class="btn btn-primary btn-sm" style="margin-top:5%"
href="Administration/Admin/AddImageToBusiness/${dataResponse.Name}">
            <i class="fas fa-images"></i>
            Add Image
        </a>`;

            $('#table').DataTable().row.add([dataResponse.name, dataResponse.location, dataResponse.description, buttons]).draw();
        }).fail(function (dataResponse) {
            toastr.error(dataResponse.responseJSON.message);
        });
});