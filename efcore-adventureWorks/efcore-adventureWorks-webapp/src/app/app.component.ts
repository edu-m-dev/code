import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <h1 class="mt-4 mb-4">My Application</h1>
      <router-outlet></router-outlet>
    </div>
  `,
  standalone: true,
  imports: [RouterOutlet]
})
export class AppComponent { title = 'efcore-adventureWorks-webapp'}
