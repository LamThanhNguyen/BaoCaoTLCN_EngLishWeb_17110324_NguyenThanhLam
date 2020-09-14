import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { error } from 'protractor';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
    @Output() cancelRegister = new EventEmitter();
    user: User;
    registerForm: FormGroup;

    constructor(
        private authService: AuthService,
        private router: Router,
        private fb: FormBuilder
    ) { }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        
    }

    createRegisterForm() {
        this.registerForm = this.fb.group({
            userName: ['', Validators.required],
            knownAs: ['', Validators.required],
            dateOfBirth: ['', Validators.required],
            city: ['', Validators.required],
            country: ['', Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required]
        }, {validators: this.passwordMatchValidator});
    }

    // Nếu trùng khớp password thì trả về null. Không thì trả về Object mismatch=true
    passwordMatchValidator(g: FormGroup) {
        return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};                                                 
    }

    register() {
        if ( this.registerForm.valid) {
            // user = tất cả giá trị của form group registerForm.
            this.user = Object.assign({}, this.registerForm.value);
            this.user.dateOfBirth = new Date(this.user.dateOfBirth.year, this.user.dateOfBirth.month, this.user.dateOfBirth.day);
            // Đăng ký user với thông tin user nhập vào.
            this.authService.register(this.user).subscribe(() => {
                
            }, error => {
                
            }, () => {
                // Nếu đăng ký thành công thì tự động login và router đến /members.
                this.authService.login(this.user).subscribe(() => {
                    this.router.navigate(['/members']);
                });
            });
        }
    }

    // Sự kiện cancelRegister đưa ra giá trị false.
    cancel() {
        this.cancelRegister.emit(false);
    }

}