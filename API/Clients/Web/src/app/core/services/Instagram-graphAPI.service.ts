import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

interface HashtagSearchResult {
  id: string;
  name: string;
  media_count: number;
  media?: {
    data: Array<{
      id: string;
      caption?: string;
      media_url: string;
      permalink: string;
      timestamp: string;
    }>;
  };
}

@Injectable({
  providedIn: 'root',
})
export class InstagramGraphApiService {
  private apiUrl = `${environment.apiUrl}/instagram`;

  constructor(private http: HttpClient) {}

  /**
   * Exchange short-lived token for a long-lived access token
   * @param shortLivedToken The short-lived access token from Instagram
   */
  getLongLivedToken(shortLivedToken: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/token`, { token: shortLivedToken });
  }

  /**
   * Refresh the long-lived access token
   * @param longLivedToken The existing long-lived access token
   */
  refreshLongLivedToken(longLivedToken: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/token/refresh`, {
      token: longLivedToken,
    });
  }

  storeLongLivedToken(token: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/store-token`, { token });
  }

  /**
   * Search for hashtags and get their media count
   * @param query The hashtag to search for (without #)
   */
  searchHashtag(query: string): Observable<HashtagSearchResult> {
    return this.http.get<HashtagSearchResult>(`${this.apiUrl}/hashtag/search`, {
      params: { query },
    });
  }
}
