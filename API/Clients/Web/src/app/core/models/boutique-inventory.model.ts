export interface BoutiqueInventory {
  boutiqueId: string;
  items: InventoryItem[];
}

export interface InventoryItem {
  id: string;
  imageUrl: string;
  name: string;
  price: number;
  // TODO: Add caption and timestamp and omit price
}
