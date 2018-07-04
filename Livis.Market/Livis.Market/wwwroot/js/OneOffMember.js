$(document).ready(function () {
    OneOffMember.init();
});

var OneOffMember = {
    init: function () {
        $(document)
            .on('click', '.jsNextOneOffMemberPage', OneOffMember.NextPage)
            .on('click', '.jsBackOneOffMemberPage', OneOffMember.BackPage);
    },    
    NextPage: function (e) {
        e.preventDefault();
        var $form = $(this).closest("form");
        $.ajax({
            type: "post",
            url: "/OneOffMemberPage/NextPage",
            data: $form.serialize(),
            success: function (result) {
                if (result.success == true) {
                    document.location = result.data;
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
            url: "/OneOffMemberPage/BackPage",
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