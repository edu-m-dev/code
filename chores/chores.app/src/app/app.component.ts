import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container">
      <header class="d-flex align-items-center justify-content-between py-4">
        <div>
          <h1 class="h3 mb-1">Chores</h1>
          <nav class="mt-2">
            <a routerLink="/" class="me-3 text-muted">List</a>
            <a routerLink="/add" class="text-muted">Add</a>
          </nav>
        </div>
        <div>
          <span *ngIf="username() as u" class="me-2 small text-secondary">Signed in: {{ u }}</span>
          <button *ngIf="!username()" (click)="login()" class="btn btn-primary btn-sm me-1">Login</button>
          <button *ngIf="username()" (click)="logout()" class="btn btn-outline-secondary btn-sm">Logout</button>
        </div>
      </header>

      <main class="card shadow-sm">
        <div class="card-body">
          <router-outlet></router-outlet>
        </div>
      </main>
    </div>
  `
})
export class AppComponent {
  private auth = inject(AuthService);
  username = signal<string | null>(this.auth.getAccountUsername());

  async login() {
    await this.auth.login();
    this.username.set(this.auth.getAccountUsername());
  }

  async logout() {
    await this.auth.logout();
    this.username.set(null);
  }
}
