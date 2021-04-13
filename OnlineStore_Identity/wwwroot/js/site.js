// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showPopUp = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

$jQueryAjaxPost = form => {
    return false;
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#view-all").html(res);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $("form-modal .modal-body").html(res);
            },
            error: function (e) {
                console.log(e);
            }
        })

    } catch(e) {
        console.log(e);
    }

   
}

$AjaxDelete = (from, e) => {
    e.preventDefault();
    if (confirm('Are you sure to delete this product?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                //data: new FormData(form),
                //contentType: false,
                //processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $("#view-all").html(res.html);
                    }
                },
                error: function (e) {
                    console.log(e);
                }
            })
        } catch (e) {
            console.log(e);
        }
    }
    return false;
}

//function prev(e) {
//    e.preventDefault();
//}