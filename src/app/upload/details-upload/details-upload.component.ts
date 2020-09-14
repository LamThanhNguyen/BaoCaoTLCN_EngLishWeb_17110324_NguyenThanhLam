import { Component, OnInit, Input } from '@angular/core';
import { FileUpLoad } from '../../_models/fileupload';
import { UploadFileService } from '../../_services/upload-file.service';

@Component({
    selector: 'app-details-upload',
    templateUrl: './details-upload.component.html',
    //styleUrls: ['./details-upload.component.css']
})

export class DetailsUploadComponent implements OnInit {
    @Input() fileUpload: FileUpLoad;

    constructor(private uploadService: UploadFileService) { }

    ngOnInit() {

    }

    deleteFileUpload(fileUpload) {
        this.uploadService.deleteFileUpload(fileUpload);
    }
}