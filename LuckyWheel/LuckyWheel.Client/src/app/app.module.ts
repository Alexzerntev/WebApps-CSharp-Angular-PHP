import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

/* App Root */
import { AppComponent } from './app.component';

/* Feature Modules */
import { CoreModule } from './core/core.module';
import { DepositCodesModule } from './deposit-codes/deposit-codes.module';
import { HistoryModule } from './history/history.module';
import { HomeModule } from './home/home.module';
import { SpinGameModule } from './spin-game/spin-game.module';
import { UserModule } from './user/user.module';

/* Routing Module */
import { AppRoutingModule } from './app-routing.module';

/* Interseptor */
import { TokenInterceptor } from './core/auth/services/token.interceptor';
import { WidgetComponent } from './widget/widget.component';

@NgModule({
  declarations: [
    AppComponent,
    WidgetComponent
  ],
  imports: [
    CoreModule.forRoot(),
    MDBBootstrapModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    DepositCodesModule,
    HistoryModule,
    HomeModule,
    SpinGameModule,
    UserModule
  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
