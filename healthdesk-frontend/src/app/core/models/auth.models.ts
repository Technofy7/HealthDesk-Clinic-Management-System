// src/app/core/models/auth.models.ts

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  userId: number;
  name: string;
  roles: string;
}

export interface CurrentUser {
  userId: number;
  name: string;
  roles: string;
  token: string;
}
