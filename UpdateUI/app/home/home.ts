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
    RoleID:number;
    ViewDetailsBool:boolean=false;
    OrderDetails:OrderData[];
    OrderItem:OrderInitiation;
    Criteria:any="select";
    Value:any="";
    constructor(private service:AuthenticationService,private orderService:OrderMiscService){
       
            }
    ngOnInit(){
        this.service.checkCredentials();
        debugger;
        this.CustomerID=this.service.GetCustomerId();
        this.RoleID=this.service.GetCustomerRoleId();
        this.GetOrders();
    }
    GetOrders(){
        this.orderService.GetOrders(this.CustomerID,this.Criteria,this.Value==""?"0":this.Value,"OrderDate").subscribe(
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
    Search(){
        this.GetOrders();
    }
    ReturnToAllOrders(){
        this.ViewDetailsBool=false;
    }
    Approve(OrderID){
        if(this.RoleID==1){
      this.orderService.ApproveOrder(OrderID).subscribe(
          data=>{
              if(data){
                  this.ChangeOrderStatus(OrderID,3,true);
              }
          }
      )
    }
    else if(this.RoleID==2){
        this.ChangeOrderStatus(OrderID,10,true);
    }
    else if(this.RoleID==3){
        this.ChangeOrderStatus(OrderID,11,true);
    }
    }
    Reject(OrderID){
        if(this.RoleID==1){
            this.ChangeOrderStatus(OrderID,7,false);            
        }
        else if(this.RoleID==2){
            this.ChangeOrderStatus(OrderID,8,false);
        }
        else if(this.RoleID==3){
            this.ChangeOrderStatus(OrderID,9,false);
        }
    }
    ChangeOrderStatus(OrderID,status,isApprove){
      this.orderService.ChangeOrderStatus(OrderID,status).subscribe(
          data=>{
              if(data && isApprove){
                  alert('Order '+OrderID + 'approved successfully');
                  this.ViewDetailsBool=false;
              }
              else if(data && !isApprove){
                alert('Order '+OrderID + 'rejected successfully');
                this.ViewDetailsBool=false;
              }
          }
      )
    }
}