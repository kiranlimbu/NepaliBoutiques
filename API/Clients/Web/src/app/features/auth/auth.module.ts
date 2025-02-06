import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { InstagramCallbackComponent } from './instagram-callback/instagram-callback.component';

@NgModule({
  declarations: [InstagramCallbackComponent],
  imports: [CommonModule, AuthRoutingModule],
})
export class AuthModule {}
