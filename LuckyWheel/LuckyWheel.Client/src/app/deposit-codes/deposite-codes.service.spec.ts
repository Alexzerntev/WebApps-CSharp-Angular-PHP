import { TestBed, inject } from '@angular/core/testing';

import { DepositeCodesService } from './deposite-codes.service';

describe('DepositeCodesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DepositeCodesService]
    });
  });

  it('should be created', inject([DepositeCodesService], (service: DepositeCodesService) => {
    expect(service).toBeTruthy();
  }));
});
