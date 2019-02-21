import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { AppModalService } from "../services/modal.service";

@Injectable()
export class AppErrorHandler implements ErrorHandler {
    constructor(private readonly injector: Injector) { }

    handleError(exception: any) {
        if (!exception.status) { return; }

        switch (exception.status) {
            case 401: {
                const router = this.injector.get(Router);
                router.navigate(["/login"]);
                break;
            }
            case 422: {
                const appModalService = this.injector.get(AppModalService);
                appModalService.alert(exception.error.message);
                break;
            }
            default: {
                console.error(exception);
                break;
            }
        }
    }
}
