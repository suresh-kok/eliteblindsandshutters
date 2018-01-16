import {Injectable} from '@angular/core';
import {CanActivate,Router} from '@angular/router';
import{HttpClient,HttpHeaders,HttpRequest} from '@angular/common/http';
import{RequestOptions,Headers} from '@angular/http';
import { HttpHandler } from '@angular/common/http/src/backend';
import { parse } from 'url';
// import {AuthService} from 'angular4-oauth-login';
export class User {
      public CustomerId:any;
      public FirstName: string;
      public MiddleName: string;
      public LastName: string;
      public Email: string;
      public DOB: any;
      public Gender: string;
      public IsActive: any;
      public Mobile: string;
      public Address: string;
      public City: string;
      public Country: string;
      public Pincode: string;
      public Password: string;
      public ConfirmPassword:string;
  }
//   var users = [
//     new User('admin@admin.com','adm9'),
//     new User('user1@gmail.com','a23')
//   ];
@Injectable()
export class AuthenticationService implements CanActivate{
    showMenu:boolean=false;
    rootUrl="http://localhost:62253/api/Customer/";
    loginUrl:any=this.rootUrl+ "CheckLogin/";
    registerUrl:any=this.rootUrl+"PostCustomer/";
constructor(private route:Router,private http:HttpClient)
{

}
logout() {
    localStorage.removeItem("user");
    this.showMenu=false;
    this.route.navigate(['']);
    
  }
  login(user){
    return this.http.get(this.loginUrl+ user.Email+"/"+user.Password);
    //  return false;
  }
 
   checkCredentials(){
    if (localStorage.getItem("user") === null){
        this.route.navigate(['']);
    }
    
  } 
  GetCustomerId(){
    let cust=localStorage.getItem("user");
    if(cust!=null){
        let temp=JSON.parse(cust);
        return temp.CustomerId;
    }  
    
  }
    canActivate(){
        if (localStorage.getItem("user") === null){
             this.route.navigate(['/unauthorized']);
        return false;
        }
        else{
            return true;
        }
       
    }
    NewUser(user:User)
    {
        debugger;
        let headers = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        var strUser=JSON.stringify(user)
        // let options = new RequestOptions({ headers: headers });
        // var data =JSON.stringify(user);
        return this.http.post(this.registerUrl,JSON.stringify(user),{headers:headers});
        //  return false;
    }
}