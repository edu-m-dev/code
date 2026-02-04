import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChoresService } from '../../services/chores.service';
import { Chore } from '../../models/chore.model';

@Component({
  selector: 'app-chore-list',
  templateUrl: './chore-list.component.html',
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class ChoreListComponent implements OnInit {
  chores: Chore[] = [];

  constructor(private choresService: ChoresService) { }

  ngOnInit(): void {
    this.choresService.getAllChores().subscribe(c => this.chores = c || []);
  }
}
