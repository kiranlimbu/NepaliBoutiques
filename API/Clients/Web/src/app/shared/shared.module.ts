import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { IconModule } from '../core/icons/icon.module';

@NgModule({
  declarations: [HeaderComponent, FooterComponent],
  imports: [CommonModule, IconModule],
  exports: [HeaderComponent, FooterComponent],
})
export class SharedModule {}
