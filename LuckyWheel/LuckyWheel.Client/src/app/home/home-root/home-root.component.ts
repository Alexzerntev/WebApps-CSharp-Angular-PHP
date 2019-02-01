import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-root',
  templateUrl: './home-root.component.html',
  styleUrls: ['./home-root.component.scss']
})
export class HomeRootComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

}
