$(document).ready(function () {
    const swiper = new Swiper('.banner-swiper', {
        loop: true,
        navigation: {
            nextEl: '.banner-slide-prev',
            prevEl: '.banner-slide-next',
        },

    });
});