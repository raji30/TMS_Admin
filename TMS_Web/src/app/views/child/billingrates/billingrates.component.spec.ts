import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BillingratesComponent } from './billingrates.component';

describe('BillingratesComponent', () => {
  let component: BillingratesComponent;
  let fixture: ComponentFixture<BillingratesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BillingratesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BillingratesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
