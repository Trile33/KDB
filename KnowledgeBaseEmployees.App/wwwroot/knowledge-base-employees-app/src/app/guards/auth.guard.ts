
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { User } from 'app/models/user.model';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      if (localStorage.getItem('currentUser')) {
        return true;
      }

      this.router.navigate(['/login'], {queryParams: {returnUrl: state.url}});
      return false;
  }

  isLoggedIn(){
    let currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      let user: User = JSON.parse(currentUser);
      return user.token !== undefined;
    }

    this.router.navigate(['/login']);
    return false;
  }

  isAdmin(){
    let currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      let user: User = JSON.parse(currentUser);
      return user.role === 'Admin';
    }

    return false;
  }
}