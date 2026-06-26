// src/app/features/auth/login/login.component.ts
// Demonstrates: Reactive Forms, FormBuilder, Validators, ActivatedRoute query params

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: false
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoading = false;
  hidePassword = true;
  errorMessage = '';
  private returnUrl = '/dashboard';

  constructor(
    private fb: FormBuilder,            // FormBuilder for ergonomic form creation
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute       // ActivatedRoute to read query params
  ) {}

  ngOnInit(): void {
    // If already logged in, skip login page
    if (this.authService.isLoggedIn) {
      this.router.navigate(['/dashboard']);
      return;
    }

    // Build the reactive form with validators
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });

    // Read returnUrl from query params (set by AuthGuard)
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/dashboard';
  }

  // Convenience getters for template access
  get emailControl() { return this.loginForm.get('email'); }
  get passwordControl() { return this.loginForm.get('password'); }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched(); // Show validation errors on all fields
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login(this.loginForm.value).subscribe({
      next: () => {
        this.router.navigateByUrl(this.returnUrl);
      },
      error: () => {
        this.errorMessage = 'Invalid email or password.';
        this.isLoading = false;
      }
    });
  }
}
