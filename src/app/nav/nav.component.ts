import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
    model: any = {};

    constructor(
        public accountService: AccountService,
        private router: Router,
    ) { }

    ngOnInit(): void {
        
    }

    login() {
        debugger;
        this.accountService.login(this.model).subscribe(() => {
            this.router.navigateByUrl('/');
            console.log("Đăng Nhập Thành Công!");
        }, error => {
            console.log(error);
        })
    }

    logout() {
        this.accountService.logout();
        this.router.navigateByUrl('/');
    }

    signOut() {
        const auth2 = gapi.auth2.getAuthInstance();
        auth2.signOut().then(function () {
            this.router.navigateByUrl('/');
            console.log('User signed out.');
        });
    }
}