import {Component,EventEmitter,ElementRef,ViewChild,Input,Output} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderMiscService} from '../../app/services/order.service'
@Component({
    selector:'order-type',
    templateUrl:'app/order/orderType.html'
})
export class OrderTypeComponent{
    @Output() notify = new EventEmitter<OrderInitiation>();
    @Input('order') OrderTypeVal:string;
    newOrder:OrderInitiation;
    IsRemake:boolean=false;
    blindTypes:any;
  
    constructor(private orderService:OrderMiscService){
     this.newOrder=new OrderInitiation();
    }
    ngOnInit(){
       this.newOrder.OrderTypeID=this.OrderTypeVal;
       if(this.OrderTypeVal=="3")
       this.GetBlindType();
       
    }
    orderChange(){
        debugger;
        if(this.newOrder.IsNew=="true")
        this.IsRemake=false;
        else if(this.newOrder.IsNew=="false")
        this.IsRemake=true;
    }
    ProceedData()
    {
        debugger;
        if(this.newOrder.OrderTypeID==3)
        {
            if(this.newOrder.BlindTypeID==''){
                alert('Please select BlindType')
                return;
            }
        }
        this.notify.emit(this.newOrder);
    }
    GetBlindType(){
        this.orderService.GetBlindType().subscribe(
            data=>{this.blindTypes=JSON.parse(data.toString()) },
            err=>console.error(err)
        )
    }
    onBlindTypeChange()
    {
        debugger;
      let blindText=this.blindTypes.filter(blind=>blind.BlindTypeID==this.newOrder.BlindTypeID);
      this.newOrder.BlindTypeText=blindText[0].BlindTypeDesc;
    }

}