import { Component, OnInit, OnDestroy } from '@angular/core';
import { BoutiqueService } from '../../../core/services/boutique.service';
import { BoutiqueFeatured } from '../../../core/models/boutique.model';

@Component({
  selector: 'app-featured-boutiques',
  templateUrl: './featured-boutiques.component.html',
  styleUrls: ['./featured-boutiques.component.css'],
})
export class FeaturedBoutiquesComponent implements OnInit, OnDestroy {
  featuredBoutiques: BoutiqueFeatured[] = [];
  isPaused = false;
  private scrollInterval: any;

  constructor(private boutiqueService: BoutiqueService) {}

  ngOnInit(): void {
    this.boutiqueService.getFeaturedBoutiques().subscribe((boutiques) => {
      this.featuredBoutiques = boutiques;
      this.startAutoScroll();
    });
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
          if (
            container.scrollLeft >=
            container.scrollWidth - container.clientWidth
          ) {
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
