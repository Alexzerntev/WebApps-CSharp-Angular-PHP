import { NgModule, ModuleWithProviders, Optional, SkipSelf } from '@angular/core';
import { HttpModule } from '@angular/http';

import { SharedModule } from '../../shared/shared.module';

import { AuthRoutingModule } from './auth-routing.module';

import { LoginComponent } from './login/login.component';

import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';
import { RegisterComponent } from './register/register.component';

@NgModule({
  imports: [
    HttpModule,
    SharedModule,
    AuthRoutingModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: []
})
export class AuthModule {

  constructor( @Optional() @SkipSelf() parrentModule: AuthModule) {
    if (parrentModule) {
      throw new Error('AuthModule is already loaded. Import it in the CoreModule only');
    }
  } 

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: AuthModule,
      providers: [
        AuthService,
        AuthGuard
      ]
    }
  }
}
