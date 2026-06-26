// src/app/features/appointments/appointment-list/appointment-list.component.ts
// Demonstrates: Component lifecycle, service calls, conditional rendering

import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppointmentService } from '../../../core/services/appointment.service';
import { Appointment } from '../../../core/models/domain.models';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.scss'],
  standalone: false
})
export class AppointmentListComponent implements OnInit {
  appointments: Appointment[] = [];
  isLoading = false;

  displayedColumns = ['date', 'doctor', 'slot', 'status', 'notes', 'actions'];

  constructor(
    private appointmentService: AppointmentService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments(): void {
    this.isLoading = true;
    this.appointmentService.getMyAppointments().subscribe({
      next: (data) => {
        this.appointments = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  cancelAppointment(id: number): void {
    if (!confirm('Cancel this appointment?')) return;

    this.appointmentService.cancel(id).subscribe({
      next: () => {
        this.snackBar.open('Appointment cancelled.', 'OK', { duration: 3000 });
        this.loadAppointments(); // Refresh list
      }
    });
  }

  getStatusColor(status: string): string {
    const map: Record<string, string> = {
      Pending: 'accent',
      Confirmed: 'primary',
      Completed: '',
      Cancelled: 'warn'
    };
    return map[status] || '';
  }

  canCancel(status: string): boolean {
    return status === 'Pending' || status === 'Confirmed';
  }
}
