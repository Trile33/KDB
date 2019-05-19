import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import {Technology} from '../models/technology.model';
import {TechnologyApiService} from '../api-services/technology-api.service';
import { ProjectApiService } from 'app/api-services/project-api.service';
import { EmployeeApiService } from 'app/api-services/employee-api.service';
import { UsedAs } from 'app/models/used-as.enum';
import { SearchQuery } from 'app/models/search-query.model';
import { AuthGuard } from 'app/guards/auth.guard';

@Component({
  selector: 'app-technologies',
  templateUrl: './technologies.component.html',
  styleUrls: ['./technologies.component.css']
})
export class TechnologiesComponent implements OnInit {

  @Input() usedAs: UsedAs = UsedAs.StandAlone;

  projectId: Number;
  employeeId: Number;

  technologies: Technology[] = new Array<Technology>();
  technologiesForAdding: Technology[] = new Array<Technology>();
  selectedTechnology: Technology = null;
  tsq: SearchQuery;

  constructor(private technologyApi: TechnologyApiService,
              private route: ActivatedRoute,
              private projectApi: ProjectApiService, 
              private employeeApi: EmployeeApiService,
              private router: Router,
              private authGuard: AuthGuard) {

                this.tsq = new SearchQuery();
  }

  ngOnInit() {
    this.authGuard.isLoggedIn();
    this.route.params.subscribe(param => {
      if(this.usedAs ===  UsedAs.OnProject){
        this.projectId = parseInt(param['id']);
      }else if(this.usedAs ===  UsedAs.OnEmployee){
        this.employeeId = parseInt(param['id']);
      }
    });
    this.loadTechnologies();
  }

  addTechnologyToProject = () => {
    this.projectApi
      .saveProjectNewTechnology(this.projectId, this.selectedTechnology.id)
      .subscribe(
        x => { this.loadTechnologies(); },
        error => { console.error('Technology saving failed!'); });
  }

  addTechnologyToEmployee = () => {
    this.employeeApi
      .saveEmployeeNewTechnology(this.employeeId, this.selectedTechnology.id)
      .subscribe(
        x => { this.loadTechnologies(); },
        error => { console.error('Technology saving failed!'); });
  }

  delete = (technologyId: Number) => {
      this.technologyApi.deleteTechnology(technologyId)
        .subscribe(
          x => {
            this.loadTechnologies();
          },
          error => { console.error('Employee deleting failed!'); 
        });
  }

  deleteProjectTechnology = (technologyId: Number) => {
    this.projectApi.deleteTechnology(this.projectId, technologyId)
      .subscribe(
      x => {
        this.loadTechnologies();
      },
      error => { console.error('Technology deleting failed!');
    });
  }

  deleteEmployeeTechnology = (technologyId: Number) => {
    this.employeeApi.deleteTechnology(this.employeeId, technologyId)
      .subscribe(
      x => {
        this.loadTechnologies();
      },
      error => { console.error('Technology deleting failed!');
    });
  }

  onRowSelected = (technology: Technology) => {
    this.router.navigateByUrl(`technologies/${technology.id}`)
  }

  createNewTechnology = () => {
    this.router.navigateByUrl(`technologies/`);
  }

  searchBy = (esq: SearchQuery) => {
    this.technologyApi.searchBy(esq)
    .subscribe(t => {
          this.technologies = t;
        },
        () => {}
      );
  }

  private loadTechnologies = () => {
    this.technologyApi.getAll()
      .subscribe(
        (result: Technology[]) => {
          if(this.usedAs === UsedAs.StandAlone){
            this.technologies = result;
            this.technologiesForAdding = null;
          } else if(this.usedAs === UsedAs.OnProject){
            this.technologies = result.filter(e => { return e.projectIds.includes(this.projectId); });
            this.technologiesForAdding = result.filter(e => { return !e.projectIds.includes(this.projectId); });
          }else if(this.usedAs === UsedAs.OnEmployee){
            this.technologies = result.filter(e => { return e.employeeIds.includes(this.employeeId); });
            this.technologiesForAdding = result.filter(e => { return !e.employeeIds.includes(this.employeeId); });
          }
        },
        (error) => {}
      );
  }
}