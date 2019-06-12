$("#CreateBusinessRecord").click(function (event) {

    event.preventDefault();
    var myformdata = $("#formCreateBusiness").serialize();
    console.log(myformdata);
    $.post("/Administration/Admin/CreateBusiness", myformdata)
        .done(function (dataResponse) {
            console.log(dataResponse);
            toastr.options.onHidden = function () {
                //window.location.reload();
            };
            toastr.options.timeOut = 100;
            toastr.options.fadeOut = 100;
            toastr.success(dataResponse);

            $("#CreateBusinessModal").modal('hide');
            $("#table").prepend(dataResponse);


        }).fail(function (dataResponse) {

            for (var key in dataResponse.responseJSON) {

                var result = (dataResponse.responseJSON[key].description).toString();
                toastr.error(result);
            }
            });
});