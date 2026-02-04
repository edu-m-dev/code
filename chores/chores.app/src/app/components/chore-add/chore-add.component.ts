import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-add',
  templateUrl: './chore-add.component.html',
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
