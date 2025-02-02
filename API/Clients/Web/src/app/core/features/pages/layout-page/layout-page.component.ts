import { Component } from '@angular/core';

@Component({
  selector: 'app-layout-page',
  templateUrl: './layout-page.component.html',
  styleUrl: './layout-page.component.css'
})

export class LayoutPageComponent {
  title = 'Nepali Boutique Directory';

  onSearch(event: Event): void {
    const searchTerm = (event.target as HTMLInputElement).value;
    // TODO: Implement search functionality
    console.log('Searching for:', searchTerm);
  }
}
