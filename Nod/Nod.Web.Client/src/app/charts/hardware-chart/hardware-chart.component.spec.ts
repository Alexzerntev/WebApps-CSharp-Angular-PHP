import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HardwareChartComponent } from './hardware-chart.component';

describe('HardwareChartComponent', () => {
  let component: HardwareChartComponent;
  let fixture: ComponentFixture<HardwareChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HardwareChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HardwareChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
