import { Component, OnInit } from '@angular/core';
import { User } from 'app/models/user.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginApiService } from 'app/api-services/login-api.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User;
  userFormGroup: FormGroup;
  returnUrl: string;
  submitted = false;
  currentUser;
  constructor(private route: ActivatedRoute, 
              private loginApiService: LoginApiService, 
              private router: Router, 
              private formBuilder: FormBuilder
              ) {

                this.user = new User();
               }

  ngOnInit() {
    this.initForm();
  }

  private initForm = () => {

    this.userFormGroup = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.loginApiService.logout();

    this.returnUrl = this.route.snapshot.queryParams['returnUrl' || '/'];
  }

  get f() { 
    return this.userFormGroup.controls; 
  }

  onSubmit() {
    this.submitted = true;

    if (this.userFormGroup.invalid){
      return;
    }
    
    this.user.username = this.f['username'].value;
    this.user.password = this.f['password'].value;

    this.loginApiService.login(this.user)
      .pipe(first())
      .subscribe(
        () => {
          this.router.navigate(['home']);
        },
        () => {
          
        });
  }
}
