import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Response } from '@angular/http/src/static_response';
import { Headers } from '@angular/http/src/headers';
import { RequestOptions } from '@angular/http/src/base_request_options';
import { Observable } from 'rxjs/Observable';

import {Project} from '../models/project.model';
import { Constants } from 'app/shared/constants';
import { SearchQuery } from 'app/models/search-query.model';


@Injectable()
export class ProjectApiService {

  constructor(private http: HttpClient) { }

  getAll = (): Observable<any> => {
    return this.http
      .get(`${Constants.baseApiUrl}projects`);
  }

  getBy = (projectId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}projects/${projectId}`);
  }

  getProjectEmployees = (projectId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}projects/${projectId}/employees`);
  }

  getProjectTechnologies = (projectId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}projects/${projectId}/technologies`);
  }

  saveProject(project: Project){
    return this.http
      .post(`${Constants.baseApiUrl}projects`, project);
  }

  updateProject(projectId: Number, project: Project){
    return this.http
      .put(`${Constants.baseApiUrl}projects/${projectId}`, project);
  }

  saveProjectNewEmployee(projectId: Number, employeeId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}projects/${projectId}/employees/${employeeId}`, null);
  }

  saveProjectNewTechnology(projectId: Number, technologyId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}projects/${projectId}/technologies/${technologyId}`, null);
  }

  deleteProject(projectId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}projects/${projectId}`);
  }

  deleteEmployee(projectId: Number, employeeId: Number){
    return this.http
    .delete(`${Constants.baseApiUrl}projects/${projectId}/employees/${employeeId}`);
  }

  deleteTechnology(projectId: Number, technologyId: Number){
    return this.http
    .delete(`${Constants.baseApiUrl}projects/${projectId}/technologies/${technologyId}`);
  }

  searchBy(psq: SearchQuery): Observable<any> {
    const params = new HttpParams().set('query', psq.query);
    return this.http
      .get(`${Constants.baseApiUrl}projects/search`, { params: params });
  }
}