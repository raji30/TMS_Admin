/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BrokerService } from './broker.service';

describe('Service: Broker', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BrokerService]
    });
  });

  it('should ...', inject([BrokerService], (service: BrokerService) => {
    expect(service).toBeTruthy();
  }));
});
