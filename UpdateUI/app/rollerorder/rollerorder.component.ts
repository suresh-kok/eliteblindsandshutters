import {Component,ElementRef,ViewChild} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderTypeComponent} from '../../app/order/orderType.component';
import {RollerOrderListComponent}from './rollerorderlist.component';
import {OrderMiscService} from '../../app/services/order.service';
@Component({
    selector:'roller-order',
    templateUrl:'app/rollerorder/rollerorder.html'
})
export class RollerOrderComponent{
    @ViewChild('extra') extra: ElementRef;
    @ViewChild('details') details: ElementRef;
    @ViewChild('home') home: ElementRef;
    @ViewChild('basic') basic:ElementRef;
    OrderInitiation:OrderInitiation;
    OrderDetail:OrderData;
    OrderDetails:OrderData[]=[];
    public materialList;
    public colors;
    public controls;
    OrderConfirmed:boolean=false;
    constructor(private service:AuthenticationService,private orderService:OrderMiscService){
     

    }
    ngOnInit(){
        this.service.checkCredentials();
        this.OrderDetail=new OrderData();
        this.OrderInitiation= new OrderInitiation();
        this.OrderConfirmed=false;
        this.OrderDetails=[];
        this.GetColor();
        this.GetMaterial();
        this.GetControls();
    }
    ngAfterViewInit(){
        let el: HTMLElement = this.basic.nativeElement as HTMLElement;
        el.click();
    }
    onNotify(message:any):void {
        this.OrderInitiation=message;
        let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        elHome.click();
    }
    GetMaterial(){
    
        this.orderService.GetMaterial('Roller').subscribe(
            data=>{
                if(data)
                this.materialList=JSON.parse(data.toString());
            },
            err=>console.error(err)
        )
    }
    GetColor(){
      this.orderService.GetColors('Roller').subscribe(
        data=>{
            if(data)
            this.colors=JSON.parse(data.toString());
        },
        err=>console.error(err)
      )
    }
    GetControls(){
       
            this.controls=[{ControlID:"11",ControlDesc:"Left"},{ControlID:"12",ControlDesc:"RIGHT"}];
            this.OrderDetail.ControlID=11;
    
    }
    onControlChange(){
        debugger;
        let text=this.controls.filter(control=>control.ControlID==this.OrderDetail.ControlID);
        this.OrderDetail.ControlName=text[0].ControlDesc;
      }
    onColorChange()
    {
        debugger;
      let text=this.colors.filter(color=>color.ColorsID==this.OrderDetail.ColorID);
      this.OrderDetail.ColorName=text[0].ColorsDesc;
    }
    OnMaterialChange(){
        debugger;
      let text=this.materialList.filter(material=>material.MaterialID==this.OrderDetail.MaterialID);
      this.OrderDetail.MaterialName=text[0].MaterialDesc;
    }
    AddItem()
    {
      debugger;
      this.OrderDetails.push(this.OrderDetail);
      this.OrderDetail=new OrderData();
    }
    Proceed()
    {
      let elDetails: HTMLElement = this.details.nativeElement as HTMLElement;
      elDetails.click();

    }
    OrderConfirm()
    {
      debugger;
      let el: HTMLElement = this.extra.nativeElement as HTMLElement;
      el.click();
    }
    Delete(item)
    {
       let index=this.OrderDetails.indexOf(item);
       if(index!==-1){
           this.OrderDetails.splice(index,1);
       }
    }
    SaveOrder()
    {
      let orderId:any=0;
      debugger;
      this.OrderInitiation.CustomerID=this.service.GetCustomerId();
      this.OrderInitiation.OrderTypeID=2;
      this.OrderInitiation.OrderDate=new Date();
      this.OrderInitiation.OrderStatusID=1;
      this.OrderInitiation.OrderStatus="IN_Processing";
    this.orderService.SaveOrderInitiation(this.OrderInitiation).subscribe(
     data=>{
         debugger;
         if(data){
            // let orderData:any;
            let orderData:any=JSON.parse(data.toString());
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
         }
     },
     err=>console.error(err)
    );
    }
    NewOrder(){
        this.OrderDetail=new OrderData();
        this.OrderInitiation= new OrderInitiation();
        this.OrderConfirmed=false;
        this.OrderDetails=[];
    }
}