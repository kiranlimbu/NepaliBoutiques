import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoutiqueListComponent } from './components/boutique-list/boutique-list.component';
import { LayoutPageComponent } from './pages/layout-page/layout-page.component';
import { RouterModule } from '@angular/router';
import { FeaturedBoutiquesComponent } from './components/featured-boutiques/featured-boutiques.component';
import { HeroSectionComponent } from './components/hero-section/hero-section.component';
import { SharedModule } from '../shared/shared.module';
import { IconModule } from '../core/icons/icon.module';

@NgModule({
  declarations: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    HeroSectionComponent,
  ],
  imports: [CommonModule, RouterModule, SharedModule, IconModule],
  exports: [
    BoutiqueListComponent,
    LayoutPageComponent,
    FeaturedBoutiquesComponent,
    HeroSectionComponent,
  ],
})
export class FeaturesModule {}
