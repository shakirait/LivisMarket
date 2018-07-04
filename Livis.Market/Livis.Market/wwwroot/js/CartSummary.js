$(document).ready(function () {
    CartSummary.init();
});

var CartSummary = {
    init: function () {
        $(document)
            .on('click', '.jsNextCartSummaryPage', CartSummary.NextPage)
            .on('click', '.jsBackCartSummaryPage', CartSummary.BackPage)
            .on('click', '.jsBackPaymentMethodCartSummary', CartSummary.backPaymentMethodCartSummary)
            .on('click', '.jsNextPaymentMethodCartSummary', CartSummary.nextPaymentMethodCartSummary)
    },
    backPaymentMethodCartSummary: function (e) {
        e.preventDefault();
        $('#changePaymentModal').modal('hide');
    },
    nextPaymentMethodCartSummary: function (e) {
        e.preventDefault();
        var paymentMethod = $("input[name='paymentMethod']:checked").val();
        document.location = $(this).attr("href") + "?PaymentMethod=" + paymentMethod;
    },
    NextPage: function (e) {
        e.preventDefault();
        $('.jsNextCartSummaryPage').prop('disabled', true);
        var form = $(this).closest("form");
        $.ajax({
            type: "post",
            url: "/CartSummaryPage/NextPage",
            data: {},
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data + "?PONumber=" + result.param;
                }
                else {
                    alert(result.message);
                    $('.jsNextCartSummaryPage').prop('disabled', false);
                }
            },
            error: function (xhr, status, error) {
                $('.jsNextCartSummaryPage').prop('disabled', false);
            }
        });
    },
    BackPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        $.ajax({
            type: "get",
            url: "/CartSummaryPage/BackPage",
            data: {},
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data;
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};