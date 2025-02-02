import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SocialPost } from '../models/social-post.model';
import { SOCIAL_POSTS } from '../../../assets/mocks/social-feed.mock';

@Injectable({
  providedIn: 'root',
})
export class SocialFeedService {
  private apiUrl = `${environment.apiUrl}/socialfeed`;
  private useMockData = true;

  constructor(private http: HttpClient) {}

  getPosts(): Observable<SocialPost[]> {
    if (this.useMockData) {
      // Map mock data to match SocialPost interface
      const mappedPosts: SocialPost[] = SOCIAL_POSTS.map((post) => ({
        id: post.id, // Convert string id to number
        userName: post.userName,
        comment: post.comment,
        boutiqueName: post.boutiqueName,
      }));
      return of(mappedPosts);
    }

    return this.http.get<SocialPost[]>(this.apiUrl);
  }

  getPost(id: number): Observable<SocialPost> {
    if (this.useMockData) {
      const post = SOCIAL_POSTS.find((p) => p.id === id);
      if (!post) {
        throw new Error('Post not found');
      }
      return of(post);
    }

    return this.http.get<SocialPost>(`${this.apiUrl}/${id}`);
  }
}
