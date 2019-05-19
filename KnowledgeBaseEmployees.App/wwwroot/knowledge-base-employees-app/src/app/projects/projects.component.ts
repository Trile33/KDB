import { Component, OnInit, Input } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';

import {Project} from '../models/project.model';
import {ProjectApiService} from '../api-services/project-api.service';
import { EmployeeApiService } from 'app/api-services/employee-api.service';
import { SearchQuery } from 'app/models/search-query.model';
import { UsedAs } from 'app/models/used-as.enum';
import { TechnologyApiService } from 'app/api-services/technology-api.service';
import { AuthGuard } from 'app/guards/auth.guard';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  @Input() usedAs: UsedAs = UsedAs.StandAlone;

  employeeId: Number;
  technologyId: Number;

  projects: Project[] = new Array<Project>();
  projectsForAdding: Project[] = new Array<Project>();
  selectedProject: Project = null;
  psq: SearchQuery;

  constructor(
    private projectApi: ProjectApiService,
    private router: Router, 
    private employeeApi: EmployeeApiService,
    private technologyApi: TechnologyApiService,
    private route: ActivatedRoute,
    private authGuard: AuthGuard) 
    {
      this.psq = new SearchQuery() 
    }

  ngOnInit() {
    this.authGuard.isLoggedIn();
    this.route.params.subscribe(param => {
      if(this.usedAs ===  UsedAs.OnTechnology){
        this.technologyId = parseInt(param['id']);
      }else if(this.usedAs ===  UsedAs.OnEmployee){
        this.employeeId = parseInt(param['id']);
      }
    });
    this.loadProjects();
  }

  onRowSelected = (project: Project) => {
    this.router.navigateByUrl(`projects/${project.id}`)
  }

  createNewProject = () => {
    this.router.navigateByUrl(`projects/`);
  }

  addProjectToEmployee = () => {
    this.employeeApi
      .saveEmployeeNewProject(this.employeeId, this.selectedProject.id)
      .subscribe(
        x => { this.loadProjects(); },
        error => { console.error('Project saving failed!'); });
  }

  addProjectToTechnology = () => {
    this.technologyApi
      .saveTechnologyNewProject(this.technologyId, this.selectedProject.id)
      .subscribe(
        x => { this.loadProjects(); },
        error => { console.error('Project saving failed!'); });
  }

  searchBy = (psq: SearchQuery) => {
    this.projectApi.searchBy(psq)
    .subscribe(p => {
          this.projects = p;
        },
        () => {}
      );
  }

  delete = (projectId: Number) => {
    this.projectApi
      .deleteProject(projectId)
      .subscribe( 
        x => { 
          this.loadProjects();
        },
        error => { console.error('Project deleting failed!'); 
      });
  }

  deleteEmployeeProject = (projectId: Number) => {
    this.employeeApi
      .deleteProject(this.employeeId, projectId)
      .subscribe(
      x => {
        this.loadProjects();
      },
      error => { console.error('Project deleting failed!');
    });
  }

  deleteTechnologyProject = (projectId: Number) => {
    this.technologyApi
      .deleteProject(this.technologyId, projectId)
      .subscribe(
      x => {
        this.loadProjects();
      },
      error => { console.error('Project deleting failed!');
    });
  }

  private loadProjects = () => {
    this.projectApi.getAll()
      .subscribe(
        (result: Project[]) => {
          if(this.usedAs === UsedAs.StandAlone){
            this.projects = result;
            this.projectsForAdding = null;
          } else if(this.usedAs === UsedAs.OnTechnology){
            this.projects = result.filter(e => { return e.technologyIds.includes(this.technologyId); });
            this.projectsForAdding = result.filter(e => { return !e.technologyIds.includes(this.technologyId); });
          }else if(this.usedAs === UsedAs.OnEmployee){
            this.projects = result.filter(e => { return e.employeeIds.includes(this.employeeId); });
            this.projectsForAdding = result.filter(e => { return !e.employeeIds.includes(this.employeeId); });
          }
        },
        (error) => {}
      );
  }
}