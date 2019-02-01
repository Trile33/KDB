import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { TechnologyApiService } from './technology-api.service';
import { ProjectApiService} from './project-api.service';
import { EmployeeApiService } from 'app/api-services/employee-api.service';

@NgModule({
    imports: [
        HttpClientModule
    ],
    providers: [
        TechnologyApiService,
        ProjectApiService,
        EmployeeApiService
    ]
})
export class ApiServicesModule {
}