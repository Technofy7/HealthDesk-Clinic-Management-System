// src/app/features/doctors/doctor-list/doctor-list.component.ts
// Demonstrates: OnInit, service injection, template filtering

import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../../core/services/doctor.service';
import { Doctor } from '../../../core/models/domain.models';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss'],
  standalone: false
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];
  filteredDoctors: Doctor[] = [];
  isLoading = false;
  searchTerm = '';

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.doctorService.getAll().subscribe({
      next: (data) => {
        this.doctors = data;
        this.filteredDoctors = data;
        this.isLoading = false;
      },
      error: () => (this.isLoading = false)
    });
  }

  onSearch(term: string): void {
    this.searchTerm = term;
    this.filteredDoctors = this.doctors.filter(d =>
      d.name?.toLowerCase().includes(term.toLowerCase()) ||
      d.specialization.toLowerCase().includes(term.toLowerCase())
    );
  }
}
