$(document).ready(function () {
    PetRegistrationpage.init();
});

var PetRegistrationpage = {
    init: function() {
        $(document)
            .on('click', '#savePetInformation', PetRegistrationpage.addNewPetInforamtion)
            .on('click', '#addNewPet,#EditPet', PetRegistrationpage.addNewPetForm);

    },
    initEditForm: function () {
       
        
        $('#otherPetType').each(function () {
            var $this = $(this),
                numberOfOptions = $(this).children('option').length;

            $this.addClass('select-hidden');
            $this.wrap('<div class="select-input"></div>');
            $this.after('<div class="form-control"></div>');

            var $styledSelect = $this.next('div.form-control');
            $styledSelect.text($this.children('option').eq(0).text());

            var $list = $('<ul />', {
                'class': 'select-options'
            }).insertAfter($styledSelect);

            for (var i = 0; i < numberOfOptions; i++) {
                $('<li />', {
                    text: $this.children('option').eq(i).text(),
                    rel: $this.children('option').eq(i).val()
                }).appendTo($list);
            }

            var $listItems = $list.children('li');

            $styledSelect.click(function (e) {
                e.stopPropagation();
                $('div.form-control.active').not(this).each(function () {
                    $(this).removeClass('active').next('ul.select-options').hide();
                });
                $(this).toggleClass('active').next('ul.select-options').toggle();
            });

            $listItems.click(function (e) {
                e.stopPropagation();
                $styledSelect.text($(this).text()).removeClass('active');
                $this.val($(this).attr('rel'));
                $list.hide();
                //console.log($this.val());
            });

            $(document).click(function () {
                $styledSelect.removeClass('active');
                $list.hide();
            });

        });
        $("#otherPetType").next('.form-control').addClass('select-sm').parents('.select-input').addClass('select-input-sm')

        
            $("#otherPetType").next('.form-control').addClass('select-default')
        
        $('#falseinput1').click(function() {
            $("#fileinput1").click();
        });
        $('#falseinput2').click(function() {
            $("#fileinput2").click();
        });
        $('#petRegistrationInformation #datetimepicker1').datetimepicker({
            viewMode: 'days',
            format: 'MM/DD/YYYY'
        });
        $('#petRegistrationInformation  #fileinput1').change(function() {
            $('#selected_filename1').text($('#fileinput1')[0].files[0].name);

        });
        $('#petRegistrationInformation  #fileinput2').change(function() {
            $('#selected_filename2').text($('#fileinput2')[0].files[0].name);

        });

        $("#btnDeletePicture1").click(function() {
            PetRegistrationpage.changeToUploadPicture(1);
            $('#falseinput1').click(function () {
                $("#fileinput1").click();
            });
            $('#petRegistrationInformation  #fileinput1').change(function () {
                $('#selected_filename1').text($('#fileinput1')[0].files[0].name);

            });
        });
        $("#btnDeletePicture2").click(function () {
            PetRegistrationpage.changeToUploadPicture(2);
            $('#falseinput2').click(function () {
                $("#fileinput2").click();
            });
            $('#petRegistrationInformation  #fileinput2').change(function () {
                $('#selected_filename2').text($('#fileinput2')[0].files[0].name);

            });
        });
    },
    addNewPetForm: function(e) {
        var container = $("#Pet_Information_Container");
       
        var url = $(this).data("url_action");
        if ($(this).data("content_id") != null && $(this).data("content_id") != undefined)
            url += "?contentid=" + $(this).data("content_id");
        $.ajax({
            type: "Get",
            url: url,
            dataType: "html",
            success: function(result) {
                $(container).html(result);
                PetRegistrationpage.initEditForm();
            },
            error: function(xhr, status, error) {
                alert('Error, please try later!');
            }
        });
    },
    addNewPetInforamtion: function(e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var token = $("#__AjaxAntiForgeryForm input").val();
        var formData = new FormData();
        formData.append("__RequestVerificationToken", token);
        formData.append("petblockid", $("#petBlockId", form).val());
        formData.append("nickName", $("#nickName", form).val());
        formData.append("petType",
            $('input[name=radiopetType]:checked', form).val() != null &&
            $('input[name=radiopetType]:checked', form).val() != undefined
            ? $('input[name=radiopetType]:checked', form).val()
                : "");
        formData.append("selectedPetType",$('#otherPetType option:selected', form).val());
        formData.append("petBirthDate", $("#datetimepicker1", form).val());
        formData.append("isUsedAntinol", $('input[name=radioAntinol]:checked', form).val());
        formData.append("petHealthStatus",
            $('input[name=radioHealth]:checked', form).val() != null &&
            $('input[name=radioHealth]:checked', form).val() != undefined
            ? $('input[name=radioHealth]:checked', form).val()
                : '');
        formData.append("petHealthStatusDescription", $('#txtPetHealth', form).val());
        formData.append("picture1",
            $('#fileinput1') != null && $('#fileinput1')[0]!=null &&
            $('#fileinput1')[0].files[0] != null &&
            $('#fileinput1')[0].files[0] != undefined
            ? $('#fileinput1')[0].files[0]
                : null);
        formData.append("isHavingPermisionForPicture1", $("#ckbIsHavingPermisionForPicture1", form).prop('checked'));
        formData.append("picture2",
            $('#fileinput2') != null && $('#fileinput2')[0] != null &&
            $('#fileinput2')[0].files[0] != null &&
            $('#fileinput2')[0].files[0] != undefined
            ? $('#fileinput2')[0].files[0]
                : null);
        formData.append("isHavingPermisionForPicture2", $("#ckbIsHavingPermisionForPicture2", form).prop('checked'));
        if ($("#isRemovePicture1", form) != null && $("#isRemovePicture1", form)!=undefined && $("#isRemovePicture1", form).val() == "true") {
            formData.append("IsRemovePicture1", true);
        } else {
            formData.append("IsRemovePicture1", false);
        }
        if ($("#isRemovePicture2", form) != null && $("#isRemovePicture2", form) != undefined && $("#isRemovePicture2", form).val() == "true") {
            formData.append("isRemovePicture2", true);
        } else {
            formData.append("isRemovePicture2", false);
        }
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                alert(result.message);
                if (result.success == true) {
                    window.location.href = window.location.href;
                }
            },
            error: function(xhr, status, error) {
                alert('Add Failed!');
            }
        });
    },
    changeToUploadPicture: function (pictureNo) {
        var strVar = "";
        strVar += "<input type=\"hidden\" id=\"isRemovePicture" + pictureNo+"\" value=\"true\">";
        strVar += "<div class=\"d-md-flex\">";
        strVar += "                                <div class=\"mr-5\">";
        strVar += "                                    <img src=\"\/Static\/img\/upload-image.jpg\" alt=\"\" class=\"mb-3\" >";
        strVar += "                                <\/div>";
        strVar += "                                <div class=\"d-flex flex-column justify-content-between\">";
        strVar += "                                    <div>";
        strVar += "                                        <label class=\"custom-control custom-checkbox mb-md-4 custom-checkbox-blue\">";
        strVar += "                                            <input type=\"checkbox\" id=\"ckbIsHavingPermisionForPicture"+pictureNo+"\" class=\"custom-control-input\">";
        strVar += "                                            <span class=\"custom-control-indicator\"><\/span>";
        strVar += "                                            <span class=\"custom-control-description\">使用の許可<\/span>";
        strVar += "                                        <\/label>";
        strVar += "                                        <div class=\"lead-md text-center text-md-left\">";
        strVar += "                                            あなたがしたくない場合はこれを解除してください";
        strVar += "                                            <br> この画像と詳細を公開する";
        strVar += "                                        <\/div>";
        strVar += "                                    <\/div>";
        strVar += "                                <\/div>";
        strVar += "                            <\/div>";
        strVar += "                            <div class=\"d-md-flex justify-content-between pb-5 pb-md-0\">";
        strVar += "                                <div class=\"mb-4 mb-md-0\">";
        strVar += "                                    <input name=\"fileinput\" id=\"fileinput" + pictureNo+"\" type=\"file\" value=\"アップロードする写真を選択\" style=\"display: none\">";
        strVar += "                                    <button type=\"button\" id=\"falseinput" + pictureNo +"\" class=\"btn btn-primary btn-sm \">アップロードする写真を選択<\/button>";
        strVar += "                                    <span id=\"selected_filename" + pictureNo +"\">No file chosen<\/span>";
        strVar += "                                <\/div>";
        strVar += "                                <a href=\"\" class=\"btn btn-primary d-block registartion-box-btn\">アップロード<\/a>";
        strVar += "                            <\/div>";

        $("#div_picture" + pictureNo).html(strVar);
    },
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
};