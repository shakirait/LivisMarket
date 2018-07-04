
var CustomerKey = "LivisMarketCustomerKey";
var Cart = {
    init: function () {
        $(document)
            .on('click', '.jsAddToCart', Cart.addCartItem)
            .on('click', '.jsRemoveToCart', Cart.removeCartItem)
            .on('focusout', '.jsQuantity', Cart.updateQuantity);
        Cart.mergeMiniCart(null);
    },
    updateQuantity: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var sku = $("#sku", form).val();
        var variantId = $("#variantId", form).val();
        var customerId = localStorage.getItem(CustomerKey);
        var qty = $(this).val();
        var bkQuantity = $("#bkQuantity", form).val();
        if (qty <= 0) {
            alert("Please enter quantity order greater than 0");
            $(this).val(Math.trunc(bkQuantity));
        } else {
            $.ajax({
                type: "POST",
                url: form[0].action,
                data: { customerId: customerId, sku: sku, variantId: variantId, quantity: qty },
                success: function (result) {
                    if (result.success === true) {
                        if (result.message === "") {
                            location.reload();
                        } else {
                            $("#bkQuantity", form).val(result.quantity);
                            form.closest('td').next().find('h3.subtitle').text(result.message);
                            $("#totalPriceCart").text(result.total);
                        }
                        Cart.updateMiniCart(e);
                    } else {
                        alert(result.message);
                    }
                },
                error: function (xhr, status, error) {
                }
            });
        }
    },
    removeCartItem: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var sku = $("#sku", form).val();
        var variantId = $("#variantId", form).val();
        var customerId = localStorage.getItem(CustomerKey);
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: { customerId: customerId, sku: sku, variantId: variantId },
            success: function (result) {
                if (result.success === true) {
                    if (result.message === "reload") {
                        location.reload();
                    } else {
                        form.closest('tr.cart-row').remove();
                        $("#totalPriceCart").text(result.message);
                    }
                    Cart.updateMiniCart(e);
                }
                else
                    alert(result.message);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    shippingCart: function (e) {
        var customerId = localStorage.getItem(CustomerKey);
        $.ajax({
            type: "POST",
            url: '/Shopping/ViewShippingAddress',
            data: { customerId: customerId },
            success: function (result) {
                if (result.message !== undefined && result.message !== null) {
                    alert(result.message);
                } else {
                    $("#shippingCart").html(result);
                    var customerId = localStorage.getItem(CustomerKey);
                    if (customerId !== undefined && customerId != null && customerId !== "")
                        $("#customerId").val(customerId);
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    previewCart: function (e) {
        var customerId = localStorage.getItem(CustomerKey);
        $.ajax({
            type: "POST",
            url: '/Shopping/Cart',
            data: { customerId: customerId },
            success: function (result) {
                if (result.message !== undefined && result.message !== null) {
                    alert(result.message);
                } else {
                    $("#previewCart").html(result);
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    mergeMiniCart: function (e) {
        var customerId = localStorage.getItem(CustomerKey);
        $.ajax({
            type: "POST",
            url: '/Shopping/MergeCart',
            data: { customerId: customerId },
            success: function (result) {
                var formContainer = $("#MiniCart");
                formContainer.html($(result));
                formContainer.change();
            },
            error: function (xhr, status, error) {
            }
        });
    },
    updateMiniCart: function (e, addItemToCart = false) {
        if (e !== null)
            e.preventDefault();
        var customerId = localStorage.getItem(CustomerKey);
        $.ajax({
            type: "POST",
            url: '/Shopping/MiniCart',
            data: { customerId: customerId },
            success: function (result) {
 
                var formContainer = $("#MiniCart");
                formContainer.html($(result));
                formContainer.change();
                //get shopping cart link
                if (e !== null && addItemToCart === true) {
                    var redirectLink = $("#shoppingCartLink").attr("href");
                    if (redirectLink != "") {
                        var message = "Your product is added to the cart successfully. Would you like to checkout your cart ?";
                        var isWant = confirm(message);
                        if (isWant) {
                            window.location.replace(redirectLink);
                        }
                    }
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    addCartItem: function (e) { 
        var self = this;
        e.preventDefault();
        var form = $(this).closest("form");        
        var formContainer = $("#" + form.data("container"));
        var skuCode = $("#sku", form).val();
        var qty = $("#quantity", form).val();
        var variantKeys = $("#variantKeys").val();
        var variantId = skuCode;
        var customerId = localStorage.getItem(CustomerKey);
        qty = qty == undefined || qty == '' ? 1 : qty;
        if (qty <= 0) {
            alert("Please enter quantity order greater than 0");
        } else {
            if (variantKeys == null || variantKeys == undefined || variantKeys === "" || variantKeys === "[]") {
                variantId = skuCode;
            } else {
                var variantOptions = [];
                $('div[id ^= "text_radio"] > input:checked').each(function (index) {
                    var data = $(this).attr("data-nicelabel");
                    var obj = jQuery.parseJSON(data);
                    var variantName = obj.checked_text;
                    variantOptions.push(variantName);
                });
                var variantSelected = variantOptions.join('-');
                var variantObjs = jQuery.parseJSON(variantKeys);
                var variant = variantObjs.find(o => o.Name === variantSelected);
                variantId = variant.Id;
            }
            $.ajax({
                type: "POST",
                url: form[0].action,
                data: { customerId: customerId, sku: skuCode, variantId: variantId, quantity: qty },
                success: function (result) {
                    if (result.success === true) {
                        if (result.message === "") {
                            localStorage.setItem(CustomerKey, null);
                            Cart.updateMiniCart(e, true);
                        } else {
                            localStorage.setItem(CustomerKey, result.message);
                            Cart.updateMiniCart(e, true);
                        }
                    } else {
                        alert(result.message);
                    }
                },
                error: function (xhr, status, error) {
                }
            });
        }
    }
};