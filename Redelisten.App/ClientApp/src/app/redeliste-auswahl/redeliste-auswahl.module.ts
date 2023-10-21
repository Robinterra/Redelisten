import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RedelisteAuswahlRoutingModule } from './redeliste-auswahl-routing.module';
// import { RedelisteAuswahlFormModule } from '../redeliste-auswahl-form/redeliste-auswahl-form.module';
import { RedelisteAuswahlComponent } from './redeliste-auswahl.component';



@NgModule({
    imports: [
        CommonModule,
        // RedelisteAuswahlFormModule,
        RedelisteAuswahlRoutingModule,
        FormsModule,
    ],
    declarations: [
        RedelisteAuswahlComponent,
    ],
    exports: [
        RedelisteAuswahlComponent,
    ]
})
export class RedelisteAuswahlModule { }
