$(document).ready(function () {
    Address.init();
});

var Address = {
    init: function () {
        $(document)
            .on('click', '.jsUpdateAddress', Address.UpdateAddress)            
            .on('click', '.jsChangeAddress', Address.ChangeAddress)            
            .on('click', '.jsDeleteAddress', Address.DeleteAddress)       
            .on('click', '.jsAddNewAddress', Address.AddNewAddress)       
        
    },
    UpdateAddress: function (e) {
        
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var addressId = $("#AddressId", form).val();
        var postcode1 = $("#PostCode1", form).val();
        var postcode2 = $("#PostCode2", form).val();

        var firstName = $("#FirstName", form).val();
        var lastName = $("#LastName", form).val();
        var phonetic1 = $("#Phonetic1", form).val();
        var phonetic2 = $("#Phonetic2", form).val();
        var phoneNumber1 = $("#PhoneNumber1", form).val();
        var phoneNumber2 = $("#PhoneNumber2", form).val();
        var phoneNumber3 = $("#PhoneNumber3", form).val();

        var state = $("#State", form).val();
        var city = $("#City", form).val();
        var street = $("#Street", form).val();
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: {
                addressId: addressId, postcode1: postcode1, postcode2: postcode2, state: state, city: city, street: street, firstName: firstName, lastName: lastName, phonetic1: phonetic1, phonetic2: phonetic2, phoneNumber1: phoneNumber1, phoneNumber2: phoneNumber2, phoneNumber3: phoneNumber3
            },
            success: function (result) {
                formContainer.html($(result));

                if (addressId != "")
                    alert('update address successful')
                else
                    alert('add address successful')

                $("#editDeliveryAddress").addClass("active");
                $("#addAdress").removeClass("active");
            },
            error: function (xhr, status, error) {
            }
        });
    },
    ChangeAddress: function (e) {

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

                $("#editDeliveryAddress").removeClass("active");
                $("#addAdress").addClass("active");
            },
            error: function (xhr, status, error) {
            }
        });
    },
    AddNewAddress: function (e) {

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

                $("#editDeliveryAddress").removeClass("active");
                $("#addAdress").addClass("active");
            },
            error: function (xhr, status, error) {
            }
        });
    },
    DeleteAddress: function (e) {

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
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};