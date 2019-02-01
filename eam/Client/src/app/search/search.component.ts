import { Component, OnInit } from '@angular/core';
import { SearchService } from './search.service';
import { LoaderService } from '../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../core/auth/services/auth.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  searchString = "";
  books = [];
  book = [];
  modal = 'closed';
  resultCount;
  page = 1;

  constructor(
    public searchService: SearchService,
    public loaderService: LoaderService,
    public toastrService: ToastrService,
    public authService: AuthService,
    public sanitizer: DomSanitizer,
    public router: Router
  ) { }

  ngOnInit() {
  }

  search() {
    this.loaderService.show();
    this.searchService.search(this.searchString).subscribe(
      result => {
        this.books = result.data;
        this.resultCount = result.data.length;
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Error");
      }
    );
  }

  openModal(b) {
    this.book = b;
    this.modal = 'open';
  }

  onSubmit(event) {
    if (event.key == "Enter") {
      this.search();
    }
  }

  onPageChange(page) {
    this.page = page;
  }

}
