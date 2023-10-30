import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SelectionComponent } from './selection/selection.component';
import { ParticipationComponent } from './participation/participation.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'selection' },
    { path: 'selection', component: SelectionComponent },
    { path: 'participation', component: ParticipationComponent},
    { path: '**', component: PagenotfoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule,
  ]
})
export class AppRoutingModule { }
