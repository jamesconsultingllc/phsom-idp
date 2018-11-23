import { TestBed, inject } from '@angular/core/testing';

import { AuthenticatedRouteGuardService } from './authenticated-route-guard.service';

describe('AuthenticatedRouteGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenticatedRouteGuardService]
    });
  });

  it('should be created', inject([AuthenticatedRouteGuardService], (service: AuthenticatedRouteGuardService) => {
    expect(service).toBeTruthy();
  }));
});
