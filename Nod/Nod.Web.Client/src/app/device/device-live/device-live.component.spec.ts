import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceLiveComponent } from './device-live.component';

describe('DeviceLiveComponent', () => {
  let component: DeviceLiveComponent;
  let fixture: ComponentFixture<DeviceLiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceLiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceLiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
