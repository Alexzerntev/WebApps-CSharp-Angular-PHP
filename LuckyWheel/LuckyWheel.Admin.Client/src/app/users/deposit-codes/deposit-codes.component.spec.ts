import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositCodesComponent } from './deposit-codes.component';

describe('DepositCodesComponent', () => {
  let component: DepositCodesComponent;
  let fixture: ComponentFixture<DepositCodesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DepositCodesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepositCodesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
