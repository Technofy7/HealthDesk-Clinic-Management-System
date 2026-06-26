// src/app/core/interceptors/error.interceptor.ts
// Demonstrates: HTTP Interceptor for centralized error handling
// Catches 401 (auto-logout), 403 (forbidden), and generic errors

import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private snackBar: MatSnackBar,
    private authService: AuthService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let message = 'An unexpected error occurred.';

        switch (error.status) {
          case 401:
            // Token expired or invalid → force logout
            this.authService.logout();
            message = 'Session expired. Please log in again.';
            break;
          case 403:
            message = 'You do not have permission to perform this action.';
            this.router.navigate(['/dashboard']);
            break;
          case 404:
            message = 'The requested resource was not found.';
            break;
          case 400:
            // Grab server validation message if available
            message = error.error?.message || 'Invalid request data.';
            break;
          case 500:
            message = 'Server error. Please try again later.';
            break;
        }

        this.snackBar.open(message, 'Close', {
          duration: 4000,
          panelClass: ['error-snackbar']
        });

        return throwError(() => error);
      })
    );
  }
}
