import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';

@Injectable({
    providedIn: 'root'
})

export class AdminService {
    baseUrl = environment.apiUrl;

    constructor(
        private http: HttpClient
    ) { }

    // Lấy các quyền hiện tại có của user
    getUsersWithRoles() {
        return this.http.get<Partial<User[]>>(this.baseUrl + 'admin/users-with-roles');
    }

    // Update User với roles đầu vào.
    // http://localhost:5000/admin/edit-roles/{{username}}?roles={{roles}}
    updateUserRoles(username: string, roles: string[]) {
        return this.http.post(this.baseUrl + 'admin/edit-roles/' + username + '?roles=' + roles, {});
    }

}