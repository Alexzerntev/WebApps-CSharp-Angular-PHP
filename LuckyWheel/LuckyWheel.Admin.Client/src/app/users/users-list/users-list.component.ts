 import { Component, OnInit, AfterViewInit} from '@angular/core';
 import { Router } from '@angular/router';

import { UsersService } from '../users.service';
import { UserProfileComponent } from '../user-profile/user-profile.component';

import { User } from '../users.model';



@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit, AfterViewInit {

  userList: Array<User> = [];
  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.usersService.getUsers().subscribe(result => {
      this.userList = result;
    });

  }
  onSelect(user) {
    this.router.navigate(['./user-profile/' + user.UserId]);
  }
}
