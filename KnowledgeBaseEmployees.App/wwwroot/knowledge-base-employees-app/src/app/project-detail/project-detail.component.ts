import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { ProjectApiService } from 'app/api-services/project-api.service';
import { Project } from '../models/project.model';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {
  project: Project;
  projectFormGroup: FormGroup;
  private projectId: Number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectApiService: ProjectApiService) {
      this.project = new Project();
      route.params.subscribe(param => {
        this.projectId = param['id'];
      });
  }

  ngOnInit() {
    this.initForm();

    if (this.projectId) {
      this.loadProject();
    }
  }

  save = () => {
    this.fetchFormData();

    if(this.projectId){
      this.projectApiService
        .updateProject(this.projectId, this.project)
        .subscribe(
          x => {
            this.router.navigateByUrl(`projects`);
          },
          error => { console.error('Project saving failed!'); });
    } else {
      this.projectApiService
        .saveProject(this.project)
        .subscribe(
          x => { 
            this.router.navigateByUrl(`projects`);
          },
          error => { console.error('Project saving failed!'); });
    }
  }

  reload = () => {
    this.loadProject();
  }

  private loadProject = () => {
    this.projectApiService
        .getBy(this.projectId)
        .subscribe(p => {
          this.project = p as Project;
          this.bindFormData();
        },
        (error) => {
          console.error(`Error happen while fetching project by ${this.projectId}`);
        });
  }

  private initForm = () => {
    this.projectFormGroup = new FormGroup({
      name: new FormControl('name', Validators.required),
      code: new FormControl('code', Validators.required),
      description: new FormControl('description')
    });
  }

  private bindFormData = () => {
    this.projectFormGroup.controls['name'].patchValue(this.project.name);
    this.projectFormGroup.controls['code'].patchValue(this.project.code);
    this.projectFormGroup.controls['description'].patchValue(this.project.description);
  }

  private fetchFormData = () => {
    this.project.name = this.projectFormGroup.controls['name'].value;
    this.project.code = this.projectFormGroup.controls['code'].value;
    this.project.description = this.projectFormGroup.controls['description'].value;
  }
}