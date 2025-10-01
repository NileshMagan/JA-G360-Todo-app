import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./todos/todos.component').then(m => m.TodosComponent)
  }
];
