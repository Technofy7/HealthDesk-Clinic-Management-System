// src/app/app-routing.module.ts
// Demonstrates: Lazy loading with loadChildren, route guards, redirects

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  // Default redirect
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  // Lazy-loaded Auth module (no guard — public)
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then(m => m.AuthModule)
  },

  // Lazy-loaded Dashboard — protected by AuthGuard
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./features/dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard]
  },

  // Lazy-loaded Appointments — protected by AuthGuard
  {
    path: 'appointments',
    loadChildren: () =>
      import('./features/appointments/appointments.module').then(m => m.AppointmentsModule),
    canActivate: [AuthGuard]
  },

  // Lazy-loaded Doctors — protected by AuthGuard
  {
    path: 'doctors',
    loadChildren: () =>
      import('./features/doctors/doctors.module').then(m => m.DoctorsModule),
    canActivate: [AuthGuard]
  },

  // Wildcard fallback
  { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    scrollPositionRestoration: 'top' // Scroll to top on navigation
  })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
