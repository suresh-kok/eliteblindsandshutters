import {Component,EventEmitter,ElementRef,ViewChild,Input,Output} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
@Component({
    selector:'overseaorder-list',
    templateUrl:'app/overseaorder/overseaorderlist.html'
})
export class OverSeaOrderListComponent{
    @Input('orderInitiate') OrderInfo:OrderInitiation;
    @Input('orderDetails') OrderDetails:OrderData[];
    @Input('orderPlaced') OrderPlaced:boolean=false;
    constructor(){
 
    }
    
}