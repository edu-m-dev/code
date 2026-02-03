import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chores-list',
  template: `
    <input [(ngModel)]="query" placeholder="Search chores" />
    <ul>
      <li *ngFor="let chore of filteredChores()" (click)="select(chore)">
        <strong>{{ chore.name }}</strong> — {{ chore.description }}
      </li>
    </ul>
  `,
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class ChoresListComponent implements OnInit {
  chores: Chore[] = [];
  query = '';

  constructor(private choresService: ChoresService) { }

  ngOnInit(): void {
    this.choresService.getAllChores().subscribe(c => this.chores = c || []);
  }

  filteredChores(): Chore[] {
    const q = this.query?.toLowerCase() || '';
    return this.chores.filter(c => c.name.toLowerCase().includes(q) || (c.description || '').toLowerCase().includes(q));
  }

  select(chore: Chore) {
    // navigate to detail (simple location change to avoid Router injection in this scaffold)
    window.location.hash = `#/chore/${chore.id}`;
  }
}
