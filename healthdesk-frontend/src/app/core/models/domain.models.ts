// src/app/core/models/domain.models.ts

export interface Doctor {
  id: number;
  userId: number;
  specialization: string;
  bio?: string;
  name?: string; // joined from User
  email?: string;
}

export interface Patient {
  id: number;
  userId: number;
  dob?: string;
  bloodGroup?: string;
  name?: string;
}

export interface TimeSlot {
  id: number;
  doctorId: number;
  dayOfWeek: number; // 1–7
  startTime: string;
  endTime: string;
}

export interface Appointment {
  id: number;
  patientId: number;
  doctorId: number;
  slotId: number;
  date: string;
  status: 'Pending' | 'Confirmed' | 'Cancelled' | 'Completed';
  notes?: string;
  doctorName?: string;
  patientName?: string;
  slotTime?: string;
}

export interface MedicalRecord {
  id: number;
  appointmentId: number;
  diagnosis?: string;
  prescription?: string;
}

export interface AppointmentFormData {
  doctorId: number;
  slotId: number;
  date: string;
  notes?: string;
}
