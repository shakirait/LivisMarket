var Product = {
    init: function () {
        $(document)
            .on('click', '.jsLoadItem', Product.loadItem)
            .on('click', '.jsLoadRecommendDetailItem', Product.loadRecommendDetailItem)
            .on('change', '.jsSelectQty', Product.changeQty)
            .on('click', '.jsChangeToCatAllItem', Product.changeToCatAllItem)
            .on('click', '.jsChangeToDogAllItem', Product.changeToDogAllItem)
            .on('click', '.jsNextPage', Product.NextPage)
            .on('click', '.jsRecommendItemCarousel', Product.changeCarousel)
    },     
    loadItem: function (e) {        
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var skuCode = $("#code", form).val();
        $.ajax({
            type: 'get',
            dataType: 'json',
            cache: false,
              url: form[0].action,
              data: { code: skuCode},
              success: function (result) {
                  $("#itemDefaultPrice").html(result.data.DefaultPrice);
                  $("#itemSubscriberPrice").html(result.data.SubscriberPrice);
                  $("#itemDealPrice").html(result.data.DealPrice);
                  $("#code").html(result.data.Code);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    loadRecommendDetailItem: function (e) {        
        e.preventDefault();
        
        var formContainer = $("#" + $(this).attr("data_container"));
        var skuCode = $(this).attr("code");
        $.ajax({
            type: "get",
            url: "/PetProductPage/LoadRecommmendDetailItem",
            data: { code: skuCode },
            success: function (result) {
                formContainer.html($(result));
                formContainer.change();
            },
            error: function (xhr, status, error) {
            }
        });
    },    
    changeCarousel: function (e) {
        e.preventDefault();
        var carouselId = $(this).attr("href");
        var query = ".jsLoadRecommendDetailItem .active[data-target='" + carouselId + "'";
        var selectedItem = $(query).parent();
        var formContainer = $("#" + $(selectedItem).attr("data_container"));
        var skuCode = $(selectedItem).attr("code");        
        $.ajax({
            type: "get",
            url: "/PetProductPage/LoadRecommmendDetailItem",
            data: { code: skuCode },
            success: function (result) {
                formContainer.html($(result));
                formContainer.change();
            },
            error: function (xhr, status, error) {
            }
        });
    },
    changeQty: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var skuCode = $("#code", form).val();
        $.ajax({
            type: "POST",
            url: form[0].action,
            data: { code: skuCode },
            success: function (result) {
                alert('Remove ' + skuCode + ' to cart successful!')
                formContainer.html($(result));
                formContainer.change();

                Cart.updateMiniCart(e);
            },
            error: function (xhr, status, error) {
            }
        });
    },
    changeToCatAllItem: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var dogCode = $("#dogCode", form).val();
        var catCode = $("#catCode", form).val();
        var animalType = "cat";
        Product.ChangeView(e, form, formContainer, dogCode, catCode, animalType);
    },    
    changeToDogAllItem: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        var formContainer = $("#" + form.data("container"));
        var dogCode = $("#dogCode", form).val();
        var catCode = $("#catCode", form).val();
        var animalType = "dog";
        Product.ChangeView(e, form, formContainer, dogCode, catCode, animalType);
    },    
    ChangeView: function (e, form, formContainer, dogCode, catCode, animalType) {
        $.ajax({
            type: "get",
            url: form[0].action,
            data: { dogCode: dogCode, catCode: catCode, animalType: animalType  },
            success: function (result) {
                formContainer.html($(result));
                formContainer.change();
            },
            error: function (xhr, status, error) {
            }
        });
    },    
    NextPage: function (e) {
        e.preventDefault();
        var form = $(this).closest("form");
        $.ajax({
            type: "get",
            url: form[0].action,
            data: {  },
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