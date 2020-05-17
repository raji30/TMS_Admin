/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListdriverComponent } from './listdriver.component';

describe('ListdriverComponent', () => {
  let component: ListdriverComponent;
  let fixture: ComponentFixture<ListdriverComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListdriverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListdriverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
