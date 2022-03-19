/*
Author       : Dreamguys
Template Name: Doccure - Bootstrap Template
Version      : 1.0
*/

$(document).ready(function () {

	var optionsResume = {};
	var resumeUploader = $('.js-uploader__box_Resume').uploader(optionsResume);

	var optionsOther = {};
	var otherUploader = $('.js-uploader__box_other').uploader(optionsOther);

	var optionsDegree = {};
	var degreeUploader = $('.js-uploader__box_Degree').uploader(optionsDegree);

	$("#ExpertPanelMenu .active").removeClass("active");
	$("#ExpertPanelMenu #Profile").addClass("active");

});

(function ($) {
    "use strict";
	
	// Pricing Options Show
	
	$('#pricing_select_phoneCallPrice input[name="rating_option_phoneCallPrice"]').on('click', function() {
		if ($(this).val() == 'price_free_phoneCallPrice') {
			$('#custom_price_cont_phoneCallPrice').show();
		}
		if ($(this).val() == 'custom_price_phoneCallPrice') {
			$('#custom_price_cont_phoneCallPrice').hide();
		}
		else {
		}
	});

	$('#pricing_select_voiceCallPrice input[name="rating_option_voiceCallPrice"]').on('click', function () {
		if ($(this).val() == 'price_free_voiceCallPrice') {
			$('#voiceCallPrice_Custom').show();
		}
		if ($(this).val() == 'custom_price_voiceCallPrice') {
			$('#voiceCallPrice_Custom').hide();
		}
		else {
		}
	});

	$('#pricing_select_textCallPrice input[name="rating_option_textCallPrice"]').on('click', function () {
		if ($(this).val() == 'price_free_textCallPrice') {
			$('#textCallPrice_Custom').show();
		}
		if ($(this).val() == 'custom_price_textCallPrice') {
			$('#textCallPrice_Custom').hide();
		}
		else {
		}
	});
	
	// Education Add More
	
    $(".education-info").on('click','.trash', function () {
		$(this).closest('.education-cont').remove();
		return false;
    });

    $(".add-education").on('click', function () {
		
		var educationcontent = '<div class="row form-row education-cont">' +
			'<div class="col-12 col-md-10 col-lg-11">' +
				'<div class="row form-row">' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>مدرک</label>' +
							'<input type="text" class="form-control" id="degreeOfEducation" name="degreeOfEducation">' +
						'</div>' +
					'</div>' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>دانشگاه/موسسه</label>' +
							'<input type="text" class="form-control" id="university" name="university">' +
						'</div>' +
					'</div>' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>سال اتمام</label>' +
							'<input type="number" class="form-control datetimepicker" id="endDate" name="endDate">' +
						'</div>' +
					'</div>' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2 col-lg-1"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>' +
		'</div>';
		
        $(".education-info").append(educationcontent);
        return false;
    });
	
	// Experience Add More
	
    $(".experience-info").on('click','.trash', function () {
		$(this).closest('.experience-cont').remove();
		return false;
    });

    $(".add-experience").on('click', function () {
		
		var experiencecontent = '<div class="row form-row experience-cont">' +
			'<div class="col-12 col-md-10 col-lg-11">' +
				'<div class="row form-row">' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>نام کلینیک</label>' +
							'<input type="text" class="form-control" id="pastClinicName" name="pastClinicName">' +
						'</div>' +
					'</div>' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>از سال</label>' +
							'<input type="text" class="form-control" id="startYear" name="startYear">' +
						'</div>' +
					'</div>' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label>تا سال</label>' +
							'<input type="text" class="form-control" id="finishYear" name="finishYear">' +
						'</div>' +
					'</div>' +
					'<div class="col-12 col-md-6 col-lg-4">' +
						'<div class="form-group">' +
							'<label> نقش</label>' +
							'<input type="text" class="form-control" id="role" name="role">' +
						'</div>' +
					'</div>' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2 col-lg-1"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>' +
		'</div>';
		
        $(".experience-info").append(experiencecontent);
        return false;
    });
	
	// Awards Add More
	
    $(".awards-info").on('click','.trash', function () {
		$(this).closest('.awards-cont').remove();
		return false;
    });

    $(".add-award").on('click', function () {

        var regcontent = '<div class="row form-row awards-cont">' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label>جایزه ها</label>' +
					'<input type="text" class="form-control" id="prizeName" name="prizeName">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label>سال</label>' +
					'<input type="text" class="form-control" id="year" name="year">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2">' +
				'<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
				'<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
			'</div>' +
		'</div>';
		
        $(".awards-info").append(regcontent);
        return false;
    });
	
	// Membership Add More
	
    $(".membership-info").on('click','.trash', function () {
		$(this).closest('.membership-cont').remove();
		return false;
    });

    $(".add-membership").on('click', function () {

        var membershipcontent = '<div class="row form-row membership-cont">' +
			'<div class="col-12 col-md-10 col-lg-5">' +
				'<div class="form-group">' +
					'<label>عضویت ها</label>' +
					'<input type="text" class="form-control" id="name" name="membershipName">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2 col-lg-2">' +
				'<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
				'<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
			'</div>' +
		'</div>';
		
        $(".membership-info").append(membershipcontent);
        return false;
    });
	
	// Registration Add More
	
    $(".registrations-info").on('click','.trash', function () {
		$(this).closest('.reg-cont').remove();
		return false;
    });

    $(".add-reg").on('click', function () {

        var regcontent = '<div class="row form-row reg-cont">' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label> ثبت نام ها </label>' +
					'<input type="text" class="form-control" id="subscriptionName" name="subscriptionName">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-5">' +
				'<div class="form-group">' +
					'<label>سال</label>' +
					'<input type="text" class="form-control" id="subscriptionYear" name="subscriptionYear">' +
				'</div>' +
			'</div>' +
			'<div class="col-12 col-md-2">' +
				'<label class="d-md-block d-sm-none d-none">&nbsp;</label>' +
				'<a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a>' +
			'</div>' +
		'</div>';
		
        $(".registrations-info").append(regcontent);
        return false;
	});
	
})(jQuery);

var otherImageFileList = [];

function addOtherFileToList(file) {
	otherImageFileList.push(file);
}

function removeOtherFileFromList(id) {
	for (var i = 0; i < otherImageFileList.length; i++) {
		if (i === parseInt(id)) {
			otherImageFileList.splice(i, 1);
			break;
		}
	}
}

var resumeImageFileList = [];

function addResumeFileToList(file) {
	resumeImageFileList.push(file);
}

function removeResumeFileFromList(id) {
	for (var i = 0; i < resumeImageFileList.length; i++) {
		if (i === parseInt(id)) {
			resumeImageFileList.splice(i, 1);
			break;
		}
	}
}

var degreeImageFileList = [];

function addDegreeFileToList(file) {
	degreeImageFileList.push(file);
}

function removeDegreeFileFromList(id) {
	for (var i = 0; i < degreeImageFileList.length; i++) {
		if (i === parseInt(id)) {
			degreeImageFileList.splice(i, 1);
			break;
		}
	}
}