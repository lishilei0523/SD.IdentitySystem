import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetPrivateKeyComponent } from './reset-private-key.component';

describe('ResetPrivateKeyComponent', () => {
  let component: ResetPrivateKeyComponent;
  let fixture: ComponentFixture<ResetPrivateKeyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResetPrivateKeyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetPrivateKeyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
