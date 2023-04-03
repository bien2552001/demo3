import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TableRestApiModel } from './model/tablerestapimodel';

@Injectable({
  providedIn: 'root'
})
export class RestapiservicesService {

  constructor(private http: HttpClient) { }


  //____________________________Get____________________________
  getcurrent(): Observable<Array<TableRestApiModel>> {
    return this.http.get<Array<TableRestApiModel>>('https://localhost:5001/data/current');
  }

  //____________________________Get____________________________
  getId(id: string): Observable<Array<TableRestApiModel>> {
    return this.http.get<Array<TableRestApiModel>>('https://localhost:5001/data/current' + id);
  }

  //____________________________Post____________________________
  postcurrent(data: any): Observable<Array<TableRestApiModel>> {
    return this.http.post<Array<TableRestApiModel>>('https://localhost:5001/data/current', data);
  }

  //____________________________Delete____________________________
 
  deleteCurrent(id: string): Observable<Array<TableRestApiModel>> {
    const url = `https://localhost:5001/data/current/${id}`;
    return this.http.delete<Array<TableRestApiModel>>(url);
  }
  //____________________________Put____________________________
  putcurrent(id: string, data: any): Observable<Array<TableRestApiModel>> {
    return this.http.put<Array<TableRestApiModel>>('https://localhost:5001/data/current/' + id, data);
  }


  //------------------------------------------------------------------------------------Chart----------------------------------------



}
