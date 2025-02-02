export interface Boutique {
    id: string;
    name: string;
    profilePicture: string;
    followers: number;
    description: string;
    contact: string;
    instagramLink: string;
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