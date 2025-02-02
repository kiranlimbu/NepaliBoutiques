import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturedBoutiquesComponent } from './featured-boutiques.component';

describe('FeaturedBoutiquesComponent', () => {
  let component: FeaturedBoutiquesComponent;
  let fixture: ComponentFixture<FeaturedBoutiquesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FeaturedBoutiquesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FeaturedBoutiquesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
