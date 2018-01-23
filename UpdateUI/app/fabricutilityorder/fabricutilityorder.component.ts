import {Component} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';

@Component({
    selector:'fabric-utility',
    templateUrl:'app/fabricutilityorder/fabricutility.html'
})

export class FabricUtilityComponent{

constructor(private authService:AuthenticationService){


}
ngOnInit(){
        this.authService.checkCredentials();
}
}