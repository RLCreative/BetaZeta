jQuery(document).ready(function () {
    var pxShow = 300;//height on which the button will show
    var fadeInTime = 100;//how slow/fast you want the button to show
    var fadeOutTime = 100;//how slow/fast you want the button to hide
    var scrollSpeed = 1000;//how slow/fast you want the button to scroll to top. can be a value, 'slow', 'normal' or 'fast'
    jQuery(window).scroll(function () {
        if (jQuery(window).scrollTop() >= pxShow) {
            jQuery("#backtotop").fadeIn(fadeInTime);
        } else {
            jQuery("#backtotop").fadeOut(fadeOutTime);
        }
    });

    jQuery('#backtotop a').click(function () {
        jQuery('html, body').animate({ scrollTop: 0 }, scrollSpeed);
        return false;
    });
});