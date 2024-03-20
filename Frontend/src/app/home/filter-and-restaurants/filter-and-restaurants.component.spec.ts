import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterAndRestaurantsComponent } from './filter-and-restaurants.component';

describe('FilterAndRestaurantsComponent', () => {
  let component: FilterAndRestaurantsComponent;
  let fixture: ComponentFixture<FilterAndRestaurantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterAndRestaurantsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FilterAndRestaurantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
