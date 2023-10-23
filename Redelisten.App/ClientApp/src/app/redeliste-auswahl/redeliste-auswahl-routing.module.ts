import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

// import { RedelisteAuswahlFormComponent } from '../redeliste-auswahl-form/redeliste-auswahl-form.component';
import { RedelisteAuswahlComponent } from './redeliste-auswahl.component';
// import {FeatureModule} from '../feature/feature.module';

const ingestRoutes: Routes = [
  {path: 'redeliste-auswahl', component: RedelisteAuswahlComponent},
  // {path: 'redeliste-auswahl/redeliste-auswahl-form', component: RedelisteAuswahlFormComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(ingestRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class RedelisteAuswahlRoutingModule {
}

