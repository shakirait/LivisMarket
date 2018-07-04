$(document).ready(function () {
    ShippingAddressSelection.init();
});

var ShippingAddressSelection = {
    init: function () {
        $(document)
            .on('click', '.jsNextShippingPage', ShippingAddressSelection.NextPage)
            .on('click', '.jsBackShippingPage', ShippingAddressSelection.BackPage)
            .on('click', '.jsUpdateShippingAddress', ShippingAddressSelection.updateShippingAddress)
            .on('click', '.jsChangeShippingAddress', ShippingAddressSelection.changeShippingAddress)
            .on('click', '.jsDeleteShippingAddress', ShippingAddressSelection.deleteShippingAddress)
            .on('click', '.jsAddNewShippingAddress', ShippingAddressSelection.addNewShippingAddress)
    },
    updateShippingAddress: function (e) {

        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var addressId = $("#AddressId", form).val();
        var postcode1 = $("#PostCode1", form).val();
        var postcode2 = $("#PostCode2", form).val();
        var state = $("#State", form).val();
        var city = $("#City", form).val();
        var street = $("#Street", form).val();

        var firstName = $("#FirstName", form).val();
        var lastName = $("#LastName", form).val();
        var phonetic1 = $("#Phonetic1", form).val();
        var phonetic2 = $("#Phonetic2", form).val();
        var phoneNumber1 = $("#PhoneNumber1", form).val();
        var phoneNumber2 = $("#PhoneNumber2", form).val();
        var phoneNumber3 = $("#PhoneNumber3", form).val();

        $.ajax({
            type: "POST",
            url: form[0].action,
            data: {
                addressId: addressId, postcode1: postcode1, postcode2: postcode2, state: state, city: city, street: street, firstName: firstName, lastName: lastName, phonetic1: phonetic1, phonetic2: phonetic2, phoneNumber1: phoneNumber1, phoneNumber2: phoneNumber2, phoneNumber3: phoneNumber3
            },
            success: function (result) {
                formContainer.html($(result));

                if (addressId != "")
                    alert('update address successful');
                else
                    alert('add address successful');

                ShippingAddressSelection.ShowModal(false);

                
            },
            error: function (xhr, status, error) {
            }
        });
    },
    changeShippingAddress: function (e) {

        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var addressId = $("#AddressId", form).val();
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: {
                addressId: addressId
            },
            success: function (result) {
                formContainer.html($(result));
                ShippingAddressSelection.ShowModal(true);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    addNewShippingAddress: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: {
            },
            success: function (result) {
                formContainer.html($(result));
                ShippingAddressSelection.ShowModal(true);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    deleteShippingAddress: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var addressId = $("#AddressId", form).val();
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: {
                addressId: addressId
            },
            success: function (result) {
                formContainer.html($(result));
            },
            error: function (xhr, status, error) {
            }
        });
    },
    ShowModal: function (bShow) {
        if (!bShow) {
            $("#addAdressModal").removeClass("show");
            $("#addAdressModal").hide();
        }
        else {
            $("#addAdressModal").addClass("show");
            $("#addAdressModal").show();
        }
    },
    NextPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var addressId = $("input:checked[name='adress']").val();
        $.ajax({
            type: "post",
            url: "/ShippingAddressSelectionPage/NextPage",
            data: { addressId: addressId},
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data;
                }
                else {
                    alert(result.message);
                }
            },
            error: function (xhr, status, error) {
            }
        });
    },
    BackPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        $.ajax({
            type: "get",
            url: "/ShippingAddressSelectionPage/BackPage",
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