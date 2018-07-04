$(document).ready(function () {
    PaymentMethodSelection.init();
});

var PaymentMethodSelection = {
    init: function () {
        $(document)
            .on('click', '.jsBackPaymentPage', PaymentMethodSelection.backPage)
            .on('click', '.jsChangePaymentMethod', PaymentMethodSelection.changePaymentMethod)
            .on('click', '.jsBackPaymentMethodPage', PaymentMethodSelection.backPaymentMethodPage)
            .on('click', '.jsNextPaymentWithConvenienceStore', PaymentMethodSelection.nextPaymentWithConvenienceStore)
            .on('click', '.jsNextPaymentWithCreditCard', PaymentMethodSelection.nextPaymentWithCreditCard)
            .on('click', '.jsNextPaymentWithCreditCardWithoutMember', PaymentMethodSelection.getTokenForCreditCard);
        PaymentMethodSelection.changePaymentMethodVisibility(this);
    },  
    changePaymentMethodVisibility: function (e) {
        //var form = $(this).closest("form");
        var paymentMethod = $("input[name='paymentMethod']:checked").val();
        if (paymentMethod == undefined || paymentMethod == "") {
            return;
        }
        
        if (paymentMethod == "CreditCard" || paymentMethod == "ConvenienceStore") {
            $("#paymentMethod").hide();
            $("#" + paymentMethod).show();
            return false;
        }
    }, 
    changePaymentMethod: function (e) {
        //var form = $(this).closest("form");
        var paymentMethod = $("input[name='paymentMethod']:checked").val();
        if (paymentMethod == undefined || paymentMethod == "")
        {
            return;
        }
        else if (paymentMethod == "CashOnDelivery") {
            PaymentMethodSelection.nextPaymentWithCOD(e);
        }
        else if (paymentMethod == "AccountReceivable") {
            PaymentMethodSelection.nextPaymentWithAccountReceivable(e);
        }
        else {
            $("#paymentMethod").hide();
            $("#" + paymentMethod).show();
            return false;
        }        
    },  
    backPaymentMethodPage: function (e) {
        e.preventDefault();
        $("#paymentMethod").show();
        $("#AccountReceivable").hide();
        $("#ConvenienceStore").hide();
        $("#CreditCard").hide();
        $("#AccountReceivable").hide();
    },
    nextPaymentWithCOD: function (e) {
        //e.preventDefault();
        
        var deliveryDate = $("#deliveryDate").val();
        var deliveryTime = $("#deliveryTime").val();
        var paymentMethodId = $("#hiddCashOnDeliveryPaymentMethodId").val();
        $.ajax({
            type: "post",
            url: "/PaymentMethodSelectionPage/NextPageWithCOD",
            data: { deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId},
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data;
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    nextPaymentWithAccountReceivable: function (e) {
        //e.preventDefault();

        var deliveryDate = $("#deliveryDate").val();
        var deliveryTime = $("#deliveryTime").val();
        var paymentMethodId = $("#hiddCashOnDeliveryPaymentMethodId").val();
        $.ajax({
            type: "post",
            url: "/PaymentMethodSelectionPage/NextPageWithAccountReceivable",
            data: { deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId },
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data;
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    getTokenForCreditCard: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var cardNumber = $("#cardNumber", form).val().replace(/-/g, '');
        var cvvCode = $("#cardCvvCode", form).val();
        var cardHolderFirstName = $("#cardHolderFirstName", form).val();
        var cardHolderLastName = $("#cardHolderLastName", form).val();
        var expireMonth = $("#expireMonth", form).val();
        var expireYear = $("#expireYear", form).val();
        Multipayment.init($("#shopId").val());
        Multipayment.getToken(
            { cardno: cardNumber, expire: expireYear + expireMonth, securitycode: cvvCode, holdername: cardHolderFirstName + ' ' + cardHolderLastName , tokennumber: null }
            , "PaymentMethodSelection.nextPaymentWithCreditCardWithoutMember"
        );  
    },
    nextPaymentWithCreditCardWithoutMember: function (response) {
        if (response.resultCode != '000')
        {
            window.alert('An error occurred during purchase processing. Please re-try or chose another payment method! ');
        }
        else {
            var deliveryDate = $("#deliveryDate").val();
            var deliveryTime = $("#deliveryTime").val();
            var paymentMethodId = $("#hiddPaymentMethodId").val();
            var cardHolderFirstName = $("#cardHolderFirstName").val();
            var cardHolderLastName = $("#cardHolderLastName").val();
            var cardNo = response.tokenObject.maskedCardNo;
            var cardToken = response.tokenObject.token;
            $.ajax({
                type: "post",
                url: "/PaymentMethodSelectionPage/NextPageWithCreditCardWithoutMember",
                data: { cardNumber: cardNo, cardToken: cardToken, cardHolderFirstName: cardHolderFirstName, cardHolderLastName: cardHolderLastName,deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId},
                success: function (result) {
                    if (result.success == false) {
                        alert(result.message);
                        return false;
                    }
                    document.location = result.data;
                    
                },
                error: function (xhr, status, error) {
                }
            });
        }
        
    },
    nextPaymentWithCreditCard: function (e) {
        e.preventDefault();
        var form = $('.jsCheckoutForm');
        var cardSeq = $("input[name='cardSeq']:checked").val();

        var deliveryDate = $("#deliveryDate").val();
        var deliveryTime = $("#deliveryTime").val();
        var paymentMethodId = $("#hiddPaymentMethodId").val();

        $.ajax({
            type: "post",
            url: "/PaymentMethodSelectionPage/NextPageWithCreditCard",
            data: { cardSeq: cardSeq, deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId },
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                document.location = result.data;
                
            },
            error: function (xhr, status, error) {
            }
        });
    },
    nextPaymentWithConvenienceStore: function (e) {
        e.preventDefault();
        //var form = $(this).closest("form");
        var convenienceStoreId = $("input[name='convenienceStore']:checked").val();

        var deliveryDate = $("#deliveryDate").val();
        var deliveryTime = $("#deliveryTime").val();
        var paymentMethodId = $("#hiddPaymentMethodId").val();
        $.ajax({
            type: "post",
            url: "/PaymentMethodSelectionPage/NextPageWithConvenienceStore",
            data: { convenienceStoreId: convenienceStoreId, deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId },
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                document.location = result.data;
            },
            error: function (xhr, status, error) {
            }
        });
    },    
    backPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        $.ajax({
            type: "get",
            url: "/PaymentMethodSelectionPage/BackPage",
            data: {},
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                document.location = result.data;
            },
            error: function (xhr, status, error) {
            }
        });
    },    
    nextPaymentWithAccountReceivable: function (e) {
        e.preventDefault();
        //var form = $(this).closest("form");
        var deliveryDate = $("#deliveryDate").val();
        var deliveryTime = $("#deliveryTime").val();
        var paymentMethodId = $("#hiddAccountReceivablePaymentMethodId").val();
        $.ajax({
            type: "post",
            url: "/PaymentMethodSelectionPage/NextPageWithAccountReceivable",
            data: { deliveryDate: deliveryDate, deliveryTime: deliveryTime, paymentMethodId: paymentMethodId },
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                document.location = result.data;
            },
            error: function (xhr, status, error) {
            }
        });
    },
    backPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        $.ajax({
            type: "get",
            url: "/PaymentMethodSelectionPage/BackPage",
            data: {},
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                document.location = result.data;
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