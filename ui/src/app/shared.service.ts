import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly ApiUrl = "http://localhost:5000/api";
  constructor(private http:HttpClient) { }
  
  getEmployees() : Observable<any[]> {
	return this.http.get<any>(this.ApiUrl+'/Employees');
  }
  
  addEmployee(val: any) {
	return this.http.post(this.ApiUrl+'/Employees', val);
  }
  
  updateEmployee(id: any, val: any) {
	return this.http.put(this.ApiUrl+'/Employees/'+id, val);
  }
  
  deleteEmployee(id: any) {
	return this.http.delete(this.ApiUrl+'/Employees/'+id);
  }
  
  getPets() : Observable<any[]> {
	return this.http.get<any>(this.ApiUrl+'/Pets');
  }
  
  addPet(val: any) {
	return this.http.post(this.ApiUrl+'/Pets', val);
  }
  
  updatePet(id: any, val: any) {
	return this.http.put(this.ApiUrl+'/Pets/'+id, val);
  }
  
  deletePet(id: any) {
	return this.http.delete(this.ApiUrl+'/Pets/'+id);
  }
}
