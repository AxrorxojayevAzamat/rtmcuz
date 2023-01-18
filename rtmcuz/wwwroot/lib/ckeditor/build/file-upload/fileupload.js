import {Plugin} from '../ckeditor.js';
import FileUploadEditing from "./fileuploadediting.js";
import FileUploadUI from "./fileuploadui.js";

export default class FileUpload extends Plugin {

    static get requires() {
        return [ FileUploadEditing, FileUploadUI ];
    }

    /**
     * @inheritDoc
     */
    static get pluginName() {
        return 'fileUpload';
    }
}