import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Chore } from '../../models/chore.model';
import { ChoresService } from '../../services/chores.service';

@Component({
  selector: 'app-chore-detail',
  template: `
    <div *ngIf="chore" class="space-y-4">
      <div class="flex items-start justify-between">
        <div>
          <h2 class="text-2xl font-semibold">{{ chore.name }}</h2>
          <p class="text-sm text-gray-600 mt-1">ID: {{ chore.id }}</p>
        </div>
        <a routerLink="/" class="text-sm text-blue-600">Back</a>
      </div>
      <div class="p-4 bg-gray-50 border rounded">{{ chore.description }}</div>
    </div>
  `,
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class ChoreDetailComponent implements OnInit {
  chore: Chore | undefined;

  constructor(private choresService: ChoresService) { }

  ngOnInit(): void {
    const id = Number(location.hash.split('/').pop());
    this.choresService.getAllChores().subscribe(list => {
      this.chore = list.find(c => c.id === id);
    });
  }
}
