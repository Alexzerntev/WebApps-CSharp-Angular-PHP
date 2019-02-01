import { TestBed, inject } from '@angular/core/testing';

import { SpinGameService } from './spin-game.service';

describe('SpinGameService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SpinGameService]
    });
  });

  it('should be created', inject([SpinGameService], (service: SpinGameService) => {
    expect(service).toBeTruthy();
  }));
});
