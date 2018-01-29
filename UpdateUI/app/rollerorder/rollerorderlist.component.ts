import {Component,EventEmitter,ElementRef,ViewChild,Input,Output} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
@Component({
    selector:'rollerorder-list',
    templateUrl:'app/rollerorder/rollerorderlist.html'
})
export class RollerOrderListComponent{
    @Input('orderInitiate') OrderInfo:OrderInitiation;
    @Input('orderDetails') OrderDetails:OrderData[];
    @Input('orderPlaced') OrderPlaced:boolean=false;
    constructor(){
 
    }
    
}