import {Component,ElementRef,ViewChild,Input,EventEmitter} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderTypeComponent} from '../../app/order/orderType.component';
import {OrderListComponent} from '../../app/order/orderlist.component';
import {OrderMiscService} from '../../app/services/order.service';

@Component({
    selector:'oversea-order',
    templateUrl:'app/overseaorder/overseaorder.html'
})

export class OverSeaComponent{
    @ViewChild('extra') extra: ElementRef;
    @ViewChild('details') details: ElementRef;
    @ViewChild('home') home: ElementRef;
    @ViewChild('basic') basic:ElementRef;
    OrderInitiation:OrderInitiation;
    OrderDetail:OrderData;
    OrderDetails:OrderData[]=[];
    public materialList;
    public cordStyles;
    public colors;
    public controls;
    OrderConfirmed:boolean=false;
    constructor(private service:AuthenticationService,element: ElementRef
        ,private orderService:OrderMiscService){
            this.OrderDetail=new OrderData();
            this.OrderInitiation= new OrderInitiation();
    }
    ngOnInit(){
        this.service.checkCredentials();
       
    }
    ngAfterViewInit(){
        let el: HTMLElement = this.basic.nativeElement as HTMLElement;
        el.click();
    }
    onNotify(message:any):void {
        this.OrderInitiation=message;
        if(this.OrderInitiation.BlindTypeID)
        {
        this.GetMaterial();
        this.GetColors();
        this.GetControls();
        }
        if(this.OrderInitiation.BlindTypeID==2)
        this.GetCordStyles();
        let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        elHome.click();
      }
      GetControls(){
          if(this.OrderInitiation.BlindTypeID==4){
              this.controls=[{ControlID:"5",ControlDesc:"MANUAL"},{ControlID:"6",ControlDesc:"MANUAL WITH HEADER"},{ControlID:"7",ControlDesc:"MANUAL WITH FASCIA BOX"},{ControlID:"8",ControlDesc:"MOTOR"}];
              this.OrderDetail.ControlID=5;
          }
          else{
            this.controls=[{ControlID:"1",ControlDesc:"MANUAL"},{ControlID:"2",ControlDesc:"MOTOR"}];
            this.OrderDetail.ControlID=1;
          }
      }
      onControlChange(){
        debugger;
        let text=this.controls.filter(control=>control.ControlID==this.OrderDetail.ControlID);
        this.OrderDetail.ControlText=text[0].ControlDesc;
      }
      GetCordStyles(){
        this.orderService.GetCordStyle('OVenetian').subscribe(
            data => {
                debugger;
                this.cordStyles = JSON.parse(data.toString());
            },
            err => console.error(err)
          );
    }
    GetColors(){
        let blindType;
        switch(this.OrderInitiation.BlindTypeID)
        {
            case "1":
              blindType='OVertical';
              break;
            case "2":
              blindType='OVenetian';
              break;
            case "3":
              blindType='OHoneycomb';
              break;
            case "4":
              blindType='ORoman';
              break;
            case "5":
              blindType='ORoller';
              break;
            case "6":
              blindType='OZebra';
              break;
        }
        this.orderService.GetColors(blindType).subscribe(
            data=>{
                if(data)
                this.colors=JSON.parse(data.toString());
            }
        )
    }
    GetMaterial(){
        let blindType;
        switch(this.OrderInitiation.BlindTypeID)
        {
            case "1":
              blindType='OVertical';
              break;
            case "2":
              blindType='OVenetian';
              break;
            case "3":
              blindType='OHoneycomb';
              break;
            case "4":
              blindType='ORoman';
              break;
            case "5":
              blindType='ORoller';
              break;
            case "6":
              blindType='OZebra';
              break;
        }
        if(blindType!=''){
            debugger;
        this.orderService.GetMaterial(blindType).subscribe(
            data=>{
                if(data)
               this.materialList=JSON.parse(data.toString());
            
            },
            err=>console.error(err)
        )
    }
    }
      onCordChange()
      {
          debugger;
        let cordText=this.cordStyles.filter(cord=>cord.CordStyleID==this.OrderDetail.CordStyleID);
        this.OrderDetail.cordStyleText=cordText[0].CordStyleDesc;
      }
      onColorChange()
      {
          debugger;
        let text=this.colors.filter(color=>color.ColorsID==this.OrderDetail.ColorID);
        this.OrderDetail.ColorText=text[0].ColorsDesc;
      }
      OnMaterialChange(){
          debugger;
        let text=this.materialList.filter(material=>material.MaterialID==this.OrderDetail.MaterialID);
        this.OrderDetail.MaterialText=text[0].MaterialDesc;
      }
      onReturnChange(){
        if(this.OrderDetail.ReturnRequired==true)
        this.OrderDetail.ReturnRequiredText="Yes";
        else
        this.OrderDetail.ReturnRequiredText="No";
      }
      onMountChange()
      {
          if(this.OrderDetail.MountType==true)
          this.OrderDetail.MountTypeText="In";
          else
          this.OrderDetail.MountTypeText="Out";
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
        // elDetails.
        //this.enableCheckOut=true;
      }
      SaveOrder()
      {
        let orderId:any=0;
        debugger;
        this.OrderInitiation.CustomerID=this.service.GetCustomerId();
        this.OrderInitiation.OrderTypeID=1;
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
      Delete(item)
      {
         let index=this.OrderDetails.indexOf(item);
         if(index!==-1){
             this.OrderDetails.splice(index,1);
         }
      }
}