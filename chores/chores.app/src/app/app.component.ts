import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <header style="display:flex;align-items:center;justify-content:space-between">
      <div>
        <h1>Chores</h1>
        <nav>
          <a routerLink="/">List</a> |
          <a routerLink="/add">Add</a>
        </nav>
      </div>
      <div>
        <span *ngIf="username() as u">Signed in: {{ u }}</span>
        <button *ngIf="!username()" (click)="login()">Login</button>
        <button *ngIf="username()" (click)="logout()">Logout</button>
      </div>
    </header>
    <router-outlet></router-outlet>
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
