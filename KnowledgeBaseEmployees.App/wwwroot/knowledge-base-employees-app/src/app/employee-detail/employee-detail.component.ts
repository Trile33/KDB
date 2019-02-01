import { Component, OnInit } from '@angular/core';
import { Employee } from 'app/models/employee.model';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { EmployeeApiService } from 'app/api-services/employee-api.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;
  employeeFormGroup: FormGroup;
  private employeeId: Number;
  employees: Employee[] = new Array<Employee>();

  constructor(private route: ActivatedRoute,
    private employeeApiService: EmployeeApiService,
    private router: Router) {
      this.employee = new Employee();
      route.params.subscribe(param => {
        this.employeeId = param['id'];
      });
  }
  
  ngOnInit() {
    this.initForm();
    
    if (this.employeeId) {
      this.loadEmployee();
    }
  }

  save = () => {
    this.fetchFormData();

    if(this.employeeId){
      this.employeeApiService
        .updateEmployee(this.employeeId, this.employee)
        .subscribe(
          x => {
            this.router.navigateByUrl(`employees`);
          },
          error => { console.error('Employee saving failed!'); });
    } else {
      this.employeeApiService
        .saveEmployee(this.employee)
        .subscribe(
          x => { 
            this.router.navigateByUrl(`employees`);
          },
          error => { console.error('Employee saving failed!'); });
    }
  }
  
  reload = () => {
    this.loadEmployee()
  }

  private loadEmployee = () => {
      this.employeeApiService
        .getBy(this.employeeId)
        .subscribe(p => {
          this.employee = p as Employee;
          this.bindFormData();
        },
        (error) => {
          console.error(`Error happen while fetching project by ${this.employeeId}`);
        });
  }

  private initForm = () => {
    this.employeeFormGroup = new FormGroup({
      firstName: new FormControl('firstName', Validators.required),
      lastName: new FormControl('lastName', Validators.required),
      username: new FormControl('username', Validators.required),
      password: new FormControl('password', Validators.required),
      isTeamLead: new FormControl(false)
    });
  }

  private bindFormData = () => {
    this.employeeFormGroup.controls['firstName'].patchValue(this.employee.firstName);
    this.employeeFormGroup.controls['lastName'].patchValue(this.employee.lastName);
    this.employeeFormGroup.controls['username'].patchValue(this.employee.username);
    this.employeeFormGroup.controls['password'].patchValue(this.employee.password);
    this.employeeFormGroup.controls['isTeamLead'].patchValue(this.employee.isTeamLead);
  }

  private fetchFormData = () => {
    this.employee.firstName = this.employeeFormGroup.controls['firstName'].value;
    this.employee.lastName = this.employeeFormGroup.controls['lastName'].value;
    this.employee.username = this.employeeFormGroup.controls['username'].value;
    this.employee.password = this.employeeFormGroup.controls['password'].value;
    this.employee.isTeamLead = this.employeeFormGroup.controls['isTeamLead'].value;
  }
}