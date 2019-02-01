import { Component, OnInit } from '@angular/core';
import { BreadcrumbsService } from './breadcrumbs.service';

@Component({
  selector: 'app-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  styleUrls: ['./breadcrumbs.component.scss']
})
export class BreadcrumbsComponent implements OnInit {

  constructor(private breadCrumbsService: BreadcrumbsService) { }

  ngOnInit() {
  }

}
