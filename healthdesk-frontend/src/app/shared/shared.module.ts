// src/app/shared/shared.module.ts
// Demonstrates: Shared Module pattern — declare once, import anywhere

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

// Angular Material Modules
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatBadgeModule } from '@angular/material/badge';
import { MatDividerModule } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';

// Shared Components & Pipes
import { NavbarComponent } from './components/navbar/navbar.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { RoleLabelPipe } from './pipes/role-label.pipe';

const MATERIAL_MODULES = [
  MatToolbarModule,
  MatButtonModule,
  MatIconModule,
  MatInputModule,
  MatFormFieldModule,
  MatCardModule,
  MatTableModule,
  MatSelectModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatSnackBarModule,
  MatProgressSpinnerModule,
  MatChipsModule,
  MatTooltipModule,
  MatBadgeModule,
  MatDividerModule,
  MatMenuModule
];

@NgModule({
  declarations: [
    NavbarComponent,
    SpinnerComponent,
    RoleLabelPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    ...MATERIAL_MODULES
  ],
  exports: [
    // Re-export everything feature modules need
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    NavbarComponent,
    SpinnerComponent,
    RoleLabelPipe,
    ...MATERIAL_MODULES
  ]
})
export class SharedModule {}
