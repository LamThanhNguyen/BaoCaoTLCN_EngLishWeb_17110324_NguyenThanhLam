import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Directive({
    selector: '[appHasRole]'
})

export class HasRoleDirective implements OnInit {
    @Input() appHasRole: string[];
    isVisible = false;

    constructor(
        private viewContainerRef: ViewContainerRef,
        private templateRef: TemplateRef<any>,
        private authService: AuthService
    ) { }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        
        //Chứa các role nằm trong userRoles của user hiện tại đang đăng nhập
        const userRoles = this.authService.decodedToken.role as Array<string>;

        if (!userRoles) {
            this.viewContainerRef.clear();
        }

        //appHasRole là giá trị nhập vào của Component này
        if (this.authService.roleMatch(this.appHasRole)) {
            if (!this.isVisible) {
                this.isVisible = true;
                this.viewContainerRef.createEmbeddedView(this.templateRef);
            } else {
                this.isVisible = false;
                this.viewContainerRef.clear();
            }
        }
    }
}