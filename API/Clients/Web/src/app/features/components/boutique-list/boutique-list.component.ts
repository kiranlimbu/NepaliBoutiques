import { Component, OnInit } from '@angular/core';
import { BoutiqueService } from '../../../core/services/boutique.service';
import { Boutique } from '../../../core/models/boutique.model';

@Component({
  selector: 'app-boutique-list',
  templateUrl: './boutique-list.component.html',
  styleUrls: ['./boutique-list.component.css'],
})
export class BoutiqueListComponent implements OnInit {
  boutiques: Boutique[] = [];

  constructor(private boutiqueService: BoutiqueService) {}

  ngOnInit(): void {
    this.boutiqueService.getBoutiquesWithInventory().subscribe((data) => {
      this.boutiques = data;
    });
  }
}
