import { Component, OnInit, OnDestroy } from '@angular/core';
import { FEATURED_BOUTIQUES } from '../../../../../assets/mocks/featured-boutiques.mock';
import { BoutiqueFeatured } from '../../../models/boutique.model';

@Component({
  selector: 'app-featured-boutiques',
  templateUrl: './featured-boutiques.component.html',
  styleUrls: ['./featured-boutiques.component.css']
})
export class FeaturedBoutiquesComponent implements OnInit, OnDestroy {
  featuredBoutiques: BoutiqueFeatured[] = [];
  isPaused = false;
  private scrollInterval: any;

  ngOnInit(): void {
    this.featuredBoutiques = FEATURED_BOUTIQUES;
    this.startAutoScroll();
  }

  ngOnDestroy(): void {
    if (this.scrollInterval) {
      clearInterval(this.scrollInterval);
    }
  }

  private startAutoScroll(): void {
    this.scrollInterval = setInterval(() => {
      if (!this.isPaused) {
        const container = document.querySelector('.carousel-track');
        if (container) {
          container.scrollLeft += 1;
          if (container.scrollLeft >= (container.scrollWidth - container.clientWidth)) {
            container.scrollLeft = 0;
          }
        }
      }
    }, 30);
  }

  onMouseEnter(): void {
    this.isPaused = true;
  }

  onMouseLeave(): void {
    this.isPaused = false;
  }
}
