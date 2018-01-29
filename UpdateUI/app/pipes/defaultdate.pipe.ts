import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'DefaultDate'})
export class DefaultDatePipe implements PipeTransform {
  transform(value: any,params:string): string {
    
    if(value=="0001-01-01T00:00:00")
    {
        return "";
    }
    else{
        return value;
    }
  }
}