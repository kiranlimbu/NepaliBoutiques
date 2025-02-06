import { Component, OnInit, OnDestroy } from '@angular/core';
import { BoutiqueService } from '../../../core/services/boutique.service';
import { BoutiqueFeatured } from '../../../core/models/boutique.model';
import { SocialPost } from '../../../core/models/social-post.model';
import { SocialFeedService } from '../../../core/services/social-feed.service';

@Component({
  selector: 'app-featured-boutiques',
  templateUrl: './featured-boutiques.component.html',
  styleUrls: ['./featured-boutiques.component.css'],
})
export class FeaturedBoutiquesComponent implements OnInit, OnDestroy {
  featuredBoutiques: BoutiqueFeatured[] = [];
  isPaused = false;
  private scrollInterval: any;
  // Social Feed
  socialPosts: SocialPost[] = [];
  currentPostIndex = 0;
  private intervalId: any;

  constructor(
    private boutiqueService: BoutiqueService,
    private socialFeedService: SocialFeedService
  ) {}

  ngOnInit(): void {
    this.boutiqueService.getFeaturedBoutiques().subscribe((boutiques) => {
      this.featuredBoutiques = boutiques;
      this.startAutoScroll();
    });
    // Social Feed
    this.socialFeedService.getPosts().subscribe((posts) => {
      this.socialPosts = posts;
      this.startRotation();
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

  // Social Feed
  private startRotation(): void {
    this.intervalId = setInterval(() => {
      this.currentPostIndex =
        (this.currentPostIndex + 1) % this.socialPosts.length;
    }, 5000); // Change post every 5 seconds
  }

  get currentPost(): SocialPost {
    return this.socialPosts[this.currentPostIndex];
  }
}
