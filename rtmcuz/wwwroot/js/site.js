$('.question, .document-category').click(function () {
    $(this).toggleClass("opened")
    $(this).children(".icon").toggleClass("icon-chevrone-bottom").toggleClass("icon-chevrone-top")
});

$('.searching-icon').click(function () {
    $('.searching').show()
    $('.navbar').hide()
    $('.header-bottom').addClass('media')
})

$('.closing-icon').click(function () {
    $('.navbar').show()
    $('.searching').hide()
    $('.header-bottom').removeClass('media')
})

$("#open-searching").click(function () {
    setTimeout(() => {
        $("#search-input").focus()
    })
})