import { NgModule, ModuleWithProviders, Optional, SkipSelf, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../../shared/shared.module';
import { LayoutRoutingModule } from './layout-routing.module';

import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule
  ],
  declarations: [
    NavbarComponent
  ],
  exports: [
    NavbarComponent
  ],
  schemas: [
    NO_ERRORS_SCHEMA
  ]
})
export class LayoutModule {
  constructor( @Optional() @SkipSelf() parrentModule: LayoutModule) {
    if (parrentModule) {
      throw new Error('AuthModule is already loaded. Import it in the CoreModule only');
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: LayoutModule,
      providers: []
    }
  }
}