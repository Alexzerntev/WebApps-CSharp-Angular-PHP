import { Component, OnInit } from '@angular/core';
import { ApplicationDataService } from './application-data.service';
import { LoaderService } from '../core/loader/loader.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../core/auth/services/auth.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';



@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent implements OnInit {

  modal = 'closed';
  modalTitle = '';
  hasChosen = false;
  chosenLesson = [];
  lessons = [];
  chosenIds = [];
  chosenLessonIds = [];
  chosenBooks = [];
  tog: boolean[] = new Array<boolean>();

  step = 0;

  constructor(
    public applicationDataService: ApplicationDataService,
    public loaderService: LoaderService,
    public toastrService: ToastrService,
    public authService: AuthService,
    public sanitizer: DomSanitizer,
    public router: Router
  ) { }

  ngOnInit() {
    this.loaderService.show();
    this.applicationDataService.getLessons(this.authService.getDepartmentId()).subscribe(
      result => {
        this.lessons = result.data;
        this.loaderService.hide();
      },
      error => {
        this.loaderService.hide();
        this.toastrService.error(error.error.error, "Error");
      }
    );

    for (let i = 0; i < 4; i++) {
      this.tog.push(false);
    }
  }

  setStep(step: number) {
    this.step = step;

    if (this.step == 1) {
      this.chosenBooks = [];
      for (let i = 0; i < this.lessons.length; i++) {
        for (let j = 0; j < this.lessons[i].books.length; j++) {
          if (this.chosenIds.find(x => x == this.lessons[i].books[j].id)) {
            this.chosenBooks.push(this.lessons[i].books[j]);
          }
        }
      }
    }
  }

  toggle(year) {
    this.tog[year] = !this.tog[year];
  }

  openModal(lesson) {
    this.chosenLesson = lesson;
    this.modalTitle = lesson.title;
    this.modal = 'open';

    for (let i = 0; i < lesson.books.length; i++) {
      if (this.chosenIds.find(x => x == lesson.books[i].id) != undefined) {
        this.hasChosen = true;
        return;
      }
    }
    this.hasChosen = false;

  }

  chooseBook(id, lessonId) {
    this.chosenIds.push(id);
    this.chosenLessonIds.push(lessonId);

    this.hasChosen = true;
  }

  unchooseBook(id, lessonId) {
    this.chosenIds.splice(this.chosenIds.indexOf(id), 1);
    this.chosenLessonIds.splice(this.chosenLessonIds.indexOf(lessonId), 1);
    this.hasChosen = false;
  }

  checkIfChosen(id) {
    if (this.chosenIds.find(x => x == id) == undefined) {
      return false;
    }
    return true;
  }

  checkIfChosenLesson(id) {
    if (this.chosenLessonIds.find(x => x == id) == undefined) {
      return false;
    }
    return true;
  }

  print() {
    let printContents, popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
    <body onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
  }

  addApplication() {
    let toSend = {
      array: [],
      jwt: []
    };
    toSend.array = []

    for (let i = 0; i < this.chosenIds.length; i++) {
      let obj = {
        book_id: [],
        lesson_id: []
      };
      obj.book_id = this.chosenIds[i];
      for (let j = 0; j < this.lessons.length; j++) {
        if (this.lessons[j].books.find(x => x.id == obj.book_id) != undefined) {
          obj.lesson_id = this.lessons[j].id;
        }
      }
      toSend.array.push(obj);
    }

    this.applicationDataService.postApplication(toSend).subscribe(
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
