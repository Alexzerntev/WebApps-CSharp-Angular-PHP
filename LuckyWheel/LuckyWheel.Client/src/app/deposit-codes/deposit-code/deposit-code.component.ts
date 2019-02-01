import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';

import { DepositeCodesService } from '../deposite-codes.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToasterService } from 'angular2-toaster';
import { UserProfileService } from '../../core//profile/user-profile.service';

import { DepositeCode } from './deposit-code.model';
import { from } from 'rxjs/observable/from';

@Component({
  selector: 'app-deposit-code',
  templateUrl: './deposit-code.component.html',
  styleUrls: ['./deposit-code.component.scss']
})
export class DepositCodeComponent implements OnInit {

  depositecodelist: Array<DepositeCode>;


  constructor(
    private depositecodeservice: DepositeCodesService,
    private router: Router,
    private route: ActivatedRoute,
    private loaderService: LoaderService,
    private toasterService: ToasterService,
    public userProfileService : UserProfileService
  ) { }

  ngOnInit() {
    this.loaderService.show();
    this.depositecodeservice.getDepositeCodes().subscribe(result => {
      this.depositecodelist = result;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.toasterService.pop('error', 'Σφάλμα', error);
      });
  }
  useDepositCode(code: DepositeCode) {
    this.loaderService.show();
    this.depositecodeservice.useDepositCode(code).subscribe(result => {
      this.userProfileService.getBalance();
      this.loaderService.hide();
      this.depositecodeservice.getDepositeCodes().subscribe(result => {
        this.depositecodelist = result;
        this.loaderService.hide();
      },
        error => {
          this.loaderService.hide();
          this.toasterService.pop('error', 'Σφάλμα', error);
        });
    },
      error => {
        this.loaderService.hide();
        this.toasterService.pop('error', 'Σφάλμα', error);
      });

  }


}
