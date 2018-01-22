import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { routing } from './app.routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { UsersComponent } from './users/users.component';
import { UsersService } from './users/shared/services/users.service';
import { UserDetailComponent } from './users/shared/components/user-detail/user-detail.component';
import { InlineFormLabelComponent } from './users/shared/components/inline-form-label/inline-form-label.component';
import { UserRepositoriesComponent } from './users/shared/components/user-repositories/user-repositories.component';

@NgModule({
  declarations: [AppComponent, NavbarComponent, UsersComponent, UserDetailComponent, InlineFormLabelComponent, UserRepositoriesComponent],
  imports: [BrowserModule, HttpModule, routing],
  providers: [
    UsersService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
