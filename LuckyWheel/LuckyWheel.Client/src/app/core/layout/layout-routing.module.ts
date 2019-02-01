import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/services/auth-guard.service';

import { HomeRootComponent } from '../../home/home-root/home-root.component';
import { TransactionsComponent } from '../../history/transactions/transactions.component';
import { SpinsComponent } from '../../history/spins/spins.component';
import { DepositCodeComponent } from '../../deposit-codes/deposit-code/deposit-code.component';
import { UserInfoComponent } from '../../user/user-info/user-info.component';
import { WheelComponent } from '../../spin-game/wheel/wheel.component';


const routes: Routes = [
  {path: 'home', component: HomeRootComponent},
  {path: 'transactions', component: TransactionsComponent, canActivate: [AuthGuard]},
  {path: 'spins', component: SpinsComponent, canActivate: [AuthGuard], },
  {path: 'deposit-codes', component: DepositCodeComponent, canActivate: [AuthGuard], },
  {path: 'profile', component: UserInfoComponent, canActivate: [AuthGuard], },
  {path: 'spin-game', component: WheelComponent, canActivate: [AuthGuard], }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
