/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserpermissionService } from './userpermission.service';

describe('Service: Userpermission', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserpermissionService]
    });
  });

  it('should ...', inject([UserpermissionService], (service: UserpermissionService) => {
    expect(service).toBeTruthy();
  }));
});
