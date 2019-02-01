import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import { environment } from '../../environments/environment'
import { Router, Route, ActivatedRoute } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { HomeDataService } from './home-data.service';
import { LoaderService } from '../core/loader/loader.service';
import { AuthService } from '../core/auth/services/auth.service';
import { DomSanitizer } from '@angular/platform-browser';
import { BreadcrumbsService } from '../breadcrumbs/breadcrumbs.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private homeDataService: HomeDataService,
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private breadCrumbsService: BreadcrumbsService
  ) { }

  ngOnInit() {
  }

}
