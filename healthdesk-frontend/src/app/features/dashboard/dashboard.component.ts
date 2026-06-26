// src/app/features/dashboard/dashboard.component.ts
// Demonstrates: OnInit, Observable with AsyncPipe, multiple service injection

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';
import { AppointmentService } from '../../core/services/appointment.service';
import { CurrentUser } from '../../core/models/auth.models';
import { Appointment } from '../../core/models/domain.models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: false
})
export class DashboardComponent implements OnInit {
  // Observable used with AsyncPipe in template (auto-subscribe/unsubscribe)
  currentUser$: Observable<CurrentUser | null>;
  appointments: Appointment[] = [];
  isLoading = false;

  // Stats for the dashboard cards
  stats = { pending: 0, confirmed: 0, completed: 0, cancelled: 0 };

  constructor(
    public authService: AuthService,
    private appointmentService: AppointmentService
  ) {
    this.currentUser$ = this.authService.currentUser$;
  }

  ngOnInit(): void {
    this.loadAppointments();
  }

  private loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getMyAppointments().subscribe({
      next: (data) => {
        this.appointments = data;
        this.calculateStats(data);
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  private calculateStats(appointments: Appointment[]): void {
    this.stats = {
      pending: appointments.filter(a => a.status === 'Pending').length,
      confirmed: appointments.filter(a => a.status === 'Confirmed').length,
      completed: appointments.filter(a => a.status === 'Completed').length,
      cancelled: appointments.filter(a => a.status === 'Cancelled').length
    };
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      Pending: 'accent',
      Confirmed: 'primary',
      Completed: '',
      Cancelled: 'warn'
    };
    return colors[status] || '';
  }
}
