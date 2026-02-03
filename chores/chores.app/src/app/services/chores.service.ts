import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Chore } from '../models/chore.model';
import { AuthService } from '../auth/auth.service';

@Injectable({ providedIn: 'root' })
export class ChoresService {
  private http = inject(HttpClient);
  private auth = inject(AuthService);
  private base = '/api/chores';

  private withAuth<T>(obs: Promise<string | null>, fn: (headers: HttpHeaders) => Observable<T>) {
    return from(obs).pipe(
      switchMap(token => {
        const headers = token ? new HttpHeaders({ Authorization: `Bearer ${token}` }) : new HttpHeaders();
        return fn(headers);
      })
    );
  }

  getAllChores(): Observable<Chore[]> {
    return this.withAuth(this.auth.getToken(), headers => this.http.get<Chore[]>(`${this.base}/getAllChores`, { headers }));
  }

  addChore(chore: Chore): Observable<Chore> {
    return this.withAuth(this.auth.getToken(), headers => this.http.post<Chore>(`${this.base}/addChore`, chore, { headers }));
  }
}
