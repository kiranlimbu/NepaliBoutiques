import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  LucideAngularModule,
  Instagram,
  Facebook,
  Mail,
  Home,
  Search,
  User,
  Settings,
  Bell,
  MessageCircle,
  Share2,
  Phone,
} from 'lucide-angular';
import { IconComponent } from './icon.component';

@NgModule({
  declarations: [IconComponent],
  imports: [
    CommonModule,
    LucideAngularModule.pick({
      Instagram,
      Facebook,
      Mail,
      Home,
      Search,
      User,
      Settings,
      Bell,
      MessageCircle,
      Share2,
      Phone,
    }),
  ],
  exports: [IconComponent],
})
export class IconModule {}
