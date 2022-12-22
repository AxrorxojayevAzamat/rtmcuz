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

let $images = $(".home-news, .last-news-img");
console.log($images);
for (const img of $images) {
    console.log(img.offsetWidth, img.offsetHeight);
    let className = img.offsetWidth > img.offsetHeight ? 'longer-width' : img.offsetWidth < img.offsetHeight ? 'longer-height' : '';
    if (className) {
        console.log(className);
        img.classList.add(className);
    }
}