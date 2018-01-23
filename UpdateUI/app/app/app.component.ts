import{Component} from '@angular/core';
import {Router} from '@angular/router';
import {LoginComponent} from '../app/login/login.component';
import {AuthenticationService, User} from '../app/services/auth.service';
@Component({
selector:'my-app',
// providers:[AuthService],
templateUrl:'app/Views/appcomponent.html',
styleUrls:['app/Views/appcomponent.css']
})

export class AppComponent{
// showMenu:boolean=false;
    constructor(private route:Router,private service:AuthenticationService){

    }
LogOut(){
    // this.showMenu=false;
        this.service.logout();
}
}
