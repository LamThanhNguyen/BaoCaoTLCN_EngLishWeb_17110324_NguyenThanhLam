import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';

import { UploadFileService } from '../../_services/upload-file.service';

@Component({
    selector: 'app-list-upload',
    templateUrl: './list-upload.component.html',
    //styleUrls: ['./list-upload.component.css']
})

export class ListUploadComponent implements OnInit {
    fileUploads: any[];

    constructor(
        private uploadService: UploadFileService
    ) {

    }

    ngOnInit() {
        // Sử dụng snapshotChanges().pipe(map()) để lưu trữ key
        this.uploadService.getFileUploads(6).snapshotChanges().pipe(
            map(changes =>
                changes.map(c => ({ key: c.payload.key, ...c.payload.val() })))
        ).subscribe(fileUploads => {
            this.fileUploads = fileUploads;
        });
    }

    
}