/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DeliveryOrderService } from './deliveryOrder.service';

describe('Service: DeliveryOrder', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DeliveryOrderService]
    });
  });

  it('should ...', inject([DeliveryOrderService], (service: DeliveryOrderService) => {
    expect(service).toBeTruthy();
  }));
});
