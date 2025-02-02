import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoutiqueListComponent } from './components/boutique-list/boutique-list.component';
import { LayoutPageComponent } from './pages/layout-page/layout-page.component';
import { RouterModule } from '@angular/router';
import { FeaturedBoutiquesComponent } from './components/featured-boutiques/featured-boutiques.component';
import { SocialFeedComponent } from './components/social-feed/social-feed.component';
import { HeroSectionComponent } from './components/hero-section/hero-section.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    SocialFeedComponent,
    HeroSectionComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule, // Add SharedModule import
  ],
  exports: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    HeroSectionComponent,
    SocialFeedComponent,
  ],
})
export class FeaturesModule {}
