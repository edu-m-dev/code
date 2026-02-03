import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-add',
  template: `
    <div class="max-w-xl mx-auto">
      <h2 class="text-2xl font-semibold mb-4">Add Chore</h2>
      <form (ngSubmit)="submit()" class="space-y-4">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Name</label>
          <input [(ngModel)]="model.name" name="name" required class="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-300" />
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Description</label>
          <input [(ngModel)]="model.description" name="description" class="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-300" />
        </div>
        <div class="flex gap-3">
          <button type="submit" class="px-4 py-2 bg-green-600 text-white rounded">Add</button>
          <a routerLink="/" class="px-4 py-2 bg-gray-200 rounded">Cancel</a>
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
