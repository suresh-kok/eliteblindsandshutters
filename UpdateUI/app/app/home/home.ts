import {Component} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderMiscService} from '../../app/services/order.service'
@Component({
    selector:'home',
    templateUrl:'app/home/home.html'
})
export class HomeComponent{
    OrderInfoList:any[];
    CustomerID:number;
    ViewDetailsBool:boolean=false;
    OrderDetails:OrderData[];
    OrderItem:OrderInitiation;
    constructor(private service:AuthenticationService,private orderService:OrderMiscService){
       
            }
    ngOnInit(){
        this.service.checkCredentials();
        debugger;
        this.CustomerID=this.service.GetCustomerId();
        this.GetOrders();
    }
    GetOrders(){
        this.orderService.GetOrders(this.CustomerID,"0","0","OrderDate").subscribe(
    data=>{
        this.OrderInfoList=JSON.parse(data.toString())},
    err=>console.error(err)
        );
    }
    ViewDetails(OrderID)
    {
        debugger;
        this.OrderItem=this.OrderInfoList.find(f=>f.OrderID==OrderID);
        this.orderService.GetOrderDetails(OrderID).subscribe(
         data=>{
             
            if(data) 
            this.OrderDetails=JSON.parse(data.toString())
            this.ViewDetailsBool=true;
        },
         err=>console.error(err)
        );
    }
    ReturnToAllOrders(){
        this.ViewDetailsBool=false;
    }
}