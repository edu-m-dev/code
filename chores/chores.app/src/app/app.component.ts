import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="max-w-4xl mx-auto">
      <header class="flex items-center justify-between py-6">
        <div>
          <h1 class="text-3xl font-bold text-gray-800">Chores</h1>
          <nav class="mt-2">
            <a routerLink="/" class="text-sm text-gray-600 hover:text-gray-900 mr-3">List</a>
            <a routerLink="/add" class="text-sm text-gray-600 hover:text-gray-900">Add</a>
          </nav>
        </div>
        <div class="flex items-center gap-3">
          <span *ngIf="username() as u" class="text-sm text-gray-700">Signed in: {{ u }}</span>
          <button *ngIf="!username()" (click)="login()" class="px-3 py-1 bg-blue-600 text-white rounded">Login</button>
          <button *ngIf="username()" (click)="logout()" class="px-3 py-1 bg-gray-200 rounded">Logout</button>
        </div>
      </header>

      <main class="bg-white shadow-sm rounded-md p-6">
        <router-outlet></router-outlet>
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
