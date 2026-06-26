// src/app/core/services/auth.service.ts
// Demonstrates: BehaviorSubject state, localStorage persistence,
// Observable pattern, service injection

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  LoginRequest,
  RegisterRequest,
  LoginResponse,
  CurrentUser
} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'hd_token';
  private readonly USER_KEY = 'hd_user';

  // BehaviorSubject holds the current user; null = not logged in
  // Components subscribe to currentUser$ to react to auth changes
  private currentUserSubject = new BehaviorSubject<CurrentUser | null>(
    this.loadUserFromStorage()
  );

  // Exposed as Observable (read-only to consumers)
  currentUser$: Observable<CurrentUser | null> = this.currentUserSubject.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  // ── Registration ──────────────────────────────────────────────
  register(request: RegisterRequest): Observable<any> {
    return this.http.post(`${environment.apiUrl}/auth/register`, request);
  }

  // ── Login ─────────────────────────────────────────────────────
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, request).pipe(
      tap(response => this.handleLoginSuccess(response))
    );
  }

  // ── Logout ────────────────────────────────────────────────────
  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
    this.currentUserSubject.next(null);
    this.router.navigate(['/auth/login']);
  }

  // ── Helpers ───────────────────────────────────────────────────
  get currentUser(): CurrentUser | null {
    return this.currentUserSubject.value;
  }

  get token(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  get isLoggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  isInRole(role: string): boolean {
    return this.currentUser?.roles?.split(',').map(r => r.trim()).includes(role) ?? false;
  }

  private handleLoginSuccess(response: LoginResponse): void {
    const user: CurrentUser = {
      userId: response.userId,
      name: response.name,
      roles: response.roles,
      token: response.token
    };
    localStorage.setItem(this.TOKEN_KEY, response.token);
    localStorage.setItem(this.USER_KEY, JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  private loadUserFromStorage(): CurrentUser | null {
    const raw = localStorage.getItem(this.USER_KEY);
    return raw ? JSON.parse(raw) : null;
  }
}
