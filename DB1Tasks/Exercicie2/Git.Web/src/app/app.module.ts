import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { routing } from './app.routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { UsersComponent } from './users/users.component';
import { UsersService } from './users/shared/services/users.service';
import { AppEnvironment, AppEnvironmentFactory } from './app-environment';
import { AppConfig, AppConfigFactory } from './app.config';

@NgModule({
  declarations: [AppComponent, NavbarComponent, UsersComponent],
  imports: [BrowserModule, HttpModule, routing],
  providers: [
    UsersService,
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: AppConfigFactory,
      deps: [AppConfig],
      multi: true
    },
    {
      provide: AppEnvironment,
      useFactory: AppEnvironmentFactory,
      deps: [AppConfig]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
