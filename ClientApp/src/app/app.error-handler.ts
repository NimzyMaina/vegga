import { ErrorHandler, Inject, NgZone, Injectable } from "@angular/core";
import { ToastyService } from "ng2-toasty";


@Injectable()
export class AppErrorHandler implements ErrorHandler {

    msg: string = 'An unexpected error occured';

    constructor(
        private ngZone: NgZone,
        private toasty: ToastyService) {}

    handleError(error: any): void {
        this.ngZone.run(() => {
            if (!navigator.onLine) {
                this.msg = 'Please check your internet connection';
            }else{
                if(error.status == 500)
                this.msg = error.statusText;
                
                if(error.status == 422){
                    var response = JSON.parse(error._body);
                    this.msg = response.message;
                }
            }

            this.toasty.error({
                title: 'Error',
                msg: this.msg,
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
        })
    }

}