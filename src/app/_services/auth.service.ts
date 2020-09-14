import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    baseUrl = environment.apiUrl + 'auth/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;
    currentUser: User;
    

    constructor(
        private http: HttpClient
    ) { }

    login(model: any) {
        return this.http.post(this.baseUrl + 'login', model, {})
            .pipe(
                map((response: any) => {
                    // user = response của server/login
                    const user = response;

                    if (user) {
                        // Lưu giữ token và user.
                        localStorage.setItem('user', JSON.stringify(user.user));
                        localStorage.setItem('token', user.token);

                        // decodedToken từ user.token để sử dụng.
                        this.decodedToken = this.jwtHelper.decodeToken(user.token);
                        // Lưu giữ user hiện tại
                        this.currentUser = user.user;
                    }
                })
            );
    }

    register(user: User) {
        return this.http.post(this.baseUrl + 'register', user);
    }

    loggedIn() {
        // Lấy token hiện tại đang được lưu giữ trên máy.
        const token = localStorage.getItem('token');
        // token hết hạn trả về false, token còn hiệu lực thì trả về false.
        return !this.jwtHelper.isTokenExpired(token);
    }

    roleMatch(allowedRoles): boolean {
        let isMatch = false;
        //Lấy ra role hiện tại được lưu giữ trong token sau khi được decoded
        const userRoles = this.decodedToken.role as Array<string>;

        //Nếu đối số role của hàm này truyền vào trùng khớp với một trong các roles của user thì true không thì false.
        allowedRoles.forEach(element => {
            if (userRoles.includes(element)) {
                isMatch = true;
                return;
            }
        });
        return isMatch;
    }
}