import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { SpinGameService } from '../spin-game.service';
import { UserProfileService } from '../../core/profile/user-profile.service';
import { LoaderService } from '../../core/loader/loader.service';
import { ToasterService } from 'angular2-toaster';


import { Spin } from '../spin-game.model';
import { error } from 'selenium-webdriver';

declare var TweenMax: any;
declare var Spin2WinWheel: any;

@Component({
  selector: 'app-wheel',
  templateUrl: './wheel.component.html',
  styleUrls: ['./wheel.component.scss']
})
export class WheelComponent implements OnInit, AfterViewInit {
  jsonData: any;
  mySpinBtn: any;
  myWheel: any;
  playAmount: number = 1;
  maxMultiplyer: number = 0;
  maxPlayAmount: number;
  isButtonDisabled: boolean = false;
  balance:number;

  constructor(
    private router: Router,
    private spinGameService: SpinGameService,
    private userProfileService: UserProfileService,
    private loaderService: LoaderService,
    private toasterService: ToasterService) {
      this.userProfileService.userProfile.Balance;
    }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.loaderService.show();
    this.spinGameService.getWheelSettings().subscribe(res => {
      this.jsonData = res;
      let segmentArray = [];
      segmentArray = this.jsonData.segmentValuesArray;
      //create a new instance of Spin2Win Wheel and pass in the vars object
      this.myWheel = new Spin2WinWheel();
      this.mySpinBtn = document.querySelector('.spinBtn');

      //WITH your own button
      this.myWheel.init({
        data: this.jsonData,
        onResult: (e) => { this.myResult(e) },
        onGameEnd: (e) => { this.myGameEnd(e) },
        onError: (e) => { this.myError(e) },
        spinTrigger: this.mySpinBtn
      });
      //WITHOUT your own button
      //this.myWheel.init({ data: this.jsonData, onResult: this.myResult, onGameEnd: this.myGameEnd, onError: this.myError });
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.toasterService.pop("error", "Σφάλμα", error);
      }
    );
    this.spinGameService.getMaxMultyplyer().subscribe(res => {
      this.maxMultiplyer = res;
      this.userProfileService.getBalanceExposed().subscribe(res=>{
        this.balance = res;
        this.maxPlayAmount = -(this.balance/this.maxMultiplyer)
      }
      );
    },
      error => {
        this.toasterService.pop("error", "Σφάλμα", error);
      }
    )
    

  }

  myResult(e) {
    //e is the result object
    //console.log(e);
    //console.log('Spin Count: ' + e.spinCount + ' - ' + 'Win: ' + e.win + ' - ' + 'Message: ' + e.msg);

    // if you have defined a userData object...
    // if (e.userData) {
    //   console.log('User defined score: ' + e.userData.score);
    // }
    //this.userProfileService.updateBalance(e.userData.score * this.playAmount);
    this.userProfileService.getBalanceExposed().subscribe(res=>{
      this.balance = res;
      this.maxPlayAmount = -(this.balance/this.maxMultiplyer)
    }
    );

    //if(e.spinCount == 3){
    //show the game progress when the spinCount is 3
    //console.log(e.target.getGameProgress());
    //restart it if you like
    //e.target.restart();
    //}  

  }

  myError(e) {
    //e is error object
    this.toasterService.pop("error", "Σφάλμα", e.msg);
    //console.log('Spin Count: ' + e.spinCount + ' - ' + 'Message: ' + e.msg);

  }

  myGameEnd(e) {
    //e is gameResultsArray
    this.delay(5000).then(() => {
      this.router.navigate(['/blank']);
      this.userProfileService.getBalance();
    });

  }

  addAmount() {
    this.playAmount += 0.5;
  }

  subAmount() {
    this.playAmount -= 0.5;
  }

  getResult() {
    if(this.balance + (this.maxMultiplyer * this.playAmount) < 0 || this.playAmount === 0)
    {
      return;
    }
    this.isButtonDisabled = true;
    this.spinGameService.getResult(this.playAmount).subscribe(res => {
      this.jsonData.spinDestinationArray.push(res);
      this.myWheel.init({
        data: this.jsonData,
        onResult: (e) => { this.myResult(e) },
        onGameEnd: (e) => { this.myGameEnd(e) },
        onError: (e) => { this.myError(e) },
        spinTrigger: this.mySpinBtn
      });
      this.mySpinBtn.click();
    },
      error => {
        this.toasterService.pop("error", "Σφάλμα", error);
      }
    );
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }


}




