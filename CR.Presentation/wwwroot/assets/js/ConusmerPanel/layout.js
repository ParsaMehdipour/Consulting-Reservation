$(document).ready(function () {

    updateSideBar();

});

function updateSideBar() {

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/api/ConsumerDetails/GetDetails',
        cache: false,
        contentType: "application/json; charset=utf-8",
        enctype: 'application/json',
        success: function (response) {

            var model = response.data;

            $('.profile-sidebar .widget-profile .profile-info-widget .booking-doc-img img').attr("src", "/" + model.iconSrc);

            $('.profile-sidebar .widget-profile .profile-info-widget .profile-det-info h3').html(model.fullName);

            $('.profile-sidebar .widget-profile .profile-info-widget .profile-det-info .patient-details h5').html(model.birthDate + " , " + model.age + " سال");

            $('.profile-sidebar .widget-profile .profile-info-widget .profile-det-info .patient-details .cityAndProvince h5').html(model.province + " , " + model.city);

        }
    });

}