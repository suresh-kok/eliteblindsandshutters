import{Routes} from '@angular/router';
import { AppComponent } from './app.component';
import{ OrderComponent} from '../app/Order/order.component';
import {HomeComponent} from '../app/home/home';
import {LoginComponent} from '../app/login/login.component';
import {RollerOrderComponent} from '../app/rollerorder/rollerorder.component';
import{ OverSeaComponent} from '../app/overseaorder/overseaorder.component';
import {FabricUtilityComponent} from '../app/fabricutilityorder/fabricutilityorder.component'
import{NotAuthorisedComponent} from '../app/erropages/unauthorised';
import {AuthenticationService} from '../app/services/auth.service';

export const routes:Routes=[
    {path:'',component:LoginComponent},
    {path:'Home',component:HomeComponent},
    {path:'VenetianOrder',component:OrderComponent},
    {path:'RollerOrder',component:RollerOrderComponent},
    {path:'OverSeaOrder',component:OverSeaComponent},
    {path:'FabricOrder',component:FabricUtilityComponent},
    {path:'unauthorized',component:NotAuthorisedComponent}
  
    // {path:'MovieDetails/:id',component:MovieListComponent,canActivate:[AuthService]},
    // {path:'Registration',component:ReactiveUserComponent},
    // {path:'Books',component:BookDataComponent},
    // {path:'unauthorized',component:NotAuthorisedComponent},
    // {path:'Cart',component:CartComponent},
    // {path:'**',component:PageNotFoundComponent}
    

]