$(document).ready(function () {
    services.init();
});

var services = {
    init: function () {
        $('#reportYear .select-options li').click(services.loadDetails);
        $('#reportMonth .select-options li').click(services.loadDetails);
    },
    loadDetails: function (e) {
        e.preventDefault();
        var year = $('#reportYear').find(':selected');
        var month = $('#reportMonth').find(':selected');
        if (year.index() == 0 || month.index() == 0) {
            return;
        }
        $.ajax({
            type: 'get',
            url: '/MarginSalesReportPage/LoadDetails',
            data: {
                year: year[0].value,
                month: month[0].value
            },
            dataType: "html",
            success: function (result) {
                $('#marginSalesReportDetailView').empty();
                $('#marginSalesReportDetailView').html(result);
                $('#btnExportToCsv').unbind('click').on('click', services.exportHistoryTransactionsToCsv);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    exportHistoryTransactionsToCsv: function (e) {
        e.preventDefault();
        $('.table-history').table2csv();
    }
};