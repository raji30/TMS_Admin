/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BaseratelistComponent } from './baseratelist.component';

describe('BaseratelistComponent', () => {
  let component: BaseratelistComponent;
  let fixture: ComponentFixture<BaseratelistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseratelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseratelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
