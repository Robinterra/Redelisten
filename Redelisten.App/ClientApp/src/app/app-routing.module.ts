import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RedelisteAuswahlComponent } from './redeliste-auswahl/redeliste-auswahl.component';
import { RedelisteTeilnahmeComponent } from './redeliste-teilnahme/redeliste-teilnahme.component';

const routes: Routes = [
    { path: 'redeliste-auswahl', component: RedelisteAuswahlComponent },
    { path: 'redeliste-teilnahme', component: RedelisteTeilnahmeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }