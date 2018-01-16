import {Component,EventEmitter,ElementRef,ViewChild,Input,Output} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
@Component({
    selector:'order-list',
    templateUrl:'app/order/orderlist.html'
})
export class OrderListComponent{
    @Input('orderInitiate') OrderInfo:OrderInitiation;
    @Input('orderDetails') OrderDetails:OrderData[];

    constructor(){
  this.testData();
    }
    testData(){
debugger;

    }
}