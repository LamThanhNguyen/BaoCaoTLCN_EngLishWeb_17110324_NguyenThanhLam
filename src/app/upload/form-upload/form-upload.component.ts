import { Component, OnInit } from '@angular/core';
import { UploadFileService } from '../../_services/upload-file.service';
import { FileUpLoad } from '../../_models/fileupload'
import { Observable } from 'rxjs'

@Component({
    selector: 'app-form-upload',
    templateUrl: './form-upload.component.html',
    //styleUrls: ['./form-upload.component.css']
})

export class FormUploadComponent implements OnInit {
    selectedFiles: FileList;
    currentFileUpload: FileUpLoad;
    percentage: number;

    constructor(private uploadService: UploadFileService) {

    }

    ngOnInit() {

    }

    // Chọn file để Upload
    selectFile(event) {
        this.selectedFiles = event.target.files;
    }

    //upload file
    upload() {
        // Lấy file đầu tiên hiện tại trong selectedFiles
        const file = this.selectedFiles.item(0);
        // Set lại nơi chưa file
        this.selectedFiles = undefined;

        this.currentFileUpload = new FileUpLoad(file);

        // Push file để upload.
        this.uploadService.pushFileToStorage(this.currentFileUpload).subscribe(
            percentage => {
                this.percentage = Math.round(percentage);
            },
            error => {
                console.log(error);
            }
        );
    }
}