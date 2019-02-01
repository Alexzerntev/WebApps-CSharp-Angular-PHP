import { Component, OnInit } from '@angular/core';
// import { SearchService } from './search.service';
import { LoaderService } from '../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../core/auth/services/auth.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { LessonsDataService } from './lessons-data.service';
import { SearchService } from '../search/search.service';

@Component({
  selector: 'app-lessons',
  templateUrl: './lessons.component.html',
  styleUrls: ['./lessons.component.scss']
})
export class LessonsComponent implements OnInit {

  searchString = "";
  searchString2 = "";
  lessons = [];
  lesson_id = [];

  books = [];
  book_id = [];

  modal = 'closed';
  resultCount;
  resultCount2;
  page = 1;

  constructor(
    // public searchService: SearchService,
    public loaderService: LoaderService,
    public toastrService: ToastrService,
    public authService: AuthService,
    public sanitizer: DomSanitizer,
    public router: Router,
    public lessonsDataService: LessonsDataService,
    public searchService: SearchService
  ) { }

  ngOnInit() {
  }

  search() {
    this.loaderService.show();
    this.lessonsDataService.search(this.searchString).subscribe(
      result => {
        this.lessons = result.data;
        this.resultCount = result.data.length;
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Error");
      }
    );
  }

  search2() {
    this.loaderService.show();
    this.searchService.search(this.searchString).subscribe(
      result => {
        this.books = result.data;
        this.resultCount2 = result.data.length;
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Error");
      }
    );
  }

  openModal(l) {
    this.lesson_id = l.id;
    this.modal = 'open';
  }

  onSubmit(event) {
    if (event.key == "Enter") {
      this.search();
    }
  }

  onSubmit2(event) {
    if (event.key == "Enter") {
      this.search2();
    }
  }


  onPageChange(page) {
    this.page = page;
  }

  add(id) {
    this.book_id = id;
    let obj = {
      lesson_id: this.lesson_id,
      book_id: this.book_id
    }

    this.lessonsDataService.addRelation(obj).subscribe(
      result => {
        this.toastrService.success("Η Καταχώρηση Πραγματοποιήθηκε με Επιτυχία", "Επιτυχία", {
          timeOut: 20000
        });
        this.router.navigate(['/home']);
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Error");
      }
    );
  }

}
