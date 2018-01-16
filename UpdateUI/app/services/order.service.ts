import {Injectable} from '@angular/core';
import{HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
@Injectable()
export class OrderMiscService {
    headers = new HttpHeaders();
    rootUrl="http://localhost:62253/api/Customer/"
     constructor( private http:HttpClient){
        this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
        this.headers.append('Access-Control-Allow-Methods', 'GET');
        this.headers.append('Access-Control-Allow-Origin', '*');
     }

     GetColors(type){
      return this.http.get(this.rootUrl+"GetColors/"+type);
     }
     GetMaterial(type){
        return this.http.get(this.rootUrl+"GetMaterial/"+type);
       }
    GetCordStyle(type){
        return  this.http.get(this.rootUrl+"GetCordStyle/"+type);
       }
       GetSlatStyle(type){
        return  this.http.get(this.rootUrl+"GetSlatStyle/"+type);
       }
       GetSize(type){
        return  this.http.get(this.rootUrl+"GetSize/"+type);
       }      

    SaveOrderInitiation(order:OrderInitiation)
    {
        debugger;
        let headers = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        return this.http.post(this.rootUrl+"OrderInitiation",JSON.stringify(order),{headers:headers});
    }
    SaveOrderDetails(orderDetails:OrderData[])
    {
        debugger;
        let headers = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        return this.http.post(this.rootUrl+"OrderDetails",JSON.stringify(orderDetails),{headers:headers});
    }

}