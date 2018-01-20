import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { UserDetailComponent } from './users/shared/components/user-detail/user-detail.component';

const appRoutes: Routes = [
  {
    path: 'users',
    component: UsersComponent
  },
  { path: 'users/:user', component: UserDetailComponent },
  { path: '', redirectTo: '/users', pathMatch: 'full' }
  // { path: '**', component: PaginaNaoEncontradaComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes, {
  useHash: true
});
