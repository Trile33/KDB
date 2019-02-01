import { TestBed, inject } from '@angular/core/testing';

import { ProjectApiService } from './project-api.service';

describe('ProjectApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProjectApiService]
    });
  });

  it('should ...', inject([ProjectApiService], (service: ProjectApiService) => {
    expect(service).toBeTruthy();
  }));
});
