import { TestBed, inject } from '@angular/core/testing';
import { EmployeeApiService } from 'app/api-services/employee-api.service';


describe('EmployeeApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EmployeeApiService]
    });
  });

  it('should ...', inject([EmployeeApiService], (service: EmployeeApiService) => {
    expect(service).toBeTruthy();
  }));
});