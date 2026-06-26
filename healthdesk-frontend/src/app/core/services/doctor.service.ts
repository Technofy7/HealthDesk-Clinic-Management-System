// src/app/core/services/doctor.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Doctor, TimeSlot } from '../models/domain.models';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private apiUrl = `${environment.apiUrl}/doctors`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.apiUrl);
  }

  getById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${this.apiUrl}/${id}`);
  }

  getSlots(doctorId: number): Observable<TimeSlot[]> {
    return this.http.get<TimeSlot[]>(`${this.apiUrl}/${doctorId}/slots`);
  }
}
