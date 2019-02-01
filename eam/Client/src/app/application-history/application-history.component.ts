import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from '../core/loader/loader.service';
import { AuthService } from '../core/auth/services/auth.service';
import { ApplicationHistoryService } from './application-history.service';

@Component({
  selector: 'app-application-history',
  templateUrl: './application-history.component.html',
  styleUrls: ['./application-history.component.scss']
})
export class ApplicationHistoryComponent implements OnInit {

  apps = [];
  page = 1;

  constructor(
    public loaderService: LoaderService,
    public toastrService: ToastrService,
    public authService: AuthService,
    public applicationHistoryService: ApplicationHistoryService
  ) { }

  ngOnInit() {

    this.loaderService.show();
    this.applicationHistoryService.getHistory().subscribe(
      result => {
        this.apps = result.data;
        
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.warning(error.error.error, "Προσοχή");
      }
    );

  }

  onPageChange(page) {
    this.page = page;
  }



}
