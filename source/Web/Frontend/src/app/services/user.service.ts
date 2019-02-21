import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "../core/services/token.service";
import { ResultModel } from "../models/result.model";
import { SignInModel } from "../models/signIn.model";
import { TokenModel } from "../models/token.model";
import { UserModel } from "../models/user.model";

@Injectable({ providedIn: "root" })
export class AppUserService {
    constructor(
        private readonly http: HttpClient,
        private readonly router: Router,
        private readonly appTokenService: AppTokenService) { }

    add(userModel: UserModel) {
        return this.http.post(`Users`, userModel);
    }

    delete(userId: number) {
        return this.http.delete(`Users/${userId}`);
    }

    list() {
        return this.http.get(`Users`);
    }

    select(userId: number) {
        return this.http.get(`Users/${userId}`);
    }

    signIn(signInModel: SignInModel): void {
        this.http
            .post(`Users/SignIn`, signInModel)
            .subscribe((resultModel: ResultModel<TokenModel>) => {
                this.appTokenService.set(resultModel.data.token);
                this.router.navigate(["/main/home"]);
            });
    }

    signOut() {
        if (this.appTokenService.any()) {
            this.http.post(`Users/SignOut`, {}).subscribe();
        }

        this.appTokenService.clear();
        this.router.navigate(["/login"]);
    }

    update(userModel: UserModel) {
        return this.http.put(`Users/${userModel.userId}`, userModel);
    }
}
