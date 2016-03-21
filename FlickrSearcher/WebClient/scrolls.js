
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
}

function startFadeInPhotoDetails() {
    
    $('photo-details').removeClass("invisible");

    updateModalScroll();
}




