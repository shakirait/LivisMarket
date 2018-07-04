window.onload = function (e) {
    ListOrganisation.init();
}
ListOrganisation = {
    organisations: [],
    organisationsTable: null,
    init: function () {
        var self = this;
        var data = $("#data").val();
        if (data != "") {
            var jsonObject = JSON.parse(data);
            self.organisations = [];
            for (var i = 0; i < jsonObject.length; i++) {
                var tempData = [];
                tempData.push(jsonObject[i].ShopName);
                tempData.push(jsonObject[i].WebsiteUrl);
                tempData.push(jsonObject[i].OpenTime);
                tempData.push(jsonObject[i].PhoneNumber);
                tempData.push(jsonObject[i].Email);
                tempData.push(jsonObject[i].FirstName);
                tempData.push(jsonObject[i].LastName);
                tempData.push(jsonObject[i].FaxNumber);
                tempData.push(jsonObject[i].RegistrationStatus);
                tempData.push(jsonObject[i].ViewUrl);
                tempData.push(jsonObject[i].EditUrl);
                self.organisations.push(tempData);
            }
            if (self.organisationsTable == null || typeof self.organisationsTable == 'undefined') {
                self.organisationsTable = $("#organisations").DataTable({
                    data: self.organisations,
                    "pageLength": 30,
                    "info": false,
                    "bLengthChange": false,
                    columns: [
                        {
                            title: "Organisation Name",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                return "<a href='" + JsonResultRow[9] + "' target='_blank'>" + JsonResultRow[0] + "</a>";
                            }
                        },
                        {
                            title: "Website Url"
                        },
                        {
                            title: "Open Time"
                        },
                        {
                            title: "Phone Number"
                        },
                        {
                            title: "Email"
                        },
                        {
                            title: "First Name"
                        },
                        {
                            title: "Last Name"
                        },
                        {
                            title: "Fax Number"
                        },
                        {
                            title: "Registration Status"
                        },
                        {
                            title: "",
                            "render": function (data,
                                type,
                                JsonResultRow,
                                meta) {
                                var alias = "<i class='glyphicon glyphicon-pencil' aria-hidden='true'></i>";
                                return "<a href='" + JsonResultRow[10] + "' target='_blank' class=\"btn btn-primary\" role=\"button\">" +
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
                self.organisationsTable.clear();
                self.organisationsTable.rows.add(self.organisations);
                self.organisationsTable.draw();
            }
            $('#organisations tbody').on('click', 'tr', function () {
                self.organisations.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            });
        }
    }
}

