import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import { UsersService } from '../users/users.service';

import { environment } from '../../environments/environment';

import { Wheel } from './wheel.model';

@Component({
  selector: 'app-wheel',
  templateUrl: './wheel.component.html',
  styleUrls: ['./wheel.component.scss']
})
export class WheelComponent implements OnInit {
  private apiUrl: string = environment.apiUri;
  wheelList: Array<Wheel> = [];
  constructor(private usersService: UsersService, private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit() {
    this.usersService.getWheels().subscribe(result => {
      this.wheelList = result;
      console.log(this.wheelList);
    });
  }

}
