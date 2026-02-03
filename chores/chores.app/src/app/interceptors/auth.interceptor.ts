import { Injectable } from '@angular/core';
import {
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, from, throwError } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Only attach token for API calls (proxy uses /api)
    if (!req.url.includes('/api')) {
      return next.handle(req);
    }

    // If this request already indicates it was retried, avoid retry loop
    const alreadyRetried = req.headers.get('x-auth-retried') === '1';

    return from(this.auth.getToken()).pipe(
      switchMap(token => {
        const authReq = token
          ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
          : req;

        return next.handle(authReq).pipe(
          catchError((err: any) => {
            if (
              err instanceof HttpErrorResponse &&
              err.status === 401 &&
              !alreadyRetried
            ) {
              // Attempt to get a fresh token and retry once
              return from(this.auth.getToken()).pipe(
                switchMap(newToken => {
                  const retryReq = newToken
                    ? req.clone({ setHeaders: { Authorization: `Bearer ${newToken}`, 'x-auth-retried': '1' } })
                    : req.clone({ setHeaders: { 'x-auth-retried': '1' } });
                  return next.handle(retryReq);
                }),
                catchError(e2 => throwError(() => e2))
              );
            }
            return throwError(() => err);
          })
        );
      })
    );
  }
}
