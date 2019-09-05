/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CarrierService } from './carrier.service';

describe('Service: Carrier', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CarrierService]
    });
  });

  it('should ...', inject([CarrierService], (service: CarrierService) => {
    expect(service).toBeTruthy();
  }));
});
