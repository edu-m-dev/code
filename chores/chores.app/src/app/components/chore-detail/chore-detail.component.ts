import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Chore } from '../../models/chore.model';
import { ChoresService } from '../../services/chores.service';

@Component({
  selector: 'app-chore-detail',
  template: `
    <div *ngIf="chore">
      <div class="d-flex justify-content-between align-items-start mb-3">
        <div>
          <h2 class="h5 mb-1">{{ chore.name }}</h2>
          <div class="small text-muted">ID: {{ chore.id }}</div>
        </div>
        <a routerLink="/" class="btn btn-link">Back</a>
      </div>

      <div class="card">
        <div class="card-body">{{ chore.description }}</div>
      </div>
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
