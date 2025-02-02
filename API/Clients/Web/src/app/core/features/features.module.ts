import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoutiqueListComponent } from './components/boutique-list/boutique-list.component';
import { LayoutPageComponent } from './pages/layout-page/layout-page.component';
import { RouterModule } from '@angular/router';
import { FeaturedBoutiquesComponent } from './components/featured-boutiques/featured-boutiques.component';
import { SocialFeedComponent } from './components/social-feed/social-feed.component';
import { HeaderComponent } from './components/header/header.component';
import { HeroSectionComponent } from './components/hero-section/hero-section.component';
import { FooterComponent } from './components/footer/footer.component';

@NgModule({
  declarations: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    SocialFeedComponent,
    HeaderComponent,
    HeroSectionComponent,
    FooterComponent,
  ],
  imports: [
    CommonModule,
    RouterModule, // for routing within features module
  ],
  exports: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    HeaderComponent,
    HeroSectionComponent,
    SocialFeedComponent,
    FooterComponent,
  ],
})
export class FeaturesModule {}
