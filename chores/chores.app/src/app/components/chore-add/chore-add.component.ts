import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-add',
  template: `
    <div class="container">
      <h2 class="h5 mb-3">Add Chore</h2>
      <form (ngSubmit)="submit()">
        <div class="mb-3">
          <label class="form-label">Name</label>
          <input [(ngModel)]="model.name" name="name" required class="form-control" />
        </div>
        <div class="mb-3">
          <label class="form-label">Description</label>
          <input [(ngModel)]="model.description" name="description" class="form-control" />
        </div>
        <div class="d-flex gap-2">
          <button type="submit" class="btn btn-success">Add</button>
          <a routerLink="/" class="btn btn-secondary">Cancel</a>
        </div>
      </form>
    </div>
  `,
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class ChoreAddComponent {
  model: Chore = { name: '', description: '' };

  constructor(private choresService: ChoresService) { }

  submit() {
    this.choresService.addChore(this.model).subscribe(c => {
      window.location.href = '/';
    });
  }
}
