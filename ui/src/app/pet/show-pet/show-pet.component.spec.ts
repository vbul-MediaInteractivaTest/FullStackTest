import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPetComponent } from './show-pet.component';

describe('ShowPetComponent', () => {
  let component: ShowPetComponent;
  let fixture: ComponentFixture<ShowPetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowPetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
