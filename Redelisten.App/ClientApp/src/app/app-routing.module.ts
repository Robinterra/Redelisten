import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedelisteAuswahlComponent } from './redeliste-auswahl/redeliste-auswahl.component';
import { RedelisteTeilnahmeComponent } from './redeliste-teilnahme/redeliste-teilnahme.component';

const routes: Routes = [
  { path: 'redelisteAuswahl', component: RedelisteAuswahlComponent },
  { path: 'redelisteTeilnahme', component: RedelisteTeilnahmeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }