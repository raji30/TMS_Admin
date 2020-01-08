/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FileUploaderService } from './file-uploader.service';

describe('Service: FileUploader', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FileUploaderService]
    });
  });

  it('should ...', inject([FileUploaderService], (service: FileUploaderService) => {
    expect(service).toBeTruthy();
  }));
});
