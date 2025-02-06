import { Component } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { InstagramGraphApiService } from '../../../core/services/Instagram-graphAPI.service';

@Component({
  selector: 'app-layout-page',
  templateUrl: './layout-page.component.html',
  styleUrl: './layout-page.component.css',
})
export class LayoutPageComponent {
  title = 'Nepali Boutique Directory';

  constructor(private instagramService: InstagramGraphApiService) {}

  connectInstagram() {
    const instagramAuthUrl =
      'https://api.instagram.com/oauth/authorize' +
      '?client_id=' +
      environment.instagramAppId +
      '&redirect_uri=' +
      encodeURIComponent('http://localhost:4200/auth/instagram/callback') +
      '&scope=instagram_basic,instagram_graph_user_profile,instagram_graph_user_media' +
      '&response_type=code';

    window.location.href = instagramAuthUrl;
  }

  getLongLivedToken() {
    const shortLivedToken = 'your_short_lived_token'; // Your short-lived token

    this.instagramService.getLongLivedToken(shortLivedToken).subscribe({
      next: (response) => {
        // Store token on server instead of localStorage
        this.instagramService
          .storeLongLivedToken(response.access_token)
          .subscribe({
            next: () =>
              console.log('Token stored successfully', response.access_token),
            error: (err) => console.error('Error storing token:', err),
          });
      },
      error: (error) => {
        console.error('Error getting long-lived token:', error);
      },
    });
  }

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value;
    // TODO: Implement search functionality
    console.log('Searching for:', searchTerm);
  }

  searchHashtag(hashtag: string) {
    this.instagramService.searchHashtag(hashtag).subscribe({
      next: (result) => {
        console.log(`Found ${result.media_count} posts for #${result.name}`);
        if (result.media?.data) {
          console.log('Recent posts:', result.media.data);
        }
      },
      error: (error) => {
        console.error('Error searching hashtag:', error);
      },
    });
  }
}
