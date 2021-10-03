import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelateAuthorityComponent } from './relate-authority.component';

describe('RelateAuthorityComponent', () => {
  let component: RelateAuthorityComponent;
  let fixture: ComponentFixture<RelateAuthorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RelateAuthorityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RelateAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
