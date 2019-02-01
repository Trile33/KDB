import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { Project } from '../../models/project.model';
import { ProjectApiService } from 'app/api-services/project-api.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  @Input() project: Project;
  projects: Project[] = new Array<Project>();

  constructor(private projectApi: ProjectApiService, private router: Router) { }

  ngOnInit() {
  }
}
