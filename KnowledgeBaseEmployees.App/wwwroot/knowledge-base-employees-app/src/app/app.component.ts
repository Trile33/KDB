import { Component, OnInit } from '@angular/core';
import { AuthGuard } from 'app/guards/auth.guard';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean = false;
  ngOnInit(): void {
    this.isLoggedIn = this.authGuard.isLoggedIn()
  }
  constructor(private authGuard : AuthGuard){}
}
