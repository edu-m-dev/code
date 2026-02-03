import { Routes } from '@angular/router';
import { ChoresListComponent } from './components/chores-list/chores-list.component';
import { ChoreDetailComponent } from './components/chore-detail/chore-detail.component';
import { ChoreAddComponent } from './components/chore-add/chore-add.component';

export const routes: Routes = [
  { path: '', component: ChoresListComponent },
  { path: 'chore/:id', component: ChoreDetailComponent },
  { path: 'add', component: ChoreAddComponent }
];
