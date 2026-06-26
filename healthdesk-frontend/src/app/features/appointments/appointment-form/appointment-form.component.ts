// src/app/features/appointments/appointment-form/appointment-form.component.ts
// Demonstrates: Reactive Forms, valueChanges Observable, cascading dropdowns

import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject, takeUntil } from 'rxjs';
import { DoctorService } from '../../../core/services/doctor.service';
import { AppointmentService } from '../../../core/services/appointment.service';
import { Doctor, TimeSlot } from '../../../core/models/domain.models';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.scss'],
  standalone: false
})
export class AppointmentFormComponent implements OnInit, OnDestroy {
  appointmentForm!: FormGroup;
  doctors: Doctor[] = [];
  availableSlots: TimeSlot[] = [];
  isLoadingDoctors = false;
  isLoadingSlots = false;
  isSubmitting = false;
  minDate = new Date(); // No past-date appointments

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private doctorService: DoctorService,
    private appointmentService: AppointmentService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadDoctors();
    this.listenToDoctorChange();
  }

  private buildForm(): void {
    this.appointmentForm = this.fb.group({
      doctorId: [null, Validators.required],
      slotId: [null, Validators.required],
      date: [null, Validators.required],
      notes: ['']
    });
  }

  private loadDoctors(): void {
    this.isLoadingDoctors = true;
    this.doctorService.getAll().subscribe({
      next: (data) => {
        this.doctors = data;
        this.isLoadingDoctors = false;
      },
      error: () => (this.isLoadingDoctors = false)
    });
  }

  // Cascading dropdown: when doctor changes, load their slots
  // Demonstrates: valueChanges + takeUntil for reactive subscription management
  private listenToDoctorChange(): void {
    this.appointmentForm.get('doctorId')!.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe((doctorId: number) => {
        // Reset slot selection when doctor changes
        this.appointmentForm.get('slotId')!.reset();
        this.availableSlots = [];

        if (doctorId) {
          this.isLoadingSlots = true;
          this.doctorService.getSlots(doctorId).subscribe({
            next: (slots) => {
              this.availableSlots = slots;
              this.isLoadingSlots = false;
            },
            error: () => (this.isLoadingSlots = false)
          });
        }
      });
  }

  getDayName(dayOfWeek: number): string {
    const days = ['', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    return days[dayOfWeek] || '';
  }

  get doctorControl() { return this.appointmentForm.get('doctorId'); }
  get slotControl() { return this.appointmentForm.get('slotId'); }
  get dateControl() { return this.appointmentForm.get('date'); }

  onSubmit(): void {
    if (this.appointmentForm.invalid) {
      this.appointmentForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const formValue = this.appointmentForm.value;

    // Format date to DateOnly string (yyyy-MM-dd)
    const date = new Date(formValue.date);
    const formatted = date.toISOString().split('T')[0];

    this.appointmentService.create({
      ...formValue,
      date: formatted
    }).subscribe({
      next: () => {
        this.snackBar.open('Appointment booked successfully!', 'OK', { duration: 3000 });
        this.router.navigate(['/appointments']);
      },
      error: () => (this.isSubmitting = false)
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
