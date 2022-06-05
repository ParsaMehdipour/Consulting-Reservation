

$.fn.select2.defaults.set('language', {
    errorLoading: function () {
        return "خطا در نمایش اطلاعات";
    },
    inputTooLong: function (args) {
        return "طول رشته از حد مجاز بیشتر است";
    },
    inputTooShort: function (args) {
        return "طول رشته از حد مجاز کمتر است";
    },
    loadingMore: function () {
        return "در حال بارگذاری";
    },
    maximumSelected: function (args) {
        return "انتخاب همه";
    },
    noResults: function () {
        return "موردی یافت نشد";
    },
    searching: function () {
        return "در حال جست جو ...";
    }
});



function getSelect2Body(model, params) {
    params.page = params.page || 1;

    var nullModel = {
        "id": "",
        "text": "لطفا یک گزینه را انتخاب نمایید",
        "disabled": false,
        "selected": true
    };

    var list = [];

    if (params.page == 1) {
        list.push(nullModel);
        for (var i = 0; i < model.items.length; i++) {
            list[i + 1] = model.items[i];
        }

    } else {
        for (var i = 0; i < model.items.length; i++) {
            list[i] = model.items[i];
        }
    }




    return {
        results: list,
        pagination: {
            more: (params.page * 10) < model.countFiltered
        }
    };
}


