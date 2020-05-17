/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListcityComponent } from './listcity.component';

describe('ListcityComponent', () => {
  let component: ListcityComponent;
  let fixture: ComponentFixture<ListcityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListcityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListcityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
