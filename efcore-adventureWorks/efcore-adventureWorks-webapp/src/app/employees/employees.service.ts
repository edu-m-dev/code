import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from './Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  private apiUrl = 'http://localhost:5228';

  constructor(private http: HttpClient) { }

  allEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}/allEmployees`);
  }
}
