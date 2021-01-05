import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Member } from '../_models/member';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
    providedIn: 'root'
})

export class MembersService {
    baseUrl = environment.apiUrl;
    members: Member[] = [];
    memberCache = new Map();
    user: User;
    userParams: UserParams;

    constructor(private http: HttpClient, private accountService: AccountService) {
        this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
            this.user = user;
            this.userParams = new UserParams(user);
        })
    }

    getUserParams() {
        return this.userParams;
    }

    setUserParams(params: UserParams) {
        this.userParams = params;
    }

    resetUserParams() {
        this.userParams = new UserParams(this.user);
        return this.userParams;
    }

    getMembers(userParams: UserParams) {
        var response = this.memberCache.get(Object.values(userParams).join('-'));
        if (response) {
            return of(response);
        }

        let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

        return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
            .pipe(map(response => {
                this.memberCache.set(Object.values(userParams).join('-'), response);
                return response;
            }))
    }

    getMember(username: string) {
        debugger;
        const member = [...this.memberCache.values()]
            .reduce((arr, elem) => arr.concat(elem.result), [])
            .find((member: Member) => member.username === username);

        if (member) {
            return of(member);
        }

        return this.http.get<Member>(this.baseUrl + 'users/' + username);
    }

    updateMember(member: Member) {
        return this.http.put(this.baseUrl + 'users', member).pipe(
            map(() => {
                const index = this.members.indexOf(member);
                this.members[index] = member;
            })
        )
    }
}