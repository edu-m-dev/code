import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h1>Chores</h1>
    <nav>
      <a routerLink="/">List</a> |
      <a routerLink="/add">Add</a>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent { }
