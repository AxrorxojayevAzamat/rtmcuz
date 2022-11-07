$('.question').click(function () {
    $(this).toggleClass("opened")
    $(this).children(".icon").toggleClass("icon-chevrone-bottom").toggleClass("icon-chevrone-top")
});