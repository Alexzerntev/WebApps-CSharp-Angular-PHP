import { Component, OnInit } from '@angular/core';
import { SecretaryHistoryService } from './secretary-history.service';
import { AuthService } from '../core/auth/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from '../core/loader/loader.service';

@Component({
  selector: 'app-secretary-history',
  templateUrl: './secretary-history.component.html',
  styleUrls: ['./secretary-history.component.scss']
})
export class SecretaryHistoryComponent implements OnInit {

  apps = [];

  page = 1;

  constructor(
    public loaderService: LoaderService,
    public toastrService: ToastrService,
    public authService: AuthService,
    public secretaryHistoryService: SecretaryHistoryService
  ) { }

  ngOnInit() {

    this.loaderService.show();
    this.secretaryHistoryService.getHistory().subscribe(
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

  deleteEntry(id) {
    this.loaderService.show();
    this.secretaryHistoryService.deleteEntry(id).subscribe(
      result => {
        this.apps.splice(this.apps.findIndex(x => x.id == id), 1);
        this.loaderService.hide();
        this.toastrService.success("Η διαγραφή πραγματοποιήθηκε με επιτυχία", "Επιτυχία");
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Αποτυχία");
      }
    );
  }

  
  onPageChange(page) {
    this.page = page;
  }

}
