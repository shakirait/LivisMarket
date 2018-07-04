$(document).ready(function () {
    $("#submit-contact").on('click',
        function() {
            $("#contact-form").submit();
        });
    $("#search-fqa").on('click', function () {
        var textSearch = $("#text-fqa-search").val();
        if (textSearch === "" || textSearch === null || textSearch === undefined) {
            ResetFilter();
            return;
        } else {
            var arrayTextSearch = textSearch.split('');
            var arrayTextSearch1 = textSearch.split(' ');
            $('.question-card').removeClass('invisible');
            $('[id^="Tab"].tab-pane.active').find('.question-card').each(function (e) {
                var textQuestion = $(this).find(".question-card-title").text();
                var textAnswer = $(this).find(".question-card-desctiption").text();
                var isContain = false;
                if (isContainWord(arrayTextSearch, textQuestion)) {
                    isContain = true;
                } else if (isContainWord(arrayTextSearch, textAnswer)) {
                    isContain = true;
                } else if (isContainWord(arrayTextSearch1, textQuestion)) {
                    isContain = true;
                } else if (isContainWord(arrayTextSearch1, textAnswer)) {
                    isContain = true;
                }

                if (isContain == false) {
                    $(this).addClass('invisible');
                } else {
                    $(this).removeClass('invisible');
                }
            });
        }
    });
    if ($('#Message').length > 0) {
        var announcementMessage = $('#Message').val();
        if (announcementMessage !== "")
        {
            alert(announcementMessage);
            $('#Message').val("");
        }
    }
    

});

function ResetFilter() {
    $('.question-card').removeClass('invisible');
}

function isContainWord(arrayTextSearch, text) {
    for (var i = 0; i < arrayTextSearch.length; i++) {
        var word = arrayTextSearch[i];
        if (text.includes(word)) {
            return true;
        }
    }
    return false;
}
function SelectFAqTab(tabName) {
    $('ul[role="tablist"]').find('a').each(function (e) {
        if (tabName === $(this).attr('href'))
            $(this).click();
    });
    ResetFilter();
}