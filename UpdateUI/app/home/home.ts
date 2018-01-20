import {Component} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
@Component({
    selector:'home',
    templateUrl:'app/home/home.html'
})
export class HomeComponent{
<<<<<<< HEAD
    OrderInfoList:OrderInitiation[];
    CustomerID:number;
    ViewDetailsBool:boolean=false;
    OrderDetails:OrderData[];
    OrderItem:OrderInitiation;
    constructor(private service:AuthenticationService,private orderService:OrderMiscService){
=======
    constructor(private service:AuthenticationService){
>>>>>>> 8457322de887d7f1e2f6db925bf31a49d4feedbb
       
            }
    ngOnInit(){
        this.service.checkCredentials();
<<<<<<< HEAD
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
=======
>>>>>>> 8457322de887d7f1e2f6db925bf31a49d4feedbb
    }
}