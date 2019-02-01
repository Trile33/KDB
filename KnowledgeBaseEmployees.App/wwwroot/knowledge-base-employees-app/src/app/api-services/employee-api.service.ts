import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Employee } from 'app/models/employee.model';
import { Constants } from 'app/shared/constants';
import { SearchQuery } from 'app/models/search-query.model';


@Injectable()
export class EmployeeApiService {

  constructor(private http: HttpClient) { }

  getAll = (): Observable<any> => {
    return this.http
      .get(`${Constants.baseApiUrl}employees`);
  }

  getBy = (employeId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}employees/${employeId}`);
  }

  getEmployeeProjects = (employeeId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}employees/${employeeId}/projects`);
  }

  getEmployeeTechnologies = (employeeId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}employees/${employeeId}/technologies`);
  }

  deleteEmployee(employeeId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}employees/${employeeId}`);
  }

  saveEmployee(employee: Employee){
    return this.http
      .post(`${Constants.baseApiUrl}employees`, employee);
  }

  updateEmployee(employeeId: Number, employee: Employee){
    return this.http
      .put(`${Constants.baseApiUrl}employees/${employeeId}`, employee);
  }

  saveEmployeeNewTechnology(employeeId: Number, technologyId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}employees/${employeeId}/technologies/${technologyId}`, null);
  }

  saveEmployeeNewProject(employeeId: Number, projectId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}employees/${employeeId}/projects/${projectId}`, null);
  }

  deleteTechnology(employeeId: Number, technologyId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}employees/${employeeId}/technologies/${technologyId}`);
  }

  deleteProject(employeeId: Number, projectId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}employees/${employeeId}/projects/${projectId}`);
  }

  searchBy(esq: SearchQuery): Observable<any> {
    const params = new HttpParams().set('query', esq.query);
    return this.http
      .get(`${Constants.baseApiUrl}employees/search`, { params: params });
  }
}