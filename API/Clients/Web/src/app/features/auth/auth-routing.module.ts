import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstagramCallbackComponent } from './instagram-callback/instagram-callback.component';

const routes: Routes = [
  {
    path: 'instagram/callback',
    component: InstagramCallbackComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
