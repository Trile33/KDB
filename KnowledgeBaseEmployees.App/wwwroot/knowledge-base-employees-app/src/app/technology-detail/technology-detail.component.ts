import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Technology } from 'app/models/technology.model';
import { TechnologyApiService } from 'app/api-services/technology-api.service';
import { AuthGuard } from 'app/guards/auth.guard';

@Component({
  selector: 'app-technology-detail',
  templateUrl: './technology-detail.component.html',
  styleUrls: ['./technology-detail.component.css']
})

export class TechnologyDetailComponent implements OnInit {
  technologyFormGroup: FormGroup;
  technology: Technology;
  private technologyId: Number;

  constructor(private route: ActivatedRoute,
              private technologyApi: TechnologyApiService,
              private router: Router,
              private authGuard: AuthGuard
              ){ 
    this.technology = new Technology();
    route.params.subscribe(param => {
    this.technologyId = param['id'];
    });}

  ngOnInit() {
    this.authGuard.isLoggedIn();
    this.initForm();
    if (this.technologyId) {
      this.loadTechnology();
    }
  }

  save = () => {
    this.fetchFormData();

    if(this.technologyId){
      this.technologyApi
        .updateTechnology(this.technologyId, this.technology)
        .subscribe(
          x => {
            this.router.navigateByUrl('technologies');
          },
          error => { console.error('Technology saving failed!'); });
    } else {
      this.technologyApi
        .saveTechnology(this.technology)
        .subscribe(
          x => { 
            this.router.navigateByUrl('technologies');
          },
          error => { console.error('Technology saving failed!'); });
    }
  }

  reload(technology: Technology){
    this.loadTechnology();
  }

  loadTechnology = () => {
    this.technologyApi
      .getBy(this.technologyId)
      .subscribe(p => {
        this.technology = p as Technology;
        this.bindFormData();
      },
      (error) => {
        console.error(`Error happen while fetching project by ${this.technologyId}`);
      });
  }

  private initForm = () => {
    this.technologyFormGroup = new FormGroup({
      name: new FormControl('name', Validators.required),
      information: new FormControl('information'),
    });
  }

  private bindFormData = () => {
    this.technologyFormGroup.controls['name'].patchValue(this.technology.name);
    this.technologyFormGroup.controls['information'].patchValue(this.technology.information);
  }

  private fetchFormData = () => {
    this.technology.name = this.technologyFormGroup.controls['name'].value;
    this.technology.information = this.technologyFormGroup.controls['information'].value;
  }
}