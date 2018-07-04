var Language = {
    init: function () {
        Language.AddToCart = "{product name} はカートに追加されました。カートに進みますか？" + "\n" + "カートに進む»「Yes」をクリック" + "\n" + "買い物を続ける»「No」をクリック";
    }, 
    getLanguageText: function (key) {        
    },
    preventSubmit: function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }
    
};