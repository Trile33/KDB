import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';

import { ApiServicesModule } from './api-services/api-services.module';

import { AppComponent } from './app.component';
import { TechnologiesComponent } from './technologies/technologies.component';
import { TechnologyComponent } from './technologies/technology/technology.component';
import { ProjectComponent } from './projects/project/project.component';
import { ProjectsComponent } from './projects/projects.component';
import { EmployeeComponent } from './employees/employee/employee.component';
import { EmployeesComponent } from './employees/employees.component';
import { LocationStrategy, HashLocationStrategy, APP_BASE_HREF } from '@angular/common';
import { ProjectDetailComponent } from 'app/project-detail/project-detail.component';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';
import { HomeComponent } from './home/home.component';
import { TechnologyDetailComponent } from 'app/technology-detail/technology-detail.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginApiService } from './api-services/login-api.service';

const routes: Routes = [
  // basic routes
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'projects', component: ProjectsComponent },
  { path: 'projects/:id', component: ProjectDetailComponent },
  { path: 'employees', component: EmployeesComponent },
  { path: 'employees/:id', component: EmployeeDetailComponent },
  { path: 'technologies', component: TechnologiesComponent },
  { path: 'technologies/:id', component: TechnologyDetailComponent },
  { path: 'login', component: LoginComponent},
  { path: '**', redirectTo: 'home'}

];

@NgModule({
  declarations: [
    AppComponent,
    TechnologiesComponent,
    TechnologyComponent,
    TechnologyDetailComponent,
    ProjectComponent,
    ProjectsComponent,
    EmployeeComponent,
    EmployeesComponent,
    ProjectDetailComponent,
    EmployeeDetailComponent,
    HomeComponent,
    LoginComponent
  ],
  providers: [
    AuthGuard,
    LoginApiService
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    ApiServicesModule,
    RouterModule.forRoot(routes)
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
