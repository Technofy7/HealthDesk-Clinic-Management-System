// src/app/app.component.ts
// Root component — shell that hosts navbar + router outlet

import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: false
})
export class AppComponent {
  title = 'HealthDesk';
}
