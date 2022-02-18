function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

const cookieVal = getCookie("cartProduct");
let ProductIds = cookieVal !== "" ? cookieVal.split("-") : [];
$(".btn-cart").click(function () {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Məhsul uğurla saxlanıldı',
        showConfirmButton: false,
        timer: 1500
    })
    const productId = $(this).attr("pro-id");
    ProductIds.push(productId);
    setCookie("cartProduct", ProductIds.join("-"), 1)

})

$(".inc").on("click", function () {
    let totalPrice = 0;
    const InputVal = parseFloat($(this).parent().find("input").val())
    const productId = $(this).parent().attr("pro-id")
    ProductIds = ProductIds.filter(x => x !== productId)
    for (var i = 0; i < InputVal; i++) {
        ProductIds.push(productId)
    }
    setCookie("cartProduct", ProductIds.join("-"), 1)
    const Price = parseFloat($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".product-total span").text("$" + Price * InputVal)
    let subtotal = $(".product-total span").text().split("$")
    for (var i = 1; i < subtotal.length; i++) {
        totalPrice += parseFloat(subtotal[i]);
    } 
    $(".cart-area .grand-total-wrap .grand-total-content .grand-total span").html(`$${totalPrice}`)

});

$(".dec").on("click", function () {
    let totalPrice = 0;
    const InputVal = Number($(this).parent().find("input").val())
    const productId = $(this).parent().attr("pro-id")
    ProductIds = ProductIds.filter(x => x !== productId)
    for (var i = 0; i < InputVal; i++) {
        ProductIds.push(productId)
    }
    setCookie("cartProduct", ProductIds.join("-"), 1)
    const Price = parseFloat($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".product-total span").text("$" + Price * InputVal)
    let subtotal = $(".product-total span").text().split("$")
    for (var i = 1; i < subtotal.length; i++) {
        totalPrice += parseFloat(subtotal[i])
    }
    $(".cart-area .grand-total-wrap .grand-total-content .grand-total span").html(`$${totalPrice}`)
})










$(".product-remove").click(function () {
    const productId = $(this).attr("pro-id");
    ProductIds = ProductIds.filter(x => x !== productId)
    setCookie("cartProduct", ProductIds.join("-"), 1)
    $(this).parents("tr").remove()

    if ($(".cart-area table  tbody tr").length == 0){
    $(".cart-area .my-cart-area").html('<p class="alert alert-danger">Səbətdə Məhsul Yoxdur</p>')
}
})