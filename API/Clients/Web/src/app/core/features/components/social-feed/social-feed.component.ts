import { Component, OnInit, OnDestroy } from '@angular/core';
import { SocialPost, SOCIAL_POSTS } from '../../../../../assets/mocks/social-feed.mock';

@Component({
  selector: 'app-social-feed',
  templateUrl: './social-feed.component.html',
  styleUrls: ['./social-feed.component.css']
})
export class SocialFeedComponent implements OnInit, OnDestroy {
  socialPosts: SocialPost[] = [];
  currentPostIndex = 0;
  private intervalId: any;

  ngOnInit(): void {
    this.socialPosts = SOCIAL_POSTS;
    this.startRotation();
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  private startRotation(): void {
    this.intervalId = setInterval(() => {
      this.currentPostIndex = (this.currentPostIndex + 1) % this.socialPosts.length;
    }, 5000); // Change post every 5 seconds
  }

  get currentPost(): SocialPost {
    return this.socialPosts[this.currentPostIndex];
  }
}
