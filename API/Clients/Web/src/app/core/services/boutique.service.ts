import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Boutique } from '../models/boutique.model';
import { MOCK_BOUTIQUES } from '../../../assets/mocks/boutique.mock';

@Injectable({
  providedIn: 'root'
})

export class BoutiqueService {
  private mockData = MOCK_BOUTIQUES;

  constructor(private http: HttpClient) {}

  getData() {
    // This will be proxied to https://localhost:500/api/data
    return this.http.get('/api/data');
  }

  getBoutiques(): Observable<Boutique[]> {
    return new Observable<Boutique[]>(subscriber => {
      subscriber.next(this.mockData);
      subscriber.complete();
    });
  }

  getBoutiqueById(id: string): Observable<Boutique> {
    const boutique = this.mockData.find(b => b.id === id);
    return new Observable<Boutique>(subscriber => {
      subscriber.next(boutique);
      subscriber.complete();
    });
  }
} 