$('#image').on("change", function (e) {
    if (e.target.value) {
        $("#attachmentUpdate").hide();
        $("#ImageId").val(-1);
    } else {
        $("#attachmentUpdate").show()
        $("#ImageId").val(null);
    }
});