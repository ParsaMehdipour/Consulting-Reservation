/*
Author       : Dreamguys
Template Name: Doccure - Bootstrap Template
Version      : 1.3
*/

(function ($) {
	"use strict";

	// Stick Sidebar

	if ($(window).width() > 767) {
		if ($('.theiaStickySidebar').length > 0) {
			$('.theiaStickySidebar').theiaStickySidebar({
				// Settings
				additionalMarginTop: 30
			});
		}
	}

	// Sidebar

	if ($(window).width() <= 991) {
		var Sidemenu = function () {
			this.$menuItem = $('.main-nav a');
		};

		function init() {
			var $this = Sidemenu;
			$('.main-nav a').on('click', function (e) {
				if ($(this).parent().hasClass('has-submenu')) {
					// e.preventDefault();
				}
				if (!$(this).hasClass('submenu')) {
					$('ul', $(this).parents('ul:first')).slideUp(350);
					$('a', $(this).parents('ul:first')).removeClass('submenu');
					$(this).next('ul').slideDown(350);
					$(this).addClass('submenu');
				} else if ($(this).hasClass('submenu')) {
					$(this).removeClass('submenu');
					$(this).next('ul').slideUp(350);
				}
			});
		}

		// Sidebar Initiate
		init();
	}

	// Textarea Text Count

	var maxLength = 100;
	$('#review_desc').on('keyup change', function () {
		var length = $(this).val().length;
		length = maxLength - length;
		$('#chars').text(length);
	});

	// Select 2

	if ($('.select').length > 0) {
		$('.select').select2({
			minimumResultsForSearch: -1,
			width: '100%'
		});
	}

	// Date Time Picker

	if ($('.datetimepicker').length > 0) {
		$('.datetimepicker').datetimepicker({
			format: 'DD/MM/YYYY',
			icons: {
				up: "fas fa-chevron-up",
				down: "fas fa-chevron-down",
				next: 'fas fa-chevron-right',
				previous: 'fas fa-chevron-left'
			}
		});
	}

	// Floating Label

	if ($('.floating').length > 0) {
		$('.floating').on('focus blur', function (e) {
			$(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
		}).trigger('blur');
	}

	// Mobile menu sidebar overlay

	$('body').append('<div class="sidebar-overlay"></div>');
	$(document).on('click', '#mobile_btn', function () {
		$('main-wrapper').toggleClass('slide-nav');
		$('.sidebar-overlay').toggleClass('opened');
		$('html').addClass('menu-opened');
		return false;
	});

	$(document).on('click', '.sidebar-overlay', function () {
		$('html').removeClass('menu-opened');
		$(this).removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});

	$(document).on('click', '#menu_close', function () {
		$('html').removeClass('menu-opened');
		$('.sidebar-overlay').removeClass('opened');
		$('main-wrapper').removeClass('slide-nav');
	});

	// Tooltip

	if ($('[data-toggle="tooltip"]').length > 0) {
		$('[data-toggle="tooltip"]').tooltip();
	}

	// Content div min height set

	function resizeInnerDiv() {
		var height = $(window).height();
		var header_height = $(".header").height();
		var footer_height = $(".footer").height();
		var setheight = height - header_height;
		var trueheight = setheight - footer_height;
		$(".content").css("min-height", trueheight);
	}

	if ($('.content').length > 0) {
		resizeInnerDiv();
	}

	$(window).resize(function () {
		if ($('.content').length > 0) {
			resizeInnerDiv();
		}
	});

	// Slick Slider

	if ($('.specialities-slider').length > 0) {
		$('.specialities-slider').slick({
			dots: true,
			autoplay: false,
			infinite: true,
			variableWidth: true,
			rtl: true,
			centerMode: true,

			prevArrow: false,
			nextArrow: false
		});
	}

	if ($('.doctor-slider').length > 0) {
		$('.doctor-slider').slick({
			dots: false,
			autoplay: false,
			infinite: true,
			rtl: true,
			centerMode: true,
			variableWidth: true,
		});
	}
	if ($('.features-slider').length > 0) {
		$('.features-slider').slick({
			dots: true,
			infinite: true,
			centerMode: true,
			slidesToShow: 3,
			speed: 500,
			variableWidth: true,
			arrows: false,
			autoplay: false,
			rtl: true,
			responsive: [{
				breakpoint: 992,
				settings: {
					slidesToShow: 1
				}

			}]
		});
	}


	// Slick Slider
	if ($('.features-slider1').length == 1) {
		$('.features-slider1').slick({
			dots: false,
			infinite: true,
			centerMode: false,
			slidesToShow: 1,
			speed: 500,
			variableWidth: true,
			arrows: true,
			autoplay: false,
			rtl: true,
			responsive: [{
				breakpoint: 992,
				settings: {
					slidesToShow: 1
				}

			}]
		});
	}
	if ($('.slider-1').length > 0) {
		$('.slider-1').slick({
			rtl: true,
		});
	}


	// Date Range Picker
	if ($('.bookingrange').length > 0) {
		var start = moment().subtract(6, 'days');
		var end = moment();

		function booking_range(start, end) {
			var start = moment(start); // pass your date obj here.
            console.log(start.format('jYYYY/jM/jD'));

            var end = moment(end); // pass your date obj here.
						console.log(end.format('jYYYY/jM/jD'));
						
			$('.bookingrange span').html(start.format('jYYYY/jM/jD') + ' - ' + end.format('jYYYY/jM/jD'));
		}

		$('.bookingrange').daterangepicker({
			months: ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'],
			startDate: start,
			endDate: end,
			ranges: {
				'امروز': [moment(), moment()],
				'دیروز': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
				'هفته گذشته': [moment().subtract(6, 'days'), moment()],
				'ماه گذشته': [moment().subtract(29, 'days'), moment()],
				'این ماه': [moment().startOf('month'), moment().endOf('month')],
				'ماه بعد': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
			},
			"locale": {
				"format": "YYYY/M/D",
				"separator": " - ",
				"applyLabel": "اعمال",
				"cancelLabel": "انصراف",
				"fromLabel": "از",
				"toLabel": "تا",
				"customRangeLabel": "سفارشی",
				"weekLabel": "هف",
				"daysOfWeek": [
					"ی",
					"د",
					"س",
					"چ",
					"پ",
					"ج",
					"ش"
				],
				"monthNames": [
					"ژانویه",
					"فوریه",
					"مارس",
					"آوریل",
					"می",
					"ژوئن",
					"جولای",
					"آگوست",
					"سپتامبر",
					"اکتبر",
					"نوامبر",
					"دسامبر"
				],
				"firstDay": 6
			}
		}, booking_range);

		booking_range(start, end);
	}
	// Chat

	var chatAppTarget = $('.chat-window');
	(function () {
		if ($(window).width() > 991)
			chatAppTarget.removeClass('chat-slide');

		$(document).on("click", ".chat-window .chat-users-list a.media", function () {
			if ($(window).width() <= 991) {
				chatAppTarget.addClass('chat-slide');
			}
			return false;
		});
		$(document).on("click", "#back_user_list", function () {
			if ($(window).width() <= 991) {
				chatAppTarget.removeClass('chat-slide');
			}
			return false;
		});
	})();

	//Increment Decrement Numberes
	var quantitiy = 0;
	$('.quantity-right-plus').click(function (e) {
		e.preventDefault();
		var quantity = parseInt($('#quantity').val());
		$('#quantity').val(quantity + 1);
	});

	$('.quantity-left-minus').click(function (e) {
		e.preventDefault();
		var quantity = parseInt($('#quantity').val());
		if (quantity > 0) {
			$('#quantity').val(quantity - 1);
		}
	});

	//Cart Click
	$("#cart").on("click", function (o) {
		o.preventDefault();
		$(".shopping-cart").fadeToggle();
		$(".shopping-cart").toggleClass('show-cart');
	});

	// Circle Progress Bar

	function animateElements() {
		$('.circle-bar1').each(function () {
			var elementPos = $(this).offset().top;
			var topOfWindow = $(window).scrollTop();
			var percent = $(this).find('.circle-graph1').attr('data-percent');
			var animate = $(this).data('animate');
			if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
				$(this).data('animate', true);
				$(this).find('.circle-graph1').circleProgress({
					value: percent / 100,
					size: 400,
					thickness: 30,
					fill: {
						color: '#da3f81'
					}
				});
			}
		});
		$('.circle-bar2').each(function () {
			var elementPos = $(this).offset().top;
			var topOfWindow = $(window).scrollTop();
			var percent = $(this).find('.circle-graph2').attr('data-percent');
			var animate = $(this).data('animate');
			if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
				$(this).data('animate', true);
				$(this).find('.circle-graph2').circleProgress({
					value: percent / 100,
					size: 400,
					thickness: 30,
					fill: {
						color: '#68dda9'
					}
				});
			}
		});
		$('.circle-bar3').each(function () {
			var elementPos = $(this).offset().top;
			var topOfWindow = $(window).scrollTop();
			var percent = $(this).find('.circle-graph3').attr('data-percent');
			var animate = $(this).data('animate');
			if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
				$(this).data('animate', true);
				$(this).find('.circle-graph3').circleProgress({
					value: percent / 100,
					size: 400,
					thickness: 30,
					fill: {
						color: '#1b5a90'
					}
				});
			}
		});
	}

	if ($('.circle-bar').length > 0) {
		animateElements();
	}
	$(window).scroll(animateElements);

	// Preloader

	$(window).on('load', function () {
		if ($('#loader').length > 0) {
			$('#loader').delay(350).fadeOut('slow');
			$('body').delay(350).css({ 'overflow': 'visible' });
		}
	})

})(jQuery);