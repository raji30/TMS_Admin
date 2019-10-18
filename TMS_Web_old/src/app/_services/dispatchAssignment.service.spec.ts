/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DispatchAssignmentService } from './dispatchAssignment.service';

describe('Service: DispatchAssignment', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DispatchAssignmentService]
    });
  });

  it('should ...', inject([DispatchAssignmentService], (service: DispatchAssignmentService) => {
    expect(service).toBeTruthy();
  }));
});
