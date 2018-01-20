import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from './users/users.component';

const appRoutes: Routes = [
  { path: 'users', component: UsersComponent },
  { path: '', redirectTo: '/users', pathMatch: 'full' }
  // { path: '**', component: PaginaNaoEncontradaComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, {useHash: true});
