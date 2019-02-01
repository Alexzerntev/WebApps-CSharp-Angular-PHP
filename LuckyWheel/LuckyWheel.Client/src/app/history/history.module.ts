import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';

import { SpinsComponent } from './spins/spins.component';
import { DepositsComponent } from './deposits/deposits.component';
import { TransactionsComponent } from './transactions/transactions.component';

import { HistoryService } from './history.service';

@NgModule({
  imports: [
    CommonModule,
    NgxPaginationModule
  ],
  declarations: [
    SpinsComponent,
    DepositsComponent, TransactionsComponent
  ],
  providers: [HistoryService],
})
export class HistoryModule { }
