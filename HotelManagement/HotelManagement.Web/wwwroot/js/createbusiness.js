$("#formCreateBusiness").on('submit', function (event) {
    event.preventDefault();
    var myformdata = $("#formCreateBusiness").serialize();

    if (!$(this).valid()) {
        return;
    }

    $.post("/Administration/Admin/CreateBusiness", myformdata)
        .done(function (dataResponse) {
            toastr.success("Created business succesfully");
            $("#CreateBusinessModal").modal('hide');

            var buttons = `
<a class="btn btn-info btn-sm"
href="/Administration/Admin/AllLogbooksForBusiness?name=${dataResponse.name}">
            <i class="fas fa-pencil-alt"></i>
            Manage Logbooks
        </a>
        <a class="btn btn-primary btn-sm" style="margin-top:5%"
href="/Administration/Admin/AddImageToBusiness?name=${dataResponse.name}">
            <i class="fas fa-images"></i>
            Add Image
        </a>`;

            var newRow = $('#table').DataTable().row.add([dataResponse.name, dataResponse.location, dataResponse.description, buttons]).draw();
        }).fail(function (dataResponse) {
            toastr.error(dataResponse.responseJSON.message);
        });
});