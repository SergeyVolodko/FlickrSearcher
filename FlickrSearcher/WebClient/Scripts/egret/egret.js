
/* ----------------------------- 
NiceScroll
----------------------------- */
$(document).ready(function () {
    'use strict';
    $("html").niceScroll({
        cursorcolor: '#E74C3C',
        cursoropacitymin: '1',
        cursorborder: '0px',
        cursorborderradius: '0px',
        mousescrollstep: 80,
        cursorwidth: '5px',
        cursorminheight: 60,
        horizrailenabled: false,
        zindex: 1090
    });
});





/* ----------------------------- 
Card Style Script
----------------------------- */
$(document).ready(function () {
    'use strict';
    var $el = $('#card-ul'),
		sectionFeature = $('#section-feature'),
		baraja = $el.baraja();

    if ($(window).width() > 480) {
        sectionFeature.appear(function () {
            baraja.fan({
                speed: 1500,
                easing: 'ease-out',
                range: 100,
                direction: 'right',
                origin: { x: 50, y: 200 },
                center: true
            });
        });
        $('#feature-expand').click(function () {
            baraja.fan({
                speed: 500,
                easing: 'ease-out',
                range: 100,
                direction: 'right',
                origin: { x: 50, y: 200 },
                center: true
            });
        });
    } else {
        sectionFeature.appear(function () {
            baraja.fan({
                speed: 1500,
                easing: 'ease-out',
                range: 80,
                direction: 'left',
                origin: { x: 200, y: 50 },
                center: true
            });
        });
        $('#feature-expand').click(function () {
            baraja.fan({
                speed: 500,
                easing: 'ease-out',
                range: 80,
                direction: 'left',
                origin: { x: 200, y: 50 },
                center: true
            });
        });
    }

    // Feature navigation
    $('#feature-prev').on('click', function (event) {
        baraja.previous();
    });

    $('#feature-next').on('click', function (event) {
        baraja.next();
    });

    // close Features
    $('#feature-close').on('click', function (event) {
        baraja.close();
    });
});



/* ----------------------------- 
Screenshot Load
----------------------------- */
$(document).ready(function () {
        'use strict';
        $('.view-project').on('click', function (e) {
            e.preventDefault();

            var href = $(this).attr('href') + ' .portfolio-project',
                portfolioWrap = $('.porfolio-container'),
                contentLoaded = $('#portfolio-load'),
                offset = $('#section-screenshots').offset().top;

            portfolioWrap.animate({ 'left': '-120%' }, { duration: 400, queue: false });
            portfolioWrap.fadeOut(400);
            $('html, body').animate({ scrollTop: offset }, { duration: 800, queue: true });
            setTimeout(function () { $('#portfolio-loader').fadeIn('fast'); }, 300);

            setTimeout(function () {
                contentLoaded.load(href, function () {
                    $('#portfolio-loader').fadeOut('fast');
                    contentLoaded.fadeIn(600).animate({ 'left': '0' }, { duration: 800, queue: false });
                    $('.back-button').fadeIn(600);
                });
            }, 400);



        });

        $('.backToProject').on('click', function (e) {
            e.preventDefault();

            var portfolioWrap = $('.porfolio-container'),
                contentLoaded = $('#portfolio-load');

            contentLoaded.animate({ 'left': '105%' }, { duration: 400, queue: false }).delay(300).fadeOut(400);
            $(this).parent().fadeOut(400);
            setTimeout(function () {
                portfolioWrap.animate({ 'left': '0' }, { duration: 400, queue: false });
                portfolioWrap.fadeIn(600);
            }, 500);

        });

});


function assignOpenModalLogic () {
    'use strict';
    $('.view-project').on('click', function (e) {
        e.preventDefault();

        var href = $(this).attr('href') + ' .portfolio-project',
			portfolioWrap = $('.porfolio-container'),
			contentLoaded = $('#portfolio-load'),
			offset = $('#section-screenshots').offset().top;

        portfolioWrap.animate({ 'left': '-120%' }, { duration: 400, queue: false });
        portfolioWrap.fadeOut(400);
        $('html, body').animate({ scrollTop: offset }, { duration: 800, queue: true });
        setTimeout(function () { $('#portfolio-loader').fadeIn('fast'); }, 300);

        setTimeout(function () {
            contentLoaded.load(href, function () {
                $('#portfolio-loader').fadeOut('fast');
                contentLoaded.fadeIn(600).animate({ 'left': '0' }, { duration: 800, queue: false });
                $('.back-button').fadeIn(600);
            });
        }, 400);



    });

    $('.backToProject').on('click', function (e) {
        e.preventDefault();

        var portfolioWrap = $('.porfolio-container'),
			contentLoaded = $('#portfolio-load');

        contentLoaded.animate({ 'left': '105%' }, { duration: 400, queue: false }).delay(300).fadeOut(400);
        $(this).parent().fadeOut(400);
        setTimeout(function () {
            portfolioWrap.animate({ 'left': '0' }, { duration: 400, queue: false });
            portfolioWrap.fadeIn(600);
        }, 500);

    });

};



