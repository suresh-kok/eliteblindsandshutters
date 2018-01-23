import{Component} from '@angular/core';

@Component({
    selector:'not-authorised',
    template:`
    <div class="alert alert-danger">
    <strong>Please login to view this page</strong> 
  </div>
    `
})
export class NotAuthorisedComponent{}