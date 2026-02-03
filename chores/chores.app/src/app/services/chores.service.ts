import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Chore } from '../models/chore.model';

@Injectable({ providedIn: 'root' })
export class ChoresService {
  private http = inject(HttpClient);
  private base = '/api/chores';

  getAllChores(): Observable<Chore[]> {
    return this.http.get<Chore[]>(`${this.base}/getAllChores`);
  }

  addChore(chore: Chore): Observable<Chore> {
    return this.http.post<Chore>(`${this.base}/addChore`, chore);
  }
}
