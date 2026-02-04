import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-edit',
  templateUrl: './chore-edit.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule]
})
export class ChoreEditComponent implements OnInit {
  model: Chore | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private choresService: ChoresService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.choresService.getAllChores().subscribe(list => {
      this.model = list.find(c => c.id === id);
    });
  }

  submit() {
    if (!this.model) return;
    this.choresService.updateChore(this.model).subscribe(() => {
      this.router.navigate(['/']);
    });
  }
}
