import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

// import { RedelisteAuswahlFormComponent } from '../redeliste-auswahl-form/redeliste-auswahl-form.component';
import { SelectionComponent } from './selection.component';
// import {FeatureModule} from '../feature/feature.module';

const ingestRoutes: Routes = [
  {path: 'selection', component: SelectionComponent},
];

@NgModule({
  imports: [
    RouterModule.forChild(ingestRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class SelectionRoutingModule {
}
