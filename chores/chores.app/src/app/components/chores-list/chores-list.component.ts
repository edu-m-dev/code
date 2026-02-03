import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chores-list',
  template: `
    <form class="row g-2 mb-3" (submit)="$event.preventDefault()">
      <div class="col">
        <input [(ngModel)]="query" placeholder="Search chores" class="form-control" />
      </div>
      <div class="col-auto">
        <button class="btn btn-primary">Search</button>
      </div>
    </form>

    <ul class="list-group">
      <li *ngFor="let chore of filteredChores()" (click)="select(chore)" class="list-group-item list-group-item-action d-flex justify-content-between align-items-start">
        <div class="ms-2 me-auto">
          <div class="fw-semibold">{{ chore.name }}</div>
          <div class="text-muted small">{{ chore.description }}</div>
        </div>
        <span class="badge bg-secondary rounded-pill">#{{ chore.id }}</span>
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
