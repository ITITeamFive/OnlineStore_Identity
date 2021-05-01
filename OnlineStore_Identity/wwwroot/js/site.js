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
        "autoWidth": true,
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
   
    });
    //$(document).ready(function () {
    //    $('#tableFilter').DataTable();
    //});
});


//------------------Show Product Details----------------------
showDetails = (url) => {
    console.log("hi");
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $('#details-modal .modal-body').html(res);
            $('#details-modal').modal('show');
        }
    })
}

CloseModel = () => {
    $('#details-modal').modal('hide');
    $('#checkout-modal').modal('hide');
}

//-------------------Delete Wishlist-----------------------
remove = (url) => {
    $.ajax({
        method: "GET",
        url: url,
        success: function (res) {
            //debugger;
            $('#wishlist').html(res);
        }
    });
}

AddToCart = (id, quantity, e) => {
 
    var btn = e.target;
    if (btn.nodeName === 'SPAN') {
        btn = btn.parentNode;
    }
    $.ajax({
        url: "Carts/AddToCart",
        type:"get",
        data: { "id": id, "quantity": quantity },
        traditional: true,
        success: function (res) {
            CloseModel();
            btn.setAttribute("disabled", "true");
            $.notify('Added successfuly', { globalPosition: 'top center', className: 'success' });
        },
        error: function (err) {
            $.notify('You need to login first', { globalPosition: 'top center', className: 'warning' });
        }
    });
}

AddToWishlist = (id, e) => {
    e.stopPropagation();
    var btn = e.target;
    var act;
    if (btn.nodeName==='SPAN') {
        btn = btn.parentNode;
    }
    if (btn.classList.contains("btn-light") || btn.classList.contains("card-link-secondary")) {
            btn.classList.remove("btn-light");
            btn.classList.add("btn-danger");
            act = "Add";
        }
        else {
            btn.classList.remove("btn-danger");
            btn.classList.add("btn-light");
            act = "Delete";
    }
    $.ajax({
        type: 'get',
        url: "Wishlists/AddOrRemove",
        traditional: true,
        data: { "id": id, "act": act }
    });
}

function prevent(e) {
    e.preventDefault();
}

removeFromCart = (id, e) => {
    e.preventDefault();
    $.ajax({
        type:"get",
        traditional: true,
        url: "Carts/RemoveFromCart",
        data: { "id": id },
        success: function (res) {
            $("#newCart").html(res)
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function IncOrDec(e, price, status, id) {
    var Temp = document.getElementById("subTotal");
    //var Total = document.getElementById("total");
    var myInput = e.target.parentNode.querySelector('input[type=number]');
    var flag=false;

    if (status == "minus") {
        if (myInput.value > 1) {
            e.target.parentNode.querySelector('input[type=number]').stepDown();
            Temp.innerHTML = (parseFloat(Temp.innerHTML) - price);
            //Total.innerHTML = (parseFloat(Total.innerHTML) - price);
            flag = true;
        }
    }

    else {
        if (parseInt(myInput.value) < parseInt(myInput.max)) {
            e.target.parentNode.querySelector('input[type=number]').stepUp();
            Temp.innerHTML = (parseFloat(Temp.innerHTML) + price);
            //Total.innerHTML = (parseFloat(Total.innerHTML) + price);
            flag = true;
        }
    }
    if (flag) {

        fetch("http://shirleyomda-001-site1.etempurl.com/odata/Carts(" + id + ")",
              {
                  method: 'PATCH',
                  headers: {
                 'Content-Type': 'application/json'
            },
                  body: JSON.stringify({ "quantity": parseInt(myInput.value) }),
        });
    }
}

MoveToWishlistFromCart = (sID, pID, e) => {
    AddToWishlist(pID, e);
    removeFromCart(sID, e);
    $.notify('Moved to wishlist successfuly', { globalPosition: 'top center', className: 'success' });
}

function changeTotal(e){
    const shippingCost = e.target.options[e.target.selectedIndex].value;
    const tempTotal = $("#tempTotal").html();
    //console.log($("#tempTotal").html());
    $("#shipping").html(shippingCost);
    $("#totalt").html((parseFloat(shippingCost) + parseFloat(tempTotal)));
}

function ConfirmPayment(e) {
    //e.preventDefault();
    let total = parseFloat($("#subTotal").html());
    $.ajax({
        type: 'get',
        url: "Carts/Purchase",
        traditional: true,
        data: { "total": total },
        success: function (res) {
            $('#checkout-modal .modal-body').html(res);
            $('#checkout-modal').modal('show');
        },
        error: function (err) {
            console.log(err);
        }
    });
    //Modal carry all things(payment details of address)
}

function checkOut(e) {
    //Address => Payment => Bill => BillProduct
    //e.preventDefault();
    //Address Table Data
    let shippingList = document.getElementById("shippingAddress");
    let shippingID = shippingList.options[shippingList.selectedIndex].getAttribute("id");
    let phone = parseInt($("#phone").val());
    let addressDetails = $("#address").val();
    //Bill
    //Payment
    var payMethod = $("input[name='pay']:checked").attr('id');
    let tempTotal = parseFloat($("#tempTotal").html());
    let total = parseFloat($("#totalt").html());


    $.ajax({
        url: "Bills/Index",
        type: 'get',
        traditional: true,
        data: { "shippingID": shippingID, "phone": phone, "addressDetails": addressDetails, "paymentID": payMethod, "tempTotal": tempTotal, "total": total },
        success: function (res) {
            CloseModel();
            $.notify('Order Submitted Successfully', { globalPosition: 'top center', className: 'success' });

            //window.location.href = 'Url.Action("BillDetails", "Bills", new { id: res.id } )';
            window.location.href = 'Bills/BillDetails/'+res.id;

            //$("#newCart").html(res);

            //Search For passing Form Mn Here !!
            //Render partial view or full view ????
        }
    });
}


