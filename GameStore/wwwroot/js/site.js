// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

//Size of picture.
$('#image-file').bind('change', function () {
    if (this.files[0].size / 1024 / 1024 > 10) {
        $('#submit').hide();
    }
    else {
        $('#submit').show();
    }
});

//It doesn't work that way at all
$('#delete').submit(function (event) {
    event.preventDefault();

    let id = $('#id').val();

    $.ajax({
        url: '/Products/Delete?id=' + id,
        method: 'DELETE',
        error: function (data) {
            location.href = '/';
        },
    }); 
});

//Add to cart.
$("#addForm").submit(function (event) {
    event.preventDefault();
    $.post('/Cart/AddProductToCart', $('#addForm').serialize(),
function (data) {
    $('#result').html(data);
    });
});

//Work with search.
function searchProducts() {
    var name = $('#search').val();
    name = encodeURIComponent(name);

    $.ajax({
        type: "POST",
        url: 'Products/Search?name=' + name,
        success: function (products) {
            document.getElementById("results").innerHTML = products;
        }
    });

    if (name == '') {
        document.getElementById('reset').hidden = true;
    }
    else {
        document.getElementById('reset').hidden = false;
    }

}

function clearData() {
    $('#search').val('');
    searchProducts();
}