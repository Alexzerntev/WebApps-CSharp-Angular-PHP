import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GpsGridComponent } from './gps-grid.component';

describe('GpsGridComponent', () => {
  let component: GpsGridComponent;
  let fixture: ComponentFixture<GpsGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GpsGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GpsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
