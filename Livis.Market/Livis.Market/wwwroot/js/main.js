$(document).ready(function() {

    $('.btn-collapse').on('click', function(e) {
        e.preventDefault();
        $(this).parents('.media-info').find('.media-body').toggleClass('media-body-collapse');
        $(this).parents('.question-card').find('.question-card-desctiption').toggleClass('question-card-collapse');
    });

    $(window).on('load', function() {
        replaceImg('.img-replace');
    });

    $(window).resize(function() {
        replaceImg('.img-replace');
    });

    replaceImg = function(container) {
        $(container).each(function() {
            var scrWidth = $(window).width();
            if (scrWidth < 768) {
                $('.img-replace').attr('src', $('.img-replace').data('mobile'));
            }
            if (scrWidth > 768) {
                $('.img-replace').attr('src', $('.img-replace').data('desktop'));
            }
        });
    }
    $(".btn-dog, .btn-cat, .btn-scroll").on('shown.bs.tab', function (e) {
        $(this).removeClass('active');
        $('html, body').animate({
            scrollTop: $($($(this).attr('href'))).offset().top
        }, 1000);
    });
    $('.btn-scroll').on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({
            scrollTop: $($($(this).attr('href'))).offset().top
        }, 1000);
    });

    var url = document.location.toString();
    if (url.match('#')) {
        $('.nav-tabs a[href="#' + url.split('#')[1] + '"]').tab('show');
    }

    // Change hash for page-reload
    $('.nav-tabs a').on('shown.bs.tab', function(e) {
        window.location.hash = e.target.hash;
        console.log('ds');
    })

    $('.step-cart a').on('shown.bs.tab', function(e) {
        $('.step-cart a').removeClass('active');
        $('.step-cart [href="' + $(e.target).attr('href') + '"]').addClass('active');
        $('html, body').animate({
            scrollTop: $('#cart').offset().top
        }, 1000);
    });

    $('.tab-next, .tab-prev').on('shown.bs.tab', function(e) {
        $('.step-cart a').removeClass('active');
        $('.step-cart [href="' + $(e.target).attr('href') + '"]').addClass('active');
        $(this).removeClass('active');
        $('html, body').animate({
            scrollTop: $('#cart').offset().top
        }, 1000);
    });

    $('#cart-3 #paymentMethod').on('change', function(e) {
        console.log($(this).val());
        // if ($(body).hasClass('inner')) {
        if ($("body").hasClass("inner")) {
            $('#selectedNext').attr('href', $(this).val() + ".html")
            $('.step-cart .step-4 a').attr('href', $(this).val() + ".html")
        } else {
            $('#selectedNext').attr('href', "#" + $(this).val())
            $('.step-cart .step-4 a').attr('href', "#" + $(this).val())
        }

    });
    $(window).scroll(function() {
        if ($(this).scrollTop() >= 500) {
            $('.to-top').fadeIn(200);
        } else {
            $('.to-top').fadeOut(200);
        }
    });
    $('.to-top').on('click', function(e) {
        e.preventDefault();
        $('body,html').animate({
            scrollTop: 0
        }, 1500);
    });

    $('#navbarSupportedContent').on('hide.bs.collapse', function() {
        $('body').removeClass('nav-open');
        $(this).removeClass('show');

    });

    $('#navbarSupportedContent').on('show.bs.collapse', function() {
        $(this).addClass('show');
        $('#navbarUserMenu').collapse('hide');
        $('body').addClass('nav-open');
    });

    $('#navbarUserMenu').on('hide.bs.collapse', function() {
        $('body').removeClass('nav-open');
        $(this).removeClass('show');
    });

    $('#navbarUserMenu').on('show.bs.collapse', function() {
        $(this).addClass('show');
        $('#navbarSupportedContent').collapse('hide');
        $('body').addClass('nav-open');
    });
    $('.button-checkbox').on('click', function() {
        $('.button-checkbox').removeClass('active');
        $(this).addClass('active');
    });


    $('#toPurchase').on('click', function(e) {
        e.preventDefault();
        var hash = this.hash;
        $('html, body').animate({
            scrollTop: $(hash).offset().top
        }, 1000);
    });
    $('.carousel-product').on('slide.bs.carousel', function(e) {
        var slideTo = $(e.relatedTarget).index();
        $('[data-target="#' + $(this).attr('id') + '"]').each(function(index) {
            $('.button-checkbox').removeClass('active');
            $('[data-slide-to="' + slideTo + '"]').addClass('active');
        });

    });
    $('#carouselProductDog').on('slide.bs.carousel', function(e) {
        $('#dogBestPrice').text($(e.relatedTarget).data('price'));
        $('#dogPerMonth').text($(e.relatedTarget).data('permonth'));
        $('#dogPerGrain').text($(e.relatedTarget).data('pergrain'));
    });
    $('#carouselProductCat').on('slide.bs.carousel', function(e) {
        $('#catBestPrice').text($(e.relatedTarget).data('price'));
        $('#catPerMonth').text($(e.relatedTarget).data('permonth'));
        $('#catPerGrain').text($(e.relatedTarget).data('pergrain'));
    });


    $('.select [type="radio"]').on('change', function() {
        $('.select').removeClass('active');
        $(this).parents('.select').addClass('active');
    });

    $('select').each(function() {
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

        $styledSelect.click(function(e) {
            e.stopPropagation();
            $('div.form-control.active').not(this).each(function() {
                $(this).removeClass('active').next('ul.select-options').hide();
            });
            $(this).toggleClass('active').next('ul.select-options').toggle();
        });

        $listItems.click(function(e) {
            e.stopPropagation();
            $styledSelect.text($(this).text()).removeClass('active');
            $this.val($(this).attr('rel'));
            $list.hide();
            //console.log($this.val());
        });

        $(document).click(function() {
            $styledSelect.removeClass('active');
            $list.hide();
        });

    });

    $('.custom-select').each(function() {
        $(this).next('.form-control').addClass('select-default')
    });

    $('.custom-select-sm').each(function() {
        $(this).next('.form-control').addClass('select-sm').parents('.select-input').addClass('select-input-sm')
    });

    $('.select-styled').each(function() {
        $(this).parents('.select-input').addClass('select-styled')
    });
    $('.next-step, .prev-step').on('click', function(e) {
        var $activeTab = $('.tab-pane.active');
        if ($(e.target).hasClass('next-step')) {
            var nextTab = $activeTab.next('.tab-pane').attr('id');
            console.log(nextTab);
            $('[href="#' + nextTab + '"]').tab('show');
        } else {
            var prevTab = $activeTab.prev('.tab-pane').attr('id');
            $('[href="#' + prevTab + '"]').tab('show');
        }
    });

    $('[href="#catBestBuy"]').on('click', function(e) {
        e.preventDefault()
        // $(this).siblings().tab('hide');
        // $(this).tab('show');
    });
    $('[href="#dogBestBuy"]').on('click', function(e) {
        e.preventDefault()
        // $(this).siblings().tab('hide');
        // $(this).tab('show');

    })

    $('#details').on('shown.bs.collapse', function() {
        $('html, body').animate({
            scrollTop: $("#details").offset().top
        }, 1000);
    });
    $('#details').on('hidden.bs.collapse', function() {
        $('html, body').animate({
            scrollTop: $("#map").offset().top
        }, 1000);
    });

    String.prototype.toCardFormat = function() {
        return this.replace(/[^0-9]/g, "").substr(0, 16).split("").reduce(cardFormat, "");

        function cardFormat(str, l, i) {
            return str + ((!i || (i % 4)) ? "" : "-") + l;
        }
    };

    $(document).ready(function() {
        $(".credit-card").keyup(function() {
            $(this).val($(this).val().toCardFormat());
        });
        $('#falseinput').click(function() {
            $("#fileinput").click();
        });
    });
    $('#fileinput').change(function() {
        $('#selected_filename').text($('#fileinput]')[0].files[0].name);

    });
    if ($('*').is('#datetimepicker1')) {
        $('#datetimepicker1').datetimepicker({
            viewMode: 'days',
            format: 'MM/dd/YYYY'
        });
    }




    $('.table-select-mobile .custom-control-input').on('change', function() {

        $('.table-select-mobile .custom-control-input').each(function() {
            var $this = $(this),
                url = $this.attr('datasrc');
            if ($(this).prop('checked')) {
                $(this).parents('tr').addClass('checked').find('.btn').addClass('btn-primary').removeClass('btn-secondary');
            } else {
                $(this).parents('tr').removeClass('checked').find('.btn').removeClass('btn-primary').addClass('btn-secondary');
            }
        });

    });

    $('.radio-btn').on('click', function(e) {
        e.preventDefault();
        $('.table-select-mobile .custom-control-input, .table-select .custom-control-input').attr("checked", false).prop("checked", false).change();
        $(this).parents('tr').find('.custom-control-input').attr("checked", true).prop("checked", true).change();

    });



    $('[href="#clinicList"]').on('click', function() {
        $("#clinicList").collapse('show')
    });
});