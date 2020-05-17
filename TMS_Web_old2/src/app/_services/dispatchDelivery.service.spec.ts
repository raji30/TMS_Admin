/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DispatchDeliveryService } from './dispatchDelivery.service';

describe('Service: DispatchDelivery', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DispatchDeliveryService]
    });
  });

  it('should ...', inject([DispatchDeliveryService], (service: DispatchDeliveryService) => {
    expect(service).toBeTruthy();
  }));
});
