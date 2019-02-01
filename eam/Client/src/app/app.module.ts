import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { DatePipe } from '@angular/common'

import { VgCoreModule } from 'videogular2/core';
import { VgControlsModule } from 'videogular2/controls';
import { VgOverlayPlayModule } from 'videogular2/overlay-play';
import { VgBufferingModule } from 'videogular2/buffering';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';

import { TokenInterceptor } from './core/auth/token.interceptor'

import { HomeDataService } from './home/home-data.service';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';
import { BreadcrumbsService } from './breadcrumbs/breadcrumbs.service';
import { ApplicationComponent } from './application/application.component';
import { ApplicationHistoryComponent } from './application-history/application-history.component';
import { SearchComponent } from './search/search.component';
import { StudentHelpComponent } from './student-help/student-help.component';
import { LessonsComponent } from './lessons/lessons.component';
import { SecretaryHistoryComponent } from './secretary-history/secretary-history.component';
import { SecretaryHelpComponent } from './secretary-help/secretary-help.component';
import { ApplicationDataService } from './application/application-data.service';
import { ProfileComponent } from './profile/profile.component';
import { SearchService } from './search/search.service';
import { LessonsDataService } from './lessons/lessons-data.service';
import { ApplicationHistoryService } from './application-history/application-history.service';
import { SecretaryHistoryService } from './secretary-history/secretary-history.service';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    BreadcrumbsComponent,
    ApplicationComponent,
    ApplicationHistoryComponent,
    SearchComponent,
    StudentHelpComponent,
    LessonsComponent,
    SecretaryHistoryComponent,
    SecretaryHelpComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    RouterModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  },
    DatePipe,
    HomeDataService,
    BreadcrumbsService,
    ApplicationDataService,
    SearchService,
    LessonsDataService,
    ApplicationHistoryService,
    SecretaryHistoryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
