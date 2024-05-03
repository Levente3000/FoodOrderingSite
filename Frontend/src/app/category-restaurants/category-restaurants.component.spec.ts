import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryRestaurantsComponent } from './category-restaurants.component';

describe('CategoryRestaurantsComponent', () => {
  let component: CategoryRestaurantsComponent;
  let fixture: ComponentFixture<CategoryRestaurantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoryRestaurantsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CategoryRestaurantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
