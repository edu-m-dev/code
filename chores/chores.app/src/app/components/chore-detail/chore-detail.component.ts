import { Component, OnInit } from '@angular/core';
import { Chore } from '../../models/chore.model';
import { ChoresService } from '../../services/chores.service';

@Component({
  selector: 'app-chore-detail',
  template: `
    <div *ngIf="chore">
      <h2>{{ chore.name }}</h2>
      <p>{{ chore.description }}</p>
      <a routerLink="/">Back</a>
    </div>
  `,
  standalone: true,
  imports: []
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
