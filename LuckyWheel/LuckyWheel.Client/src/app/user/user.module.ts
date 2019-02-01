import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { FileSelectDirective } from 'ng2-file-upload/ng2-file-upload';

import { UserInfoComponent } from './user-info/user-info.component';

import { UserInfoService } from '../user/user-info.service';
import { ProfileImageDirective } from './profile-image.directive';

import { LoadersCssModule } from 'angular2-loaders-css';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [ 
    UserInfoComponent,
    FileSelectDirective,
    ProfileImageDirective
   ],
  providers: [UserInfoService]
})
export class UserModule { }
