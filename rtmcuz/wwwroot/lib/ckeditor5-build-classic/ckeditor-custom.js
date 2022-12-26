var myEditor = null;




$(document).ready(function () {
    
    //html editor
    setCkEditor();
    jQuery.validator.setDefaults({
        // This will ignore all hidden elements alongside `contenteditable` elements
        // that have no `name` attribute
        ignore: ":hidden, [contenteditable='true']:not([name])"
      });

      

    // $('form').each(function () {
    //     debugger

    //     if ($(this).data('validator'))
    //         $(this).data('validator').settings.ignore = ".ck-editor *";
    // });
});

function setCkEditor() {
    ClassicEditor
        .create(document.querySelector("#Content"), {
            Language: 'en-ru', //Set language
            Toolbar: { //Set the toolbar
                items: [
                    'heading',
                    '|',
                    'bold',
                    'italic',
                    'link',
                    'bulletedList',
                    'numberedList',
                    'imageUpload',
                    'blockQuote',
                    'insertTable',
                    //'mediaEmbed',
                    'undo',
                    'redo'
                ]
            },
            Ckfinder: { //Set the upload path
                uploadUrl: '/Home/UploadCKEditorImage'
                // Back-end processing upload logic returns json data, including uploaded (option true / false) and url two fields
            },
            link: {
                // Automatically add target="_blank" and rel="noopener noreferrer" to all external links.
                addTargetToExternalLinks: true,
    
                // Let the users control the "download" attribute of each link.
                decorators: [
                    {
                        mode: 'manual',
                        label: 'Downloadable',
                        attributes: {
                            download: 'download'
                        }
                    }
                ]
            }
    
        })
        .then(editor => {
            myEditor = editor;
        })
        .catch(error => {
            console.error(error);
        });
}
