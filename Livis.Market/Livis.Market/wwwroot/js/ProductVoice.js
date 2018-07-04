$(document).ready(function () {
    ProductVoice.init();
});

var ProductVoice = {
    init: function () {
        $(document)
            .on('click', '.jsSendProductVoiceForm', ProductVoice.sendForm)
            .on('click', '.jsCurrentPageLink', ProductVoice.pageLinkClick)
            .on('click', '.jsPrePageLink', ProductVoice.prePageLinkClick)
            .on('click', '.jsNextPageLink', ProductVoice.nextPageLinkClick);
        ProductVoice.loadMediaInfo();
    },    
    pageLinkClick: function (e) {
        var pageIndex = $(this).attr("content");
        ProductVoice.loadMediaInfoPage(pageIndex);

        $(".jsCurrentPageLink").removeClass("active");
        $(this).addClass("active");
        return false;
    },
    nextPageLinkClick: function (e) {
        var pageIndex = $(".jsCurrentPageLink.active").attr("content");

        var pageCount = $("#hdnPageCount").val();
        if (pageIndex == pageCount) {
            return false;
        }
        pageIndex++;
        ProductVoice.loadMediaInfoPage(pageIndex);

        $(".jsCurrentPageLink").removeClass("active");
        $(".jsCurrentPageLink[content='" + pageIndex + "']").addClass("active");
        return false;
    },
    prePageLinkClick: function (e) {
        var pageIndex = $(".jsCurrentPageLink.active").attr("content");

        if (pageIndex == 1) {
            return false;
        }
        pageIndex--;
        ProductVoice.loadMediaInfoPage(pageIndex);

        $(".jsCurrentPageLink").removeClass("active");
        $(".jsCurrentPageLink[content='" + pageIndex + "']").addClass("active");
        return false;
    },
    loadMediaInfo: function () {
        ProductVoice.loadMediaInfoPage(1); //start from 1
    },
    loadMediaInfoPage: function (pageIndex) {
        $(".jsMediaInfo").hide();
        var pageSize = $("#hdnPageSize").val();
        var minIndex = pageSize * (pageIndex - 1);
        var maxIndex = pageSize * pageIndex;
        $(".jsMediaInfo").each(function () {
            var currentIndex = $(this).attr("currentIndex");
            if (currentIndex >= minIndex && currentIndex < maxIndex) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        })
    },
    sendForm: function (e) {
        e.preventDefault();
        var agree = $("#Agree", form).prop('checked');
        if (agree == false) {
            alert("Please check agree before post.")
            return;
        }

        var form = $(this).closest("form");
        var token = $("#__AjaxAntiForgeryForm input").val();
        var formData = new FormData();
        formData.append("__RequestVerificationToken", token);
        formData.append("NameOfContributor", $("#NameOfContributor", form).val());
        formData.append("Email", $("#Email", form).val());
        formData.append("EmailRepeat", $("#EmailRepeat", form).val());
        formData.append("PhoneNumber", $("#PhoneNumber", form).val());
        formData.append("Prefecture", $("#Prefecture", form).val());
        formData.append("PetName", $("#PetName", form).val());
        formData.append("PetType", $("#PetType", form).val());
        formData.append("PetBreed", $("#PetBreed", form).val());
        formData.append("Rating", $("#Rating", form).val());
        formData.append("Comment", $("#Comment", form).val());
        formData.append("Rating", $("#Rating", form).val());
        formData.append("Age", $("#Age", form).val());

        formData.append("picture_upload_1",
            $('#picture_upload_1') != null && $('#picture_upload_1')[0] != null &&
                $('#picture_upload_1')[0].files[0] != null &&
                $('#picture_upload_1')[0].files[0] != undefined
                ? $('#picture_upload_1')[0].files[0]
                : null);

        formData.append("picture_upload_2",
            $('#picture_upload_2') != null && $('#picture_upload_2')[0] != null &&
                $('#picture_upload_2')[0].files[0] != null &&
                $('#picture_upload_2')[0].files[0] != undefined
                ? $('#picture_upload_2')[0].files[0]
                : null);
        formData.append("picture_upload_3",
            $('#picture_upload_3') != null && $('#picture_upload_3')[0] != null &&
                $('#picture_upload_3')[0].files[0] != null &&
                $('#picture_upload_3')[0].files[0] != undefined
                ? $('#picture_upload_3')[0].files[0]
                : null);
        formData.append("Agree", $("#Agree", form).prop('checked'));

        $.ajax({
            type: "POST",
            url: form[0].action,
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (result.success == false) {
                    alert(result.message);
                } else {
                    alert('Add Success!');
                    return
                }

            },
            error: function (xhr, status, error) {
                alert('Add Failed!');
            }
        });
    },
    openPage: function (evt, pageNumber) {
        ProductVoice.openTab(evt, "Page" + pageNumber);
    },
    openTab: function (evt, tabName) {
        // Declare all variables
        var i, tabcontent, tablinks;

        // Get all elements with class="tabcontent" and hide them
        tabcontent = document.getElementsByClassName("tabcontent");
        for (i = 0; i < tabcontent.length; i++) {
            tabcontent[i].style.display = "none";
        }

        // Get all elements with class="tablinks" and remove the class "active"
        tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++) {
            tablinks[i].className = tablinks[i].className.replace(" active", "");
        }

        // Show the current tab, and add an "active" class to the button that opened the tab
        document.getElementById(tabName).style.display = "block";
        evt.currentTarget.className += " active";
    },
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};