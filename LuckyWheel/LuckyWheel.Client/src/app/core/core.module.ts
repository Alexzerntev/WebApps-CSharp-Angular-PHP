import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { LoadersCssModule } from 'angular2-loaders-css';
import { ToasterModule, ToasterService } from 'angular2-toaster';

/* Modules */
import { AuthModule } from '../core/auth/auth.module';
import { LayoutModule } from '../core/layout/layout.module';

/* Services */
import { UserProfileService } from './profile/user-profile.service';
import { LoaderService } from '../core/loader/loader.service';
import { WebsocketService } from './websocket.service';

/* Component */
import { LoaderComponent } from './loader/loader.component';

@NgModule({
  imports: [
    AuthModule.forRoot(),
    LayoutModule.forRoot(),
    BrowserAnimationsModule,
    ToasterModule,
    CommonModule,
    LoadersCssModule
  ],
  declarations: [LoaderComponent],
  providers: [],
  exports: [
    LayoutModule,
    LoadersCssModule,
    LoaderComponent,
    ToasterModule,
    BrowserAnimationsModule
  ]
})
export class CoreModule {

  constructor( @Optional() @SkipSelf() parrentModule: CoreModule) {
    if (parrentModule) {
      throw new Error('CoreModule is already loaded. Import it in the AppModule only');
    }
  }

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule,
      providers: [
        UserProfileService,
        LoaderService,
        ToasterService,
        WebsocketService
      ]
    }
  }
}