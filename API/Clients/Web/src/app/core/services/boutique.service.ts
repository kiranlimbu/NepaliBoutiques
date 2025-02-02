import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { Boutique, BoutiqueFeatured } from '../models/boutique.model';
import { MOCK_BOUTIQUES } from '../../../assets/mocks/boutique.mock';
import { BOUTIQUE_INVENTORIES } from '../../../assets/mocks/boutique-inventories.mock';
import { FEATURED_BOUTIQUES } from '../../../assets/mocks/featured-boutiques.mock';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BoutiqueService {
  private apiUrl = `${environment.apiUrl}/boutique`;
  private mockData = MOCK_BOUTIQUES;
  private useMockData = true;

  constructor(private http: HttpClient) {}

  /**
   * Fetches all boutiques from the API
   * @returns Observable<Boutique[]> List of boutiques
   */
  getBoutiquesWithInventory(): Observable<Boutique[]> {
    if (this.useMockData) {
      const boutiquesWithInventory = MOCK_BOUTIQUES.map((boutique) => ({
        ...boutique,
        inventory:
          BOUTIQUE_INVENTORIES.find((inv) => inv.boutiqueId === boutique.id)
            ?.items || [],
      }));
      return of(boutiquesWithInventory);
    }

    return this.http.get<Boutique[]>(this.apiUrl);
  }

  /**
   * Fetches a specific boutique by ID
   * @param id The boutique ID
   * @returns Observable<Boutique> The requested boutique
   */
  getBoutiqueById(id: string): Observable<Boutique> {
    if (this.useMockData) {
      const boutique = this.mockData.find((b) => b.id === id);
      if (!boutique) {
        return throwError(() => new Error('Boutique not found'));
      }
      return of(boutique);
    }

    return this.http.get<Boutique>(`${this.apiUrl}/${id}`);
  }

  /**
   * Creates a new boutique
   * @param boutique The boutique data to create
   * @returns Observable<Boutique> The created boutique
   */
  createBoutique(boutique: Omit<Boutique, 'id'>): Observable<Boutique> {
    if (this.useMockData) {
      const newBoutique = {
        ...boutique,
        id: Math.random().toString(36).substr(2, 9),
      };
      this.mockData.push(newBoutique);
      return of(newBoutique);
    }

    return this.http.post<Boutique>(this.apiUrl, boutique);
  }

  /**
   * Updates an existing boutique
   * @param id The boutique ID
   * @param boutique The updated boutique data
   * @returns Observable<Boutique> The updated boutique
   */
  updateBoutique(
    id: string,
    boutique: Partial<Boutique>
  ): Observable<Boutique> {
    if (this.useMockData) {
      const index = this.mockData.findIndex((b) => b.id === id);
      if (index !== -1) {
        this.mockData[index] = { ...this.mockData[index], ...boutique };
        return of(this.mockData[index]);
      }
      return throwError(() => new Error('Boutique not found'));
    }

    return this.http.put<Boutique>(`${this.apiUrl}/${id}`, boutique);
  }

  /**
   * Deletes a boutique
   * @param id The boutique ID to delete
   * @returns Observable<void>
   */
  deleteBoutique(id: string): Observable<void> {
    if (this.useMockData) {
      const index = this.mockData.findIndex((b) => b.id === id);
      if (index !== -1) {
        this.mockData.splice(index, 1);
        return of(void 0);
      }
      return throwError(() => new Error('Boutique not found'));
    }

    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  /**
   * Fetches featured boutiques
   * @returns Observable<BoutiqueFeatured[]> List of featured boutiques
   */
  getFeaturedBoutiques(): Observable<BoutiqueFeatured[]> {
    if (this.useMockData) {
      return of(FEATURED_BOUTIQUES);
    }

    return this.http.get<BoutiqueFeatured[]>(`${this.apiUrl}/featured`);
  }
}
