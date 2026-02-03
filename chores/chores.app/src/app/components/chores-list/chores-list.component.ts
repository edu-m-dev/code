import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chores-list',
  template: `
    <div class="flex items-center gap-3 mb-4">
      <input [(ngModel)]="query" placeholder="Search chores" class="flex-1 px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-300" />
      <button class="px-3 py-2 bg-blue-600 text-white rounded">Search</button>
    </div>

    <ul class="grid gap-4">
      <li *ngFor="let chore of filteredChores()" (click)="select(chore)" class="p-4 bg-gray-50 border rounded hover:shadow cursor-pointer">
        <div class="flex items-center justify-between">
          <div>
            <div class="text-lg font-semibold text-gray-800">{{ chore.name }}</div>
            <div class="text-sm text-gray-600 mt-1">{{ chore.description }}</div>
          </div>
          <div class="text-sm text-gray-500">#{{ chore.id }}</div>
        </div>
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
