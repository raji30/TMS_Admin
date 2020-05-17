/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ItemlistComponent } from './itemlist.component';

describe('ItemlistComponent', () => {
  let component: ItemlistComponent;
  let fixture: ComponentFixture<ItemlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
