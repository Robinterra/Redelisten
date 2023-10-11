import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedelisteAuswahlComponent } from './redeliste-auswahl/redeliste-auswahl.component';

const routes: Routes = [
  { path: 'redelisteAuswahl', component: RedelisteAuswahlComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }