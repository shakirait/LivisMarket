$(document).ready(function () {
    CreditCard.init();
});

var CreditCard = {
    init: function () {
        $(document)
            .on('click', '.jsAddCreditCart', CreditCard.getTokenForCreditCard)
            .on('click', '.jsRemoveCreditCard', CreditCard.removeCreditCart)
    },  
    clearCreditCard: function (form) {
        $("#cardNumber", form).val('');
        $("#month", form).val('');
        $("#year", form).val('');
        $("#firtName", form).val('');
        $("#lastName", form).val('');
    },
    getTokenForCreditCard: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var cardNumber = $("#cardNumber", form).val().replace(/-/g, '');
        var cvvCode = $("#cardCVV", form).val();
        var cardHolderFirstName = $("#firtName", form).val();
        var cardHolderLastName = $("#lastName", form).val();
        var expireMonth = $("#month", form).val();
        var expireYear = $("#year", form).val();
        Multipayment.init($("#shopId").val());
        Multipayment.getToken(
            { cardno: cardNumber, expire: expireYear + expireMonth, securitycode: cvvCode, holdername: cardHolderFirstName + ' ' + cardHolderLastName, tokennumber: null }
            , "CreditCard.addCreditCard"
        );
    },
    addCreditCard: function (response) {        
        var form = $("#newCreditCardForm");
        var formContainer = $("#" + form.data("container"));
        var cardNumber = response.tokenObject.maskedCardNo;
        var month = $("#month", form).val();
        var year = $("#year", form).val();
        var firtName = $("#firtName", form).val();
        var lastName = $("#lastName", form).val();
        var cardToken = response.tokenObject.token;
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: { CardNumber: cardNumber, CardToken: cardToken, ExpireDateMonth: month, ExpireDateYear: year, FirstName: firtName, LastName: lastName },
            success: function (result) {
                if (result.success == false)
                {
                    alert(result.message);
                    return;
                }
                alert('Add credit cart successful!')

                CreditCard.clearCreditCard(form);
                formContainer.html($(result));
                formContainer.change();
            },
            error: function (xhr, status, error) {
            }
        });
    },
    removeCreditCart: function (e) {        
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var cardSeq = $("input[name='cardSeq']:checked", form).val();
        if (cardSeq == undefined) {
            alert("Please select card to remove");
            return;
        }
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: { cardSeq: cardSeq },
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                    return;
                }
                alert('Remove credit cart successful!')
                formContainer.html($(result));
                formContainer.change();
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