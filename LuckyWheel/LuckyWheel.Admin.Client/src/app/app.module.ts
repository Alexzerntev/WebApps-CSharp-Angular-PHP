import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';

import { ComponentsModule } from './components/components.module';
import { UsersModule } from './users/users.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WheelComponent } from './wheel/wheel.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    WheelComponent,
  ],
  imports: [
    ComponentsModule,
    UsersModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
