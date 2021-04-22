// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// using the filtering at the dashboard table

    
$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

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

//$(document).ready(function () {
//    $('#productFrom').submit(function (e) {
//        e.preventDefault();
//        console.log(e);
//        debugger;
//        /*  var $form = $(this);*/
//        var form = document.getElementById("productFrom");
//        try {
//            $.ajax({
//                type: 'POST',
//                url: "Products/AddOrEdit",
//                data: new FormData(form),
//                contentType: false,
//                processData: false,
//                success: function (res) {

//                    $("#view-all").html(res);
//                    $('#form-modal .modal-body').html('');
//                    $('#form-modal .modal-title').html('');
//                    $('#form-modal').modal('hide');
//                    $.notify('Submitted successfuly', { globalPosition: 'top center', className: 'success' });

//                },
//                error: function (e) {
//                    console.log(e);
//                }
//            })

//        } catch (e) {
//            console.log(e);
//        }
//    });
//});

  // check if the input is valid using a 'valid' propertyif (!$form.valid) return false;

//jQueryAjaxPost = (e) =>{
///*return false;*/
//    e.preventDefault();
//    console.log(e);
//    var form = document.getElementById("productFrom");
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
               
//                    $("#view-all").html(res);
//                    $('#form-modal .modal-body').html('');
//                    $('#form-modal .modal-title').html('');
//                $('#form-modal').modal('hide');
//                $.notify('Submitted successfuly', { globalPosition: 'top center', className: 'success' });
               
//            },
//            error: function (e) {
//                console.log(e);
//            }
//        })

//    } catch(e) {
//        console.log(e);
//    }

   
//}

function AjaxDelete(e){
    e.preventDefault();
    //const id = e.target.getAttribute("data-id");
    var form = document.getElementById("delForm");
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
              
                $("#view-all").html(res);
                $('#delete-modal .modal-body').html('');
                $('#delete-modal .modal-title').html('');
                $('#delete-modal').modal('hide');
                $.notify('Deleted successfuly', { globalPosition: 'top center', className: 'success' });
                $('#tableFilter').DataTable({
                    "scrollY": "450px",
                    "scrollCollapse": true,
                    "paging": true,
                    "select": true,
                    "ordering": true,
                    "searching": true,
                    "scrollX": false,
                    "autoWidth": true
                });

            },
            error: function (e) {
                console.log(e);
            }
        })

    } catch (e) {
        console.log(e);
    }
  
    
}

deletePopUp = (url,title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $('#delete-modal .modal-body').html(res);
            $('#delete-modal .modal-title').html(title);
            $('#delete-modal').modal('show');
        }
    })
} 

function canelDel() {
    $('#delete-modal .modal-body').html('');
    $('#delete-modal .modal-title').html('');
    $('#delete-modal').modal('hide');
}
function editDel() {
    $('#form-modal .modal-body').html('');
    $('#form-modal .modal-title').html('');
    $('#form-modal').modal('hide');
}
//function as(event){
//    e.preventDefault();
//}

$(document).ready(function () {
    $('#tableFilter').DataTable({
        "scrollY": "350px",
        "scrollCollapse": false,
        "paging": true,
        "select": true,
        "ordering": true,
        "searching": true,
        "scrollX": false,
        "autoWidth": true
    });
    //$(document).ready(function () {
    //    $('#tableFilter').DataTable();
    //});
});