import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HardwareStatusGridComponent } from './hardware-status-grid.component';

describe('HardwareStatusGridComponent', () => {
  let component: HardwareStatusGridComponent;
  let fixture: ComponentFixture<HardwareStatusGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HardwareStatusGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HardwareStatusGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
