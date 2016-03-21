
/* ----------------------------- 
NiceScroll
----------------------------- */

var theNiceScroll = {
    cursorcolor: '#E74C3C',
    cursoropacitymin: '1',
    cursorborder: '0px',
    cursorborderradius: '0px',
    mousescrollstep: 80,
    cursorwidth: '5px',
    cursorminheight: 60,
    horizrailenabled: false,
    zindex: 1090
};

$(document).ready(function () {
    'use strict';
    $("html").niceScroll(theNiceScroll);
});

function updateModalScroll() {
    $(".photo-detail-modal")
        .niceScroll(theNiceScroll);
    $(".photo-detail-modal")
        .addClass('scroll-added');
}

function removeModalScroll() {
    $(".photo-detail-modal").getNiceScroll().remove();
    $(".photo-detail-modal")
        .removeClass('scroll-added');
}

function updateMainScroll() {
    $("html")
        .niceScroll(theNiceScroll);
}

function removeMainScroll() {
    $("html").getNiceScroll().remove();
}



/* ----------------------------- 
Animations
----------------------------- */
function fadeOutPhotoDetails() {

    removeModalScroll();
    $('photo-details').animate({ 'left': '-100%' }, { duration: 600, queue: false });
    $('photo-details').animate({ 'right': '100%' }, { duration: 600, queue: false });
    $('photo-details').animate({}, {}).delay(150).fadeOut(450);
    setTimeout(function () {
        $('photo-details').attr('style', function (i, style) {
            return style = "";
        });
    }, 1200);
    $("html").getNiceScroll().show(); //.locked = false;

}

function startFadeInPhotoDetails() {

    $("html").getNiceScroll().hide();//.locked = true;
    $('photo-details').removeClass("invisible");

}


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





