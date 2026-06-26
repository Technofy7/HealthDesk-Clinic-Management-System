// src/app/features/doctors/doctors.module.ts

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { DoctorListComponent } from './doctor-list/doctor-list.component';

const routes: Routes = [
  { path: '', component: DoctorListComponent }
];

@NgModule({
  declarations: [DoctorListComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class DoctorsModule {}
