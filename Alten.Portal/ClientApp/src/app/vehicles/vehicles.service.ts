import { Injectable } from '@angular/core';  
import { HttpClient, HttpParams } from '@angular/common/http';  
import { Observable } from 'rxjs';  
import { ICustomer } from '../shared-models/Customer.models';
import { IVechicleModel } from './model/vechicle.model';
import { RequestOptions } from '@angular/http';
import { FilterVehicle } from './model/filtervehicle.model';
import { environment } from '../../environments/environment';


@Injectable({  
  providedIn: 'root'  
})


export class VehiclesService {
    cutomerUrl = environment.cutomerApiEndpoint;
    vehiclesUrl = environment.vehicleApiEndpoint;

  constructor(private http: HttpClient) { }  
  GetAllCustomer(): Observable<ICustomer[]> {  
    return this.http.get<ICustomer[]>(this.cutomerUrl + 'api/Customers/GetCustomers')  
  }  

  
    GetVehicles(filterVehicle: FilterVehicle): Observable<IVechicleModel[]> {
        return this.http.post<IVechicleModel[]>(this.vehiclesUrl + 'api/Vehicles/GetVehicles', filterVehicle);  
  }  

}  
