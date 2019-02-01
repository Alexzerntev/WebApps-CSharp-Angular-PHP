import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deposit-codes',
  templateUrl: './deposit-codes.component.html',
  styleUrls: ['./deposit-codes.component.scss']
})
export class DepositCodesComponent implements OnInit {
  Codes = ['abc0100', 'abc0200', 'cba0550', 'wxz1000'];
  addCode(newCode: string) {
    if (newCode) {
      this.Codes.push(newCode);
    }
  }
  constructor() { }

  ngOnInit() {
  }

}
