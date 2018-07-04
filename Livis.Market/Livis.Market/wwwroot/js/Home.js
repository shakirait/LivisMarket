$(document).ready(function () {
    Home.init();
});

var Home = {
    init: function () {
        $(document)
            .on('click', '.jsHomeRegisterVetCode', Home.registerVetCode)        
    }, 
    registerVetCode: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var vetCode = $("#vetCode", form).val();
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: { code: vetCode },
            success: function (result) {
                if (result.success == true) {
                    $("#homeVetCodeBlock").hide();
                }
                alert(result.message);
            },
            error: function (xhr, status, error) {
            }
        });
        return false;
    },
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};