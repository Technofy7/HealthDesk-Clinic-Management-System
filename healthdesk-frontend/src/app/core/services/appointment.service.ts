// src/app/core/services/appointment.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Appointment, AppointmentFormData } from '../models/domain.models';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private apiUrl = `${environment.apiUrl}/appointments`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiUrl);
  }

  getById(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.apiUrl}/${id}`);
  }

  getMyAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/my`);
  }

  create(data: AppointmentFormData): Observable<Appointment> {
    return this.http.post<Appointment>(this.apiUrl, data);
  }

  updateStatus(id: number, status: string): Observable<Appointment> {
    return this.http.patch<Appointment>(`${this.apiUrl}/${id}/status`, { status });
  }

  cancel(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
