import { TestBed } from '@angular/core/testing';

import { ApexchartservicesService } from './apexchartservices.service';

describe('ApexchartservicesService', () => {
  let service: ApexchartservicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApexchartservicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
