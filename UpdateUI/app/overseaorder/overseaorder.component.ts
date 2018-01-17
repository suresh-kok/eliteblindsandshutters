import {Component,ElementRef,ViewChild,Input,EventEmitter} from '@angular/core';
import {AuthenticationService, User} from '../../app/services/auth.service';
import {OrderData}from '../DataModels/orderdata';
import {OrderInitiation}from '../DataModels/orderInitiationData';
import {OrderTypeComponent} from '../../app/order/orderType.component';
import {OrderListComponent} from '../../app/order/orderlist.component';
import {OrderMiscService} from '../../app/services/order.service';

@Component({
    selector:"oversea-order",
    templateUrl:'app/overseaorder/overseaorder.html'
})
export class OverSeaComponent{
    @ViewChild('extra') extra: ElementRef;
    @ViewChild('details') details: ElementRef;
    @ViewChild('home') home: ElementRef;
    @ViewChild('basic') basic:ElementRef;
    OrderInitiation:OrderInitiation;
    constructor(private service:AuthenticationService){

    }
    ngOnInit(){
        this.service.checkCredentials();
    }
    ngAfterViewInit(){
        let el: HTMLElement = this.basic.nativeElement as HTMLElement;
        el.click();
    }
    onNotify(message:any):void {
        debugger;
        this.OrderInitiation=message;
        let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        elHome.click();
      }
}