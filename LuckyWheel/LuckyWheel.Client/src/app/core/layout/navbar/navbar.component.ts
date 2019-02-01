import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../auth/services/auth.service';
import { UserProfileService } from '../../profile/user-profile.service';

import { UserProfile } from '../../profile/user-profile.model';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  userProfile = new UserProfile();

  constructor(private authService: AuthService, private userProfileService: UserProfileService) { }

  ngOnInit() {
    this.userProfile = this.userProfileService.userProfile;
  }

}
