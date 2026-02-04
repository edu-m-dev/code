import { Routes } from '@angular/router';
import { ChoreListComponent } from './components/chore-list/chore-list.component';
import { ChoreDetailComponent } from './components/chore-detail/chore-detail.component';
import { ChoreAddComponent } from './components/chore-add/chore-add.component';
import { ChoreEditComponent } from './components/chore-edit/chore-edit.component';

export const routes: Routes = [
  { path: '', component: ChoreListComponent },
  { path: 'chore/:id', component: ChoreDetailComponent },
  { path: 'add', component: ChoreAddComponent }
  ,{ path: 'edit/:id', component: ChoreEditComponent }
];
