$(document).ready(function () {
    $(".bookModal").click(function (ev) {
        ev.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(response => response.text())
            .then(data => {
                $("#quickModal .modal-dialog").html(data);
                $("#quickModal").show();
                $('.product-details-slider').slick({
                    slidesToShow: 1,
                    arrows: false,
                    fade: true,
                    draggable: false,
                    swipe: false,
                    asNavFor: '.product-slider-nav'
                });

                $('.product-slider-nav').slick({
                    infinite: true,
                    autoplay: true,
                    autoplaySpeed: 8000,
                    slidesToShow: 4,
                    arrows: true,
                    prevArrow: '<button class="slick-prev"><i class="fa fa-chevron-left"></i></button>',
                    nextArrow: '<button class="slick-next"><i class="fa fa-chevron-right"></i></button>',
                    asNavFor: '.product-details-slider',
                    focusOnSelect: true
                });
            });
    });
});
