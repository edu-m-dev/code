import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './app.component.html'
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
