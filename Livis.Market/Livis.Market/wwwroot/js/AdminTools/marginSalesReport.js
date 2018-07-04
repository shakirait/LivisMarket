$(document).ready(function () {
    var year = $("#reportYear")[0];
    var month = $('#reportMonth')[0];
    var runButton = $('#btnRunJob');
    var chkRunUptoDate = $('#chkRunUptoDate')[0];
    var message = $('#message');
    message.text($("#jobSelectStartTimeTextHidden").text());
    runButton.on('click', function (e) {
        e.preventDefault();
        if (year.selectedIndex == 0 || month.selectedIndex == 0) {
            return;
        }
        runButton.hide();
        message.text($("#runningTextHidden").text());
        $.ajax({
            type: 'POST',
            url: '/custom-plugins/margin-sales-report/execute',
            data: {
                Year: year.value,
                Month: month.value,
                ShouldRunTillNow: chkRunUptoDate.checked
            },
            success: function (data) {
                runButton.show();
                if (data.result === true) {
                    message.text($("#jobCompleteTextHidden").text());
                }
                else {
                    message.text($("#jobFailTextHidden").text() + ' ' + data.message);
                }
            },
            error: function (xhr, status, error) {
            }
        });
    });
});