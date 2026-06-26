// src/app/shared/components/spinner/spinner.component.ts
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  template: `
    <div class="spinner-wrapper" *ngIf="show">
      <mat-spinner [diameter]="diameter"></mat-spinner>
    </div>
  `,
  styles: [`
    .spinner-wrapper {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 32px;
    }
  `],
  standalone: false
})
export class SpinnerComponent {
  @Input() show = false;
  @Input() diameter = 48;
}
