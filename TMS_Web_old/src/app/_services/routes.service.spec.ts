/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RoutesService } from './routes.service';

describe('Service: Routes', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RoutesService]
    });
  });

  it('should ...', inject([RoutesService], (service: RoutesService) => {
    expect(service).toBeTruthy();
  }));
});
