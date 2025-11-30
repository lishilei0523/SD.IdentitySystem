import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelateRoleComponent } from './relate-role.component';

describe('RelateRoleComponent', () => {
  let component: RelateRoleComponent;
  let fixture: ComponentFixture<RelateRoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RelateRoleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RelateRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
