import { Injectable } from '@angular/core'
import { AngularFireDatabase, AngularFireList } from '@angular/fire/database';
import { AngularFireStorage } from '@angular/fire/storage';

import { FileUpLoad } from '../_models/fileupload';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})

export class UploadFileService {
    private basePath = '/uploads';

    constructor(private db: AngularFireDatabase, private storage: AngularFireStorage) {}

    // upload hình ảnh lên Firebase Storage
    // sau đó lưu thông tin trả về từ Firebase Storage sau khi upload hình ảnh vào RealTime Database              
    pushFileToStorage(fileUpload: FileUpLoad): Observable<number> {
        const filePath = `${this.basePath}/${fileUpload.file.name}`;
        const storageRef = this.storage.ref(filePath);
        const uploadTask = this.storage.upload(filePath, fileUpload.file);

        uploadTask.snapshotChanges().pipe(
            finalize(() => {
                storageRef.getDownloadURL().subscribe(downloadURL => {
                    console.log('File available at', downloadURL);
                    fileUpload.url = downloadURL;
                    fileUpload.name = fileUpload.file.name;
                    console.log(fileUpload);
                    this.saveFileData(fileUpload);
                });
            })
        ).subscribe();

        // Trả về phần trăm hoàn thành việc tải lên.
        return uploadTask.percentageChanges();
    }

    private saveFileData(fileUpload: FileUpLoad) {
        this.db.list(this.basePath).push(fileUpload);
    }

    getFileUploads(numberItems): AngularFireList<FileUpLoad> {
        return this.db.list(this.basePath, ref =>
            ref.limitToLast(numberItems));
    }

    deleteFileUpload(fileUpload: FileUpLoad) {
        this.deleteFileDatabase(fileUpload.key)
            .then(() => {
                this.deleteFileStorage(fileUpload.key);
            })
            .catch(error => console.log(error));
    }

    private deleteFileDatabase(key: string) {
        return this.db.list(this.basePath).remove(key);
    }

    private deleteFileStorage(name: string) {
        const storageRef = this.storage.ref(this.basePath);
        storageRef.child(name).delete();
    }
}
