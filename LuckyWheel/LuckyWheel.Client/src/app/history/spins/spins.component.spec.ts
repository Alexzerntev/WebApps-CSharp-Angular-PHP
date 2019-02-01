import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpinsComponent } from './spins.component';

describe('SpinsComponent', () => {
  let component: SpinsComponent;
  let fixture: ComponentFixture<SpinsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpinsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpinsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
