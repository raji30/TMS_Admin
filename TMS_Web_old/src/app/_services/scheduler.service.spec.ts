/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SchedulerService } from './scheduler.service';

describe('Service: Scheduler', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SchedulerService]
    });
  });

  it('should ...', inject([SchedulerService], (service: SchedulerService) => {
    expect(service).toBeTruthy();
  }));
});
