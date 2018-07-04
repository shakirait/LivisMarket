window.onload = function (e) {
    CreateProduct.init();
    $('#image-picker').on('show.bs.modal', function (e) {
        $(this).find('.btn-ok').attr('data-id', $(e.relatedTarget).data('id'));
        if ($(".src-images").find('option').length > 0) {
            $(".src-images").removeClass("hidden");
            $(".src-images").imagepicker();
        } else {
            $(".src-images").addClass("hidden");
        }
    });
    $('#image-picker').on('click', '.btn-ok', function (e) {
        var id = $(".btn-ok").attr("data-id");
        var src = $(".image_picker_selector").find('.thumbnail.selected').find('img').attr('src');
        $('#' + id).attr("src", src);
        $('#hidden_' + id).val(src);
        $('.btn-cancel').click();
    });
    $('div[id^="hiddenColor_"]').colorpicker({
        format: "hex"
    });
}
CreateProduct = {
    $hdImg: "#hdImg",
    fileIndex: 0,
    photos: [],
    partnerPhotos: [],
    variants: [],
    variantsTable: null,
    prices: [],
    pricesTable: null,
    init: function () {
        var self = this;
        $(document)
            .on('click', '.btn-primary', CreateProduct.validationForm)
            .on('click', '.addNewOption', CreateProduct.addNewOption)
            .on('click', '.removeoptiondetail', CreateProduct.removeOption)
            .on('click', '.generateVariant', CreateProduct.generateVariant);
        CreateProduct.initItems(1);
        CreateProduct.initItems(2);
        $("#rtxDescription").ckeditor();
        if ($('.options').find('.optiondetail').length > 0) {
            $('.optionValue').tagsinput({
            });
        }

        var variantValues = $("#variantKeys").val();
        if (variantValues != "") {
            var jsonObject = JSON.parse(variantValues);
            self.variants = [];
            for (var i = 0; i < jsonObject.length; i++) {
                var tempData = [];
                tempData.push(jsonObject[i].Id);
                tempData.push(jsonObject[i].Name);
                tempData.push(jsonObject[i].BlockUrl);
                tempData.push(jsonObject[i].Color);
                self.variants.push(tempData);
            }
            if (self.variantsTable == null || typeof self.variantsTable == 'undefined') {
                self.variantsTable = $("#variants").DataTable({
                    data: self.variants,
                    "pageLength": 4,
                    "info": false,
                    "bLengthChange": false,
                    "paging": false,
                    "searching": false,
                    columns: [
                        {
                            title: "Id",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var id = JsonResultRow[0];
                                return id + " <input type='hidden' class='variantId' name='Variants[" + meta.row +
                                    "].Id' value='" + id + "' />";
                            }
                        },
                        {
                            title: "Variant",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var name = JsonResultRow[1];
                                return name + " <input type='hidden' class='variantName' name='Variants[" + meta.row +
                                    "].Name' value='" + name + "' />";
                            }
                        },
                        {
                            title: "Image",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var id = JsonResultRow[0];
                                var image = JsonResultRow[2];
                                if (image == "" || image == undefined || image == null) {
                                    return " <input type='hidden' class='variantUrl' clas name='Variants[" + meta.row +
                                        "].BlockUrl' value='' id='hidden_" + id + "'/>" + "<img attr-btnUpload data-id='" + id +
                                        "' data-toggle='modal' id='" + id +
                                        "' data-target='#image-picker' class='img-thumbnail' src='/images/upload.png' style='height:50px; cursor: pointer'>";
                                } else {
                                    return " <input type='hidden' class='variantUrl' name='Variants[" + meta.row +
                                        "].BlockUrl' value='" + image + "' id='hidden_" + id + "'/>" + "<img attr-btnUpload data-id='" + id +
                                        "' data-toggle='modal' id='" + id +
                                        "' data-target='#image-picker' class='img-thumbnail' src='" + image + "' style='height:50px; cursor: pointer'>";
                                }
                                
                            },
                            "orderable": false,
                            "searchable": false
                        },
                        {
                            title: "Color",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var id = JsonResultRow[0];
                                var color = JsonResultRow[3];
                                return "<div id='hiddenColor_" + id + "' class='input-group colorpicker-component' title='Using input value'>"
                                    + "<input type='text' class='form-control input-sm' value ='" + color + "' name='Variants[" + meta.row +
                                    "].Color' />"
                                    + "<span class='input-group-addon'><i></i></span>"
                                    + "</div>";
                            },
                            "orderable": false,
                            "searchable": false
                        }
                    ],
                    initComplete: function () {
                        $('div[id^="hiddenColor_"]').colorpicker({
                            format: "hex"
                        });
                    }
                });
            } else {
                self.variantsTable.clear();
                self.variantsTable.rows.add(self.variants);
                self.variantsTable.draw();
                $('div[id^="hiddenColor_"]').colorpicker({
                    format: "hex"
                });
            }
        }

        var productPrices = $("#productPrices").val();
        if (productPrices != "") {
            var jsonObject = JSON.parse(productPrices);
            self.prices = [];
            for (var i = 0; i < jsonObject.length; i++) {
                var tempData = [];
                tempData.push(jsonObject[i].CustomerGroup);
                tempData.push(jsonObject[i].Price);
                tempData.push(jsonObject[i].CustomerGroupName);
                self.prices.push(tempData);
            }
            if (self.pricesTable == null || typeof self.pricesTable == 'undefined') {
                self.pricesTable = $("#prices").DataTable({
                    data: self.prices,
                    "pageLength": 4,
                    "info": false,
                    "bLengthChange": false,
                    "paging": false,
                    "searching": false,
                    columns: [
                        {
                            title: "Customer Group",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var customerGroup = JsonResultRow[0];
                                var name = JsonResultRow[2];
                                return name + " <input type='hidden' class='customerGroup' name='Prices[" + meta.row +
                                    "].CustomerGroup' value='" + customerGroup + "' />";
                            },
                            "orderable": false,
                            "searchable": false
                        },
                        {
                            title: "Price",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var price = JsonResultRow[1];
                                return "<input type='number' step='any' class='price' name='Prices[" + meta.row +
                                    "].Price' value='" + price + "' />";
                            },
                            "orderable": false,
                            "searchable": false
                        },
                        {
                            title: "Customer Group Name",
                            "visible": false,
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var name = JsonResultRow[2];
                                return " <input type='hidden' class='customerGroupName' name='Prices[" + meta.row +
                                    "].CustomerGroupName' value='" + name + "' />";
                            }
                        }
                    ],
                    initComplete: function () {
                    }
                });
            } else {
                self.pricesTable.clear();
                self.pricesTable.rows.add(self.prices);
                self.pricesTable.draw();
            }
        }
    },
    generateVariant: function (e) {
        var self = CreateProduct;
        if ($('.options').find('.optiondetail').length > 0) {
            var isValid = true;
            $('.options').find('.optiondetail').each(function (index) {
                var name = $(this).find('.optionName').val();
                var values = $(this).find('.optionValue').val();
                if (name == "" || values == "") {
                    isValid = false;
                }
            });
            if (isValid === false) {
                alert("Please fill in information all of fields of option.");
            } else {
                var listOfOptions = [];
                $('.options').find('.optiondetail').each(function (index) {
                    var optObj = {}
                    var name = $(this).find('.optionName').val();
                    var values = $(this).find('.optionValue').val();
                    optObj['Name'] = name;
                    optObj['Values'] = values;
                    listOfOptions.push(optObj);
                });
                $.ajax({
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    type: 'POST',
                    url: "/Product/GeneratingVariants",
                    data: JSON.stringify(listOfOptions),
                    success: function (data) {
                        var sku = $('#Sku').val();
                        self.variants = [];
                        
                        for (var i = 0; i < data.variants.length; i++) {
                            var tempData = [];
                            var index = i + 1;
                            tempData.push(sku + "-" + index);
                            tempData.push(data.variants[i]);
                            tempData.push("");
                            tempData.push("");
                            self.variants.push(tempData);
                        }
                        if (self.variantsTable == null || typeof self.variantsTable == 'undefined') {
                            self.variantsTable = $("#variants").DataTable({
                                data: self.variants,
                                "pageLength": 4,
                                "info": false,
                                "bLengthChange": false,
                                columns: [
                                    {
                                        title: "Id",
                                        "render": function (data,
                                            type,
                                            JsonResultRow,
                                            meta) {
                                            var id = JsonResultRow[0];
                                            return id + " <input type='hidden' class='variantId' name='Variants[" + meta.row +
                                                "].Id' value='" + id + "' />";
                                        }
                                    },
                                    {
                                        title: "Variant",
                                        "render": function (data,
                                            type,
                                            JsonResultRow,
                                            meta) {
                                            var name = JsonResultRow[1];
                                            return name + " <input type='hidden' class='variantName' name='Variants[" + meta.row +
                                                "].Name' value='" + name + "' />";
                                        }
                                    },
                                    {
                                        title: "Image",
                                        "render": function (data,
                                            type,
                                            JsonResultRow,
                                            meta) {
                                            var id = JsonResultRow[0];
                                            return " <input type='hidden' class='variantUrl' name='Variants[" + meta.row +
                                                "].BlockUrl' value='' id='hidden_" + id + "'/>" + "<img attr-btnUpload data-id='" + id +
                                                "' data-toggle='modal' id='" + id + "' data-target='#image-picker' class='img-thumbnail' src='/images/upload.png' style='height:50px; cursor: pointer'>";
                                        },
                                        "orderable": false,
                                        "searchable": false
                                    },
                                    {
                                        title: "Color",
                                        "render": function (data,
                                            type,
                                            JsonResultRow,
                                            meta) {
                                            var id = JsonResultRow[0];
                                            var color = JsonResultRow[3];
                                            return "<div id='hiddenColor_" + id + "' class='input-group colorpicker-component' title='Using input value'>"
                                                + "<input type='text' class='form-control input-lg' value ='" + color + "' name='Variants[" + meta.row +
                                                "].Color' />"
                                                + "<span class='input-group-addon'><i></i></span>"
                                                + "</div>";
                                        },
                                        "orderable": false,
                                        "searchable": false
                                    }
                                ],
                                initComplete: function () {
                                    $('div[id^="hiddenColor_"]').colorpicker({
                                        format: "hex"
                                    });
                                }
                            });
                        } else {
                            self.variantsTable.clear();
                            self.variantsTable.rows.add(self.variants);
                            self.variantsTable.draw();
                            $('div[id^="hiddenColor_"]').colorpicker({
                                format: "hex"
                            });
                        }
                    },
                    error: function () {
                        alert("There was error generating variants !");
                    }
                });
            }
        } else {
            alert("Please add options or characteristics of product before generating variants.");
        }
    },
    addNewOption: function (e) {
        var template = $(`<fieldset class='col-md-12 optiondetail'>
                            <legend align='right'><i title='remove' class='glyphicon glyphicon-remove removeoptiondetail'></i></legend>
                            <div class='control-group'>
                                <div class='form-group col-md-12'>
                                    <input type='text' class='optionName form-control' placeholder='Name' />
                                    <input type='text' class='optionValue form-control' data-role='tagsinput' placeholder='Values' />
                                </div>
                            </div>
                        </fieldset>`);
        $('.options').append(template);
        template.find('.optionValue').tagsinput({
        });
    },
    removeOption: function (e) {
        $(this).closest('.optiondetail').remove();
    },
    validationForm: function (e) {
        var self = this;
        var value = $("#rtxDescription").val();
        if (value === "") {
            alert("Description field is mandatory.");
            e.preventDefault();
        }
        $('.options').find('.optiondetail').each(function (index) {
            $(this).find('.optionName').attr('name', 'Options[' + index + '].Name');
            $(this).find('.optionValue').attr('name','Options[' + index + '].Values');
        });
        $('.variantId').each(function (index) {
            $(this).attr('name', 'Variants[' + index + '].Id');
        });
        $('.variantName').each(function (index) {
            $(this).attr('name', 'Variants[' + index + '].Name');
        });
        $('.variantUrl').each(function (index) {
            $(this).attr('name', 'Variants[' + index + '].BlockUrl');
        });
    },
    initItems: function (type) {
        var self = this,
        items = type == 1 ? self.photos : self.partnerPhotos,
        $preview = type == 1 ? $("#divPreviewPhotos") : $("#divPreviewPartnerPhotos"),
        $hdImg = type == 1 ? $('#hdImg') : $('#hdPartnerImg');
        $hdImg.find("input[name$='BlobUrl']").each(function (index) {
            var value = $(this).val();
            items.push(value);
        });
        $preview.find('.glyphicon-remove').each(function (index) {
            var src = $(this).closest('div').find('img').attr('src');
            $(this).click(self.removeItem(type, src));
        });
    },
    refreshItems: function (type) {
        var self = this,
            items = type == 1 ? self.photos : self.partnerPhotos,
            $preview = type == 1 ? $("#divPreviewPhotos") : $("#divPreviewPartnerPhotos"),
            nameAttr = type == 1 ? 'Photos' : 'PartnerPhotos',
            $hdImg = type == 1 ? $('#hdImg') : $('#hdPartnerImg');
        if (type == 1) {
            $(".src-images").find('option').remove();
        }
        $preview.find("div").remove();
        $hdImg.html('');
        for (var i = 0; i < items.length; i++) {
            var src = items[i];
            var img = $(`<div><i title="remove" class="glyphicon glyphicon-remove"></i><img class="img-thumbnail" src='${src}' style='height:100px' /></div>`);
            var inp = `<input name='${nameAttr}[${i}].BlobUrl' type='hidden' value='${src}' />`;
            $preview.prepend(img);
            img.find('.glyphicon-remove').click(self.removeItem(type, src));
            $hdImg.append(inp);
            if (type == 1) {
                var imgOption = $(`<option data-img-src='${src}' value='${src}'>${src}</option>`);
                $(".src-images").append(imgOption);
            }
           
        }
    },
    removeItem: function (type, url) {
        return function () {
            var self = CreateProduct,
                items = type == 1 ? self.photos : self.partnerPhotos;
            var index = $.inArray(url, items);
            items.splice(index, 1);

            self.refreshItems(type);
        }
    },
    previewFiles: function (t, type) {
        var self = CreateProduct,
            files = t.files,
            data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/Product/UploadFiles", data: data,
            contentType: false, processData: false,
            success: function (data) {
                if (type == 1) // photos
                {
                    self.photos = self.photos.concat(data.items);
                }
                else // partner photos
                {
                    self.partnerPhotos = self.partnerPhotos.concat(data.items);
                }

                self.refreshItems(type);
                $(t).val('');
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });
    }
}

function previewFiles(t, type) {
    CreateProduct.previewFiles(t, type);
}