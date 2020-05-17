/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FiledownloadService } from './filedownload.service';

describe('Service: Filedownload', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FiledownloadService]
    });
  });

  it('should ...', inject([FiledownloadService], (service: FiledownloadService) => {
    expect(service).toBeTruthy();
  }));
});
