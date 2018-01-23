import {Injectable} from '@angular/core';
import{HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
@Injectable()
export class MiscellaneousService {
     headers = new HttpHeaders();
   api_key='17bca5a48c36fd2fc5ee89653708b849';
    constructor( private http:HttpClient){
       this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
       this.headers.append('Access-Control-Allow-Methods', 'GET');
       this.headers.append('Access-Control-Allow-Origin', '*');
    }
    
    GetAllCountries(){
        
       return this.http.get('http://battuta.medunes.net/api/country/all/?key='+this.api_key,{headers:this.headers});
    }
    GetRegions(country){
      
        let regionUrl='http://battuta.medunes.net/api/region/'+country+'/all/?key='+this.api_key;
        return this.http.get(regionUrl,{headers:this.headers});
    }
    GetCities(country,region){
        let url="http://battuta.medunes.net/api/city/"+country+"/search/?region="+region+"&key="+this.api_key;
        return this.http.get(url,{headers:this.headers});
    }
}