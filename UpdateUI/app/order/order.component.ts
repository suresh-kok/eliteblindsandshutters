import {Component,ElementRef,ViewChild,Input,EventEmitter} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderTypeComponent} from './orderType.component';
import {OrderListComponent} from './orderlist.component';
import {OrderMiscService} from '../../app/services/order.service'
@Component({
    selector:'place-order',
    templateUrl:'app/order/ordercomponent.html'
})
export class OrderComponent{
    @ViewChild('extra') extra: ElementRef;
    @ViewChild('details') details: ElementRef;
    @ViewChild('home') home: ElementRef;
    @ViewChild('basic') basic:ElementRef;
    orderItem:OrderData
    OrderDetails:any[]=[];
    enableCheckOut:boolean=false;
    OrderInitiation:OrderInitiation;
    public slatStyles;
    public cordStyles;
    OrderConfirmed:bool:false;

    constructor(private service:AuthenticationService,element: ElementRef
    ,private orderService:OrderMiscService){
        this.orderItem=new OrderData();
        this.OrderInitiation= new OrderInitiation();
       
            }
    ngOnInit(){
        this.service.checkCredentials();
        
        this.GetCordStyles();
 
        this.GetSlatStyles();
    }
    ngAfterViewInit(){
        let el: HTMLElement = this.basic.nativeElement as HTMLElement;
        el.click();
    }
    AddItem()
    {
        debugger;
        this.OrderDetails.push(this.orderItem);
        this.orderItem=new OrderData();
    }
    OrderConfirm(){
        debugger;
        // this.element.nativeElement.querySelector('#extraTab').click();
        let el: HTMLElement = this.extra.nativeElement as HTMLElement;
        // let elDetails: HTMLElement = this.details.nativeElement as HTMLElement;
        // let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        el.click();
        // elDetails.
        this.enableCheckOut=true;
    }
    Proceed(){
        let elDetails: HTMLElement = this.details.nativeElement as HTMLElement;
        elDetails.click();
    }
    Delete(item:any)
    {
        // this.OrderDetails.pop(item);
    }

    GetSlatStyles(){
        this.orderService.GetSlatStyle('Venetian').subscribe(
            data => { 
               this.slatStyles = JSON.parse(data)
            },
            err => console.error(err)
          );
    }
    GetCordStyles(){
        this.orderService.GetCordStyle('OVenetian').subscribe(
            data => {
                this.cordStyles = JSON.parse(data)
            },
            err => console.error(err)
          );
    }
    onCordChange()
    {
        debugger;
      let cordText=this.cordStyles.filter(cord=>cord.CordStyleID==this.orderItem.CordStyleID);
      this.orderItem.cordStyleText=cordText[0].CordStyleDesc;
    }
    onSlatChange()
    {
        debugger;
      let slatText=this.slatStyles.filter(slat=>slat.SlatStyleID==this.orderItem.SlatStyleID);
      this.orderItem.slatStyleText=slatText[0].SlatStyleDesc;
    }
    onReturnChange(){
        if(this.orderItem.ReturnRequired==true)
        this.orderItem.ReturnRequiredText="Yes";
        else
        this.orderItem.ReturnRequiredText="No";
    }
    onMountChange()
    {
        if(this.orderItem.MountType==true)
        this.orderItem.MountTypeText="In";
        else
        this.orderItem.MountTypeText="Out";
    }
    onNotify(message:any):void {
        this.OrderInitiation=message;
        let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        elHome.click();
      }
      SaveOrder(){
          let orderId:any=0;
          debugger;
          this.OrderInitiation.CustomerId=this.service.GetCustomerId();
          this.OrderInitiation.orderType=1;
          this.OrderInitiation.OrderDate=new Date();
          this.OrderInitiation.OrderStatus="Order Received";
        this.orderService.SaveOrderInitiation(this.OrderInitiation).subscribe(
         data=>{
             debugger;
             if(data){

                 var orderData=JSON.parse(data);
                 if(orderData.OrderID!=0)
                 {
                     this.OrderInitiation.OrderID=orderData.OrderID;
                     for(var i=0;i<this.OrderDetails.length;i++)
                     {
                         this.OrderDetails[i].OrderID=orderData.OrderID;
                     }

                    this.orderService.SaveOrderDetails(this.OrderDetails).subscribe(
                        data=>{
                            if(data)
                            {
                                this.OrderConfirmed=true;
                                alert('Order saved successfully');
                            }
                        }
                       );
                 }
                //  orderId=orderData.OrderID;
             }
         }
         err=>console.error(err)
        );
     
        
    }

}