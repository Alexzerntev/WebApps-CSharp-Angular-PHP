import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepositCodeComponent } from './deposit-code/deposit-code.component';
import { DepositeCodesService } from './deposite-codes.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DepositCodeComponent],
  providers:[DepositeCodesService]
})
export class DepositCodesModule { }
