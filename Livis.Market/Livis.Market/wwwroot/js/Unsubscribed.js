$(document).ready(function () {
    Unsubscribed.init();
});

var Unsubscribed = {
    init: function () {
        $(document)            
            .on('click', '.jsDeleteAccount', Unsubscribed.deleteAccount)
    },    
    deleteAccount: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var reason = $("#reason", form).val();        
        $.ajax({
            type: "post",
            url: form[0].action,
            data: { reason: reason  },
            success: function (result) {
                if (result.success == true) {
                    alert(result.message);
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
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};