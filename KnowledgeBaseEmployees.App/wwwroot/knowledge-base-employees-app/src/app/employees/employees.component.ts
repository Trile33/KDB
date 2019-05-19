import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

import { Employee } from 'app/models/employee.model';
import { EmployeeApiService } from 'app/api-services/employee-api.service';
import { element } from 'protractor';
import { ProjectApiService } from 'app/api-services/project-api.service';
import { UsedAs } from 'app/models/used-as.enum';
import { TechnologyApiService } from 'app/api-services/technology-api.service';
import { SearchQuery } from 'app/models/search-query';
import { AuthGuard } from 'app/guards/auth.guard';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  @Input() usedAs: UsedAs = UsedAs.StandAlone;

  projectId: Number;
  technologyId: Number;

  employees: Employee[] = new Array<Employee>();
  employeesForAdding: Employee[] = new Array<Employee>();
  selectedEmployee: Employee = null;
  esq: SearchQuery;

  constructor(private employeeApiService: EmployeeApiService, 
              private route: ActivatedRoute, 
              private router: Router, 
              private projectApi: ProjectApiService,
              private technologyApi: TechnologyApiService,
              private authGuard: AuthGuard) {

                this.esq = new SearchQuery();
  }

  ngOnInit() {
    this.authGuard.canRenderView();
    this.route.params.subscribe(param => {
      if(this.usedAs ===  UsedAs.OnProject){
        this.projectId = parseInt(param['id']);
      }else if(this.usedAs ===  UsedAs.OnTechnology){
        this.technologyId = parseInt(param['id']);
      }
  });
    this.loadEmployees();
  }

  onRowSelected = (employee: Employee) => {
    this.router.navigateByUrl(`employees/${employee.id}`)
  }

  createNewEmployee = () => {
    this.router.navigateByUrl(`employees/`);
  }

  addEmployeeToProject = () => {
    this.projectApi
      .saveProjectNewEmployee(this.projectId, this.selectedEmployee.id)
      .subscribe(
        x => { this.loadEmployees(); },
        error => { console.error('Employee saving failed!'); });
  }

  addEmployeeToTechnology = () => {
    this.technologyApi
      .saveTechnologyNewEmployee(this.technologyId, this.selectedEmployee.id)
      .subscribe(
        x => { this.loadEmployees(); },
        error => { console.error('Employee saving failed!'); });
  }

  delete = (employeeId: Number) => {
      this.employeeApiService.deleteEmployee(employeeId)
        .subscribe(
          x => {
            this.loadEmployees();
          },
          error => { console.error('Employee deleting failed!'); 
        });
  }

  deleteEmployeeFromProject = (employeeId: Number) => {
    this.projectApi
      .deleteEmployee(this.projectId, employeeId)
      .subscribe(
      x => {
        this.loadEmployees();
      },
      error => { console.error('Employee deleting failed!');
    });
  }

  deleteEmployeeFromTechnology = (employeeId: Number) => {
    this.technologyApi
      .deleteEmployee(this.technologyId, employeeId)
      .subscribe(
      x => {
        this.loadEmployees();
      },
      error => { console.error('Employee deleting failed!');
    });
  }

  searchBy = (esq: SearchQuery) => {
    this.employeeApiService.searchBy(esq)
    .subscribe(p => {
          this.employees = p;
        },
        () => {}
      );
  }

  private loadEmployees = () => {
    this.employeeApiService.getAll()
      .subscribe(
        (result: Employee[]) => {
            if(this.usedAs === UsedAs.StandAlone){  
              this.employees = result;
              this.employeesForAdding = null;
            }else if(this.usedAs === UsedAs.OnProject){
                this.employees = result.filter(p => { return p.projectIds.includes(this.projectId); });
                this.employeesForAdding = result.filter(p => { return !p.projectIds.includes(this.projectId); });
            }else if(this.usedAs === UsedAs.OnTechnology){
              this.employees = result.filter(p => { return p.technologyIds.includes(this.technologyId); });
              this.employeesForAdding = result.filter(p => { return !p.technologyIds.includes(this.technologyId); });
            }
        },
        (error) => {}
      );
  }
}