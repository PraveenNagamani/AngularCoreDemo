import { TestBed } from '@angular/core/testing';

import { PaymentDetail } from './payment-detail';

describe('PaymentDetail', () => {
  let service: PaymentDetail;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaymentDetail);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
