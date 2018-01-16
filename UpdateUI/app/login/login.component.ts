import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {MiscellaneousService} from '../../app/services/misc.service';
import {Observable} from 'rxjs/Observable';
import{HttpClient,HttpHeaders,HttpResponse} from '@angular/common/http';
// import { AuthService } from 'angular4-social-login';
// import { SocialUser } from 'angular4-social-login';
// import { GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
declare const gapi: any;
@Component({
    selector:'login',
    templateUrl:'app/login/login.html',
    styleUrls:['app/login/login.css'],
    // providers:[AuthService]
})
export class LoginComponent{
user:User;
errorMsg:any;
showMsg:boolean=false;
showSuccessMsg:boolean=false;
showSignUp:boolean=false;

public auth2:any;
public countries;
public country="";
public regions;
public region="";
public cities;
public city="";
regionLoaded:boolean=false;
showModal:boolean=false;
// GOOGLE_CLIENT_ID:any = '69036395730-22jg334m029vcak2qvbdt610ra391ooe.apps.googleusercontent.com';

    constructor(private route:Router,private service:AuthenticationService
                ,private http:HttpClient,private miscService:MiscellaneousService
              )
    {
        this.user=new User();
        // this.LoadCountries();
    }
           ngOnInit(){
           
           } 
            public onSignIn(googleUser) {
                var user : any;
                debugger;
                let userAuthToken = googleUser.id_token;
                let userDisplayName = googleUser.getBasicProfile().getName();
                console.log(this);
            
            };             
            LoadCountries()
            {
                       this.miscService.GetAllCountries().subscribe(
                          data => { this.countries = data},
                          err => console.error(err)
                        );

            }
            LoadRegions(){
                this.region="";
                this.user.City="";
                // this.regions=this.miscService.GetRegions(this.country);
                this.miscService.GetRegions(this.user.Country).subscribe(
                    data => { 
                        this.regions = data;
                    },
                    err => console.error(err)
                  );
            }
            LoadCities()
            {
                this.city="";
             
                this.miscService.GetCities(this.user.Country,this.region).subscribe(
                    data => { 
                       
                        this.cities = data;
                    },
                    err => console.error(err)
                  );
            }
            GoogleSignIn(){
                // this._googleAuth.signIn(GoogleLoginProvider.PROVIDER_ID);
                // this._googleAuth.authenticateUser((profile)=>{
                //     // let data=profile.
                //     debugger;
                //     console.log(profile);
                //   });
            //     gapi.load('auth2',  () => {
            //    this.auth2= gapi.auth2.init({ 
            //         client_id: this.GOOGLE_CLIENT_ID, 
            //         scope: 'profile email'
            //         // response_type: 'id_token permission'
            //     }, this.onSignIn);
           // }
        // );
            }
            signOut()
            {
             gapi.load('auth2',()=>{
                 debugger;
               let auth= gapi.auth2.getAuthInstance();
             });  
            }
    Login()
    {
    //   let res= this.service.login(this.user);
    //   debugger;
      this.service.login(this.user).subscribe( data => {
        debugger;
        if(data){
            // let
            // this.user.CustomerId=data;
        localStorage.setItem("user", data);
        this.route.navigate(['Home']); 
    }else{
        this.errorMsg = 'Failed to login';
        this.showMsg=true;
    }
        return data},
        err => console.error(err))
    }
    ShowLogin()
    {
        this.user=new User();
        this.showSignUp=false;
    }
    SignUp(){
        this.user=new User();
        this.showSignUp=true;
    }

    Register(){
        if(this.Validation()==false)
        {
            return false;   
        }
        this.user.IsActive=1;
        this.service.NewUser(this.user).subscribe(
            data => { if(data){
                this.user=new User();
               alert('Registration Successfull. Please login to continue');
               this.showSignUp=false;
                // this.showSuccessMsg=true;
                // this.showMsg=false;
            }
        else{
            alert('Error occured. Please try again');
            // this.errorMsg = 'Failed to login';
            // this.showMsg=true;
            // this.showSuccessMsg=false;
        }},
            err => console.error(err)
          );
        
        
    }
    Validation(){
        if(this.user.Password!=this.user.ConfirmPassword){
            alert("Passwords donot match");
            return false;
        }
    }
   
}