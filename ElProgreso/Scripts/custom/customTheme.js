(function ($) {
    "use strict"; // Start of use strict

    // Collapse Navbar
    var navbarCollapse = function () {
        if ($(".main-nav").offset().top > 100) {
            $(".main-nav").addClass("navbar-shrink");
        } else {
            $(".main-nav").removeClass("navbar-shrink");
        }
    };

    // Collapse now if page is not at top
    navbarCollapse();
    // Collapse the navbar when page is scrolled
    $(window).scroll(navbarCollapse);

})(jQuery); // End of use strict