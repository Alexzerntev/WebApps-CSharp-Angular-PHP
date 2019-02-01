import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SpinsComponent } from './spins/spins.component';
import { TransactionsComponent } from './transactions/transactions.component';

import { UserHistoryService } from './user-history.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    TransactionsComponent,
    SpinsComponent,
  ],
  providers: [UserHistoryService],
  exports: [ SpinsComponent, TransactionsComponent ],
})
export class UserHistoryModule {
}

