import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { ToasterService } from 'angular2-toaster';
import { LoaderService } from '../../core/loader/loader.service';

import { UserInfoService } from '../user-info.service';
import { UserInfo } from '../user.model';
import { environment } from '../../../environments/environment';

import { AuthService } from '../../core/auth/services/auth.service';



@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  private apiUrl: string = environment.apiUri + "User";

  public uploader: FileUploader = new FileUploader({ url: this.apiUrl + "/UploadPhoto" });
  public hasBaseDropZoneOver: boolean = false;
  public hasAnotherDropZoneOver: boolean = false;

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }

  constructor(private userInfoService: UserInfoService,
    private authService: AuthService,
    public toasterService: ToasterService,
    public loaderService: LoaderService
  ) { }

  userInfo: UserInfo = new UserInfo();


  ngOnInit() {
    this.loaderService.show();
    this.userInfoService.getUserInfo()
      .subscribe(result => {
        this.userInfo = result;
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toasterService.pop("error", "Σφάλμα", error);
      });
    this.uploader.authToken = "Bearer " + this.authService.getToken();
  }
}
