import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DOIntakeComponent } from './dointake.component';

describe('DOIntakeComponent', () => {
  let component: DOIntakeComponent;
  let fixture: ComponentFixture<DOIntakeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DOIntakeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DOIntakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
