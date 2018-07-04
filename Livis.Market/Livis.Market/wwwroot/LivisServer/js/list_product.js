window.onload = function (e) {
    ListProduct.init();
    // Delete confirm bootstrap
    $('#confirm-delete').on('show.bs.modal', function (e) {
        $(this).find('.btn-ok').attr('data-id', $(e.relatedTarget).data('id'));
    });

    $('#confirm-delete').on('click', '.btn-ok', function (e) {
        var id = $(".btn-ok").attr("data-id");
        $.ajax({
            type: "POST",
            url: "/Product/Delete",
            data: { sku: id },
        })
            .done(function (items) {
                if (items.success) {
                    ListProduct.productsTable.row('.selected').remove().draw(false);
                    $('.btn-cancel').click();
                } else {
                    alert("Unable delete this product.");
                    console.log(items.message);
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            });
    });
}
ListProduct = {
    products: [],
    productsTable: null,
    init: function () {
        var self = this;
        var data = $("#data").val();
        if (data != "") {
            var jsonObject = JSON.parse(data);
            self.products = [];
            for (var i = 0; i < jsonObject.length; i++) {
                var tempData = [];
                tempData.push(jsonObject[i].ImageUrl);
                tempData.push(jsonObject[i].Name);
                tempData.push(jsonObject[i].Length);
                tempData.push(jsonObject[i].Width);
                tempData.push(jsonObject[i].Height);
                tempData.push(jsonObject[i].Weight);
                tempData.push(jsonObject[i].Cost);
                tempData.push(jsonObject[i].SuggestedPrice);
                tempData.push(jsonObject[i].Sku);
                tempData.push(jsonObject[i].ViewUrl);
                tempData.push(jsonObject[i].EditUrl);
                tempData.push(jsonObject[i].DeleteUrl);
                self.products.push(tempData);
            }
            if (self.productsTable == null || typeof self.productsTable == 'undefined') {
                self.productsTable = $("#products").DataTable({
                    data: self.products,
                    "pageLength": 30,
                    "info": false,
                    "bLengthChange": false,
                    columns: [
                        {
                            title: "Image",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                return "<a href='" + JsonResultRow[9] + "' target='_blank'><img src='" + JsonResultRow[0] + "' style='height:50px; cursor: pointer'></a>";
                            },
                            "orderable": false,
                            "searchable": false
                        },
                        {
                            title: "Name",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                return "<a href='" + JsonResultRow[9] + "'>" + JsonResultRow[1] + "</a>";
                            }
                        },
                        {
                            title: "Length"
                        },
                        {
                            title: "Width"
                        },
                        {
                            title: "Height"
                        },
                        {
                            title: "Weight"
                        },
                        {
                            title: "Cost"
                        },
                        {
                            title: "Suggested Price"
                        },
                        {
                            title: "Sku"
                        },
                        {
                            title: "",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var alias = "<i class='glyphicon glyphicon-pencil' aria-hidden='true'></i>";
                                return "<a href='" + JsonResultRow[10] + "' class=\"btn btn-primary\" role=\"button\">" +
                                    alias +
                                    "</a>";
                            },
                            "orderable": false,
                            "searchable": false
                        },
                        {
                            title: "",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var alias =
                                    "<i class='glyphicon glyphicon-trash' aria-hidden='true'></i>";
                                var sku = JsonResultRow[8];
                                return "<a href=\"#\" class=\"btn btn-danger delProduct\" data-id='" + sku +
                                    "' data-toggle=\"modal\" data-target=\"#confirm-delete\" role=\"button\">" +
                                    alias +
                                    "</a>";
                            },
                            "orderable": false,
                            "searchable": false
                        }
                    ],
                    initComplete: function () {
                    }
                });
            } else {
                self.productsTable.clear();
                self.productsTable.rows.add(self.products);
                self.productsTable.draw();
            }
            $('#products tbody').on('click', 'tr', function () {
                self.productsTable.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            });
        }
    }
}

