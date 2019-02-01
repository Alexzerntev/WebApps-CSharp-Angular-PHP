import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { UsersListComponent } from './users-list/users-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

import { UsersService } from './users.service';

import { UserHistoryModule } from './user-history/user-history.module';
import { DepositCodesComponent } from './deposit-codes/deposit-codes.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    UserHistoryModule
  ],
  declarations: [
    UsersListComponent,
    UserProfileComponent,
    DepositCodesComponent
  ],
  providers: [
    UsersService
  ],
  exports: [ UsersListComponent, DepositCodesComponent ],
})
export class UsersModule { }
