// src/app/app.module.ts
// Demonstrates: Root NgModule, HTTP_INTERCEPTORS multi-provider,
// BrowserAnimationsModule, forRoot pattern

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Interceptors (registered as multi-providers — both run in chain)
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';

// Angular Material — app-wide
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent   // Only root component here; feature components are in feature modules
  ],
  imports: [
    BrowserModule,
    HttpClientModule,          // Provides HttpClient for injection everywhere
    AppRoutingModule,
    MatSnackBarModule,          // Needed at root level for ErrorInterceptor's snack bar
    SharedModule
  ],
  providers: [
    // HTTP Interceptors are registered as multi-providers.
    // 'multi: true' means Angular accumulates them into an array rather than overwriting.
    // Interceptors execute in ORDER of registration (AuthInterceptor first, then ErrorInterceptor).
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
