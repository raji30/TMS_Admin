/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RatesheetlistComponent } from './ratesheetlist.component';

describe('RatesheetlistComponent', () => {
  let component: RatesheetlistComponent;
  let fixture: ComponentFixture<RatesheetlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RatesheetlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RatesheetlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
