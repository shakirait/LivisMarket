$(document).ready(function () {
    PurchaseOrderDetail.init();
});

var PurchaseOrderDetail = {
    init: function () {
        $(document)
            .on('click', '.jsPurchaseOrderSeeDetails', PurchaseOrderDetail.SeeDetails)
            .on('click', '.jsPurchaseOrderDetailBack', PurchaseOrderDetail.BackToPurchaseOrderList)
    },
    RenewPurchaseOrderToCart: function (e) {
        var self = this;
        e.preventDefault();
        $.ajax({
            type: "post",
            url: "/PurchaseOrderPage/AddToCart",
            data: {
                contentId: $(self).data('content-id'),
                poId: $(self).data('po-id')
            },
            success: function (result) {
                $('#purchaseOrderDetailRenewToCart').empty();
                var html = '';
                switch (result.status) {
                    case 'success':
                        html = '<div class="alert alert-success alert-dismissable">';
                        html += '<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
                        html += '<strong>Success!</strong> This purchase order is renewed to cart. Click <a href="' + result.cartLink + '">here</a> to view your cart.';
                        html += '</div>';
                        break;
                    case 'partial':
                        html = '<div class="alert alert-success alert-dismissable">';
                        html += '<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
                        html += '<strong>Partial success!</strong> This purchase order is partial renewed to cart. Click <a href="' + result.cartLink + '">here</a> to view your cart.';
                        html += '</div>';
                        html += '<div class="alert alert-warning alert-dismissable">';
                        html += '<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
                        result.messages.forEach(function (item) {
                            html += '<strong>Warning!</strong> ' + item;
                        });
                        html += '</div>';
                        break;
                    case 'failure':
                        html = '<div class="alert alert-danger alert-dismissable">';
                        html += '<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
                        html += '<strong>Danger!</strong> This purchase order cannot renew to cart.';
                        result.messages.forEach(function (item) {
                            html += '<strong>Danger!</strong> ' + item;
                        });
                        html += '</div>';
                        break;
                }
                $('#purchaseOrderDetailRenewToCart').html(html);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    SeeDetails: function (e) {
        var self = this;
        e.preventDefault();
        $.ajax({
            type: "get",
            url: $(self).data('url-action'),
            data: {
                poId: $(self).data('po-id'),
                postBackContentId: $(self).data('add-to-cart-post-back-content-id')
            },
            dataType: "html",
            success: function (result) {
                $("#tblPurchaseOrderInformation").hide();
                $('#purchaseOrderDetailSection').empty();
                $('#purchaseOrderDetailSection').html(result);
                $('.jsRenewPurchaseOrderToCart').unbind('click').on('click', PurchaseOrderDetail.RenewPurchaseOrderToCart);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    BackToPurchaseOrderList: function (e) {
        $("#tblPurchaseOrderInformation").show();
        $('#purchaseOrderDetailSection').empty();
    }
};