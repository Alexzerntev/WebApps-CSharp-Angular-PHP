import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositCodeComponent } from './deposit-code.component';

describe('DepositCodeComponent', () => {
  let component: DepositCodeComponent;
  let fixture: ComponentFixture<DepositCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DepositCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepositCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
