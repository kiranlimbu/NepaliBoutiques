import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InstagramGraphApiService } from '../../../core/services/Instagram-graphAPI.service';

@Component({
  selector: 'app-instagram-callback',
  templateUrl: './instagram-callback.component.html',
  styleUrls: ['./instagram-callback.component.css'],
})
export class InstagramCallbackComponent implements OnInit {
  loading = true;
  error: string | null = null;
  success = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private instagramService: InstagramGraphApiService
  ) {}

  ngOnInit() {
    // Get the code from URL query params
    this.route.queryParams.subscribe((params) => {
      const code = params['code'];
      if (code) {
        this.handleInstagramCode(code);
      } else {
        this.error = 'No authorization code received from Instagram';
        this.loading = false;
      }
    });
  }

  private handleInstagramCode(code: string) {
    this.instagramService.getLongLivedToken(code).subscribe({
      next: (response) => {
        this.success = true;
        this.loading = false;
        // Store the token or handle the successful response
        console.log('Token received:', response);
      },
      error: (err) => {
        this.error = 'Failed to exchange code for token: ' + err.message;
        this.loading = false;
      },
    });
  }

  retryAuthorization() {
    // Redirect back to Instagram authorization
    window.location.href =
      'https://api.instagram.com/oauth/authorize' +
      '?client_id=' +
      'YOUR_CLIENT_ID' +
      '&redirect_uri=' +
      encodeURIComponent('http://localhost:4200/auth/instagram/callback') +
      '&scope=user_profile,user_media' +
      '&response_type=code';
  }
}
