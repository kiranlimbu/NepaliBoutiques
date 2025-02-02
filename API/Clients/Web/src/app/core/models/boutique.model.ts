import { InventoryItem } from './boutique-inventory.model';

export interface Boutique {
  id: string;
  name: string;
  profilePicture: string;
  followers: number;
  description: string;
  contact: string;
  instagramLink: string;
  inventory?: InventoryItem[];
}

/**
 * Represents a featured boutique with essential display information
 */
export interface BoutiqueFeatured {
  id: string;
  name: string;
  imageUrl: string;
  logoUrl: string;
}
