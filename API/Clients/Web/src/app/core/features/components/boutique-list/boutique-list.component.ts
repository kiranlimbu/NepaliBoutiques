import { Component, OnInit } from '@angular/core';
import { BoutiqueService } from '../../../services/boutique.service';
import { Boutique } from '../../../models/boutique.model';
import { BOUTIQUE_INVENTORIES } from '../../../../../assets/mocks/boutique-inventories.mock';
import { BoutiqueInventory } from '../../../models/boutique-inventory.model';

@Component({
  selector: 'app-boutique-list',
  templateUrl: './boutique-list.component.html',
  styleUrls: ['./boutique-list.component.css']
})
export class BoutiqueListComponent implements OnInit {
  boutiques: Boutique[] = [];
  inventories: BoutiqueInventory[] = BOUTIQUE_INVENTORIES;

  constructor(private boutiqueService: BoutiqueService) {}

  ngOnInit(): void {
    this.boutiqueService.getBoutiques().subscribe(data => {
      this.boutiques = data;
    });
  }

  getInventoryForBoutique(boutiqueId: string): BoutiqueInventory | undefined {
    return this.inventories.find(inv => inv.boutiqueId === boutiqueId);
  }
} 
