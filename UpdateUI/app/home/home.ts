import {Component} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
@Component({
    selector:'home',
    templateUrl:'app/home/home.html'
})
export class HomeComponent{
    constructor(private service:AuthenticationService){
       
            }
    ngOnInit(){
        this.service.checkCredentials();
    }
}