import { TestBed, inject } from '@angular/core/testing';

import { UserHistoryService } from './user-history.service';

describe('UserHistoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserHistoryService]
    });
  });

  it('should be created', inject([UserHistoryService], (service: UserHistoryService) => {
    expect(service).toBeTruthy();
  }));
});
