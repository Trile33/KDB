import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import {Technology} from '../models/technology.model';
import { Constants } from 'app/shared/constants';
import { SearchQuery } from 'app/models/search-query';

@Injectable()
export class TechnologyApiService {

  constructor(private http: HttpClient) { }

  getAll = (): Observable<any> => {
    return this.http
      .get(`${Constants.baseApiUrl}technologies`);
  }

  getBy = (technologyId: Number): Observable<any> =>{
    return this.http
      .get(`${Constants.baseApiUrl}technologies/${technologyId}`);
  }

  deleteTechnology(technologyId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}technologies/${technologyId}`);
  }

  saveTechnology(technology: Technology){
    return this.http
      .post(`${Constants.baseApiUrl}technologies`, technology);
  }

  updateTechnology(technologyId: Number, technology: Technology){
    return this.http
      .put(`${Constants.baseApiUrl}technologies/${technologyId}`, technology);
  }

  saveTechnologyNewProject(technologyId: Number, projectId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}technologies/${technologyId}/projects/${projectId}`, null);
  }

  saveTechnologyNewEmployee(technologyId: Number, employeeId: Number){
    return this.http
      .put(`${Constants.baseApiUrl}technologies/${technologyId}/employees/${employeeId}`, null);
  }

  deleteProject(technologyId: Number, projectId: Number){
    return this.http
      .delete(`${Constants.baseApiUrl}technologies/${technologyId}/projects/${projectId}`);
  }

  deleteEmployee(technologyId: Number, employeeId: Number){
    return this.http
    .delete(`${Constants.baseApiUrl}technologies/${technologyId}/employees/${employeeId}`);
  }

  searchBy(tsq: SearchQuery): Observable<any> {
    const params = new HttpParams().set('query', tsq.query);
    return this.http
      .get(`${Constants.baseApiUrl}technologies/search`, { params: params });
  }
}
