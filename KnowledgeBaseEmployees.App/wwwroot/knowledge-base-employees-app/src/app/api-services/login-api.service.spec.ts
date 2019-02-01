import { TestBed, inject } from '@angular/core/testing';
import { LoginApiService } from 'app/api-services/login-api.service';


describe('LoginApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoginApiService]
    });
  });

  it('should ...', inject([LoginApiService], (service: LoginApiService) => {
    expect(service).toBeTruthy();
  }));
});