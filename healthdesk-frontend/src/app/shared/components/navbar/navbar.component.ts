// src/app/shared/components/navbar/navbar.component.ts
// Demonstrates: Component consuming Observable via AsyncPipe, OnDestroy cleanup

import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';
import { CurrentUser } from '../../../core/models/auth.models';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  standalone: false
})
export class NavbarComponent implements OnInit, OnDestroy {
  currentUser: CurrentUser | null = null;

  // Subject used to complete subscriptions on destroy (memory leak prevention)
  private destroy$ = new Subject<void>();

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Subscribe to auth state changes
    this.authService.currentUser$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => (this.currentUser = user));
  }

  logout(): void {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    // Emit to complete all takeUntil subscriptions → prevents memory leaks
    this.destroy$.next();
    this.destroy$.complete();
  }
}
