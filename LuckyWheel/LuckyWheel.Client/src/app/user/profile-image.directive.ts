import { Directive, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { DomSanitizer } from '@angular/platform-browser';
import { error } from 'selenium-webdriver';

@Directive({
  selector: '[profile-image]',
  providers: [],
  host: {
    '[src]': 'sanitizedImageData'
  }
})
export class ProfileImageDirective implements OnInit {
  imageData: any;
  sanitizedImageData: any;
  private apiUrl: string = environment.apiUri + "User";

  constructor(private http: HttpClient,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.sanitizedImageData = "../../assets/defaultPicture.png";
    this.http.get(this.apiUrl + "/DownloadPhoto")
      .map(image => image)
      .subscribe(
      data => {
        if (data) {
          this.imageData = 'data:image/png;base64,' + data;
          this.sanitizedImageData = this.sanitizer.bypassSecurityTrustUrl(this.imageData);
        }
      },
      error => {

      }
      );
  }
}