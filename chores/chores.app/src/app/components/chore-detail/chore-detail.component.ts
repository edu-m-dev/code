import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Chore } from '../../models/chore.model';
import { ChoresService } from '../../services/chores.service';

@Component({
  selector: 'app-chore-detail',
  templateUrl: './chore-detail.component.html',
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
