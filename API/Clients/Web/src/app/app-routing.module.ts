import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutPageComponent } from './core/features/pages/layout-page/layout-page.component';

const routes: Routes = [
  { path: '', component: LayoutPageComponent }, // landing page
  { path: '**', redirectTo: '' } // redirect to landing page if route not found
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
