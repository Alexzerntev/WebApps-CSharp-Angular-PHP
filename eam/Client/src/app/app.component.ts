import { Component } from '@angular/core';

import { LoaderService } from './core/loader/loader.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'eam';
  constructor(public loaderService: LoaderService) {
  }
}
