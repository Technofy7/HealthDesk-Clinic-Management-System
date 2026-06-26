// src/app/core/guards/role.guard.ts
// Demonstrates: Role-based CanActivate guard using route data

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    // Expected role is passed via route data: { data: { role: 'Doctor' } }
    const expectedRole: string = route.data['role'];

    if (!this.authService.isLoggedIn) {
      this.router.navigate(['/auth/login']);
      return false;
    }

    if (this.authService.isInRole(expectedRole)) {
      return true;
    }

    // User is logged in but doesn't have the required role
    this.router.navigate(['/dashboard']);
    return false;
  }
}
