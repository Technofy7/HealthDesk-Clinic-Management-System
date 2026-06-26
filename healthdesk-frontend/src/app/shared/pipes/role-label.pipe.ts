// src/app/shared/pipes/role-label.pipe.ts
// Demonstrates: Custom Pipe with PipeTransform

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'roleLabel',
  standalone: false
})
export class RoleLabelPipe implements PipeTransform {
  transform(roles: string): string {
    if (!roles) return 'User';

    return roles
      .split(',')
      .map(r => r.trim())
      .map(r => {
        switch (r) {
          case 'Doctor': return '🩺 Doctor';
          case 'Patient': return '🏥 Patient';
          case 'Admin': return '⚙️ Admin';
          default: return r;
        }
      })
      .join(', ');
  }
}
