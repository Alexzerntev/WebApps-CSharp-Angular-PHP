import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-blank',
  templateUrl: './blank.component.html',
  styleUrls: ['./blank.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class BlankComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
    this.router.navigate(['/spin-game']);
  }

}
