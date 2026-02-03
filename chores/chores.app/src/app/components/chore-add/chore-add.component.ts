import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-add',
  template: `
    <h2>Add Chore</h2>
    <form (ngSubmit)="submit()">
      <div>
        <label>Name</label>
        <input [(ngModel)]="model.name" name="name" required />
      </div>
      <div>
        <label>Description</label>
        <input [(ngModel)]="model.description" name="description" />
      </div>
      <button type="submit">Add</button>
    </form>
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
