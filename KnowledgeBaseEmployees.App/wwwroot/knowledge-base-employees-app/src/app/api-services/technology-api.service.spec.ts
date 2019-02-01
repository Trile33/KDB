import { TestBed, inject } from '@angular/core/testing';

import { TechnologyApiService } from './technology-api.service';

describe('TechnologyApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TechnologyApiService]
    });
  });

  it('should ...', inject([TechnologyApiService], (service: TechnologyApiService) => {
    expect(service).toBeTruthy();
  }));
});
