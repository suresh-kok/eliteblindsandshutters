import {Component,EventEmitter,ElementRef,ViewChild,Input,Output} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderInitiation}from '../DataModels/orderInitiationData';
@Component({
    selector:'order-type',
    templateUrl:'app/order/orderType.html'
})
export class OrderTypeComponent{
    @Output() notify = new EventEmitter<OrderInitiation>();
    @Input('order') OrderTypeVal:string;
    newOrder:OrderInitiation;
    IsRemake:boolean=false;
    constructor(){
     this.newOrder=new OrderInitiation();
    }
    ngOnInit(){
       this.newOrder.OrderType=this.OrderTypeVal;
    }
    orderChange(){
        debugger;
        if(this.newOrder.IsNew==true)
        this.IsRemake=false;
        else
        this.IsRemake=true;
    }
    ProceedData()
    {
        debugger;
        this.notify.emit(this.newOrder);
    }

}