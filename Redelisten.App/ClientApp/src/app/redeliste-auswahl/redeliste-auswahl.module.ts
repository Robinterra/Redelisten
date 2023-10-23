import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { RedelisteAuswahlRoutingModule } from './redeliste-auswahl-routing.module';
import { RedelisteAuswahlComponent } from './redeliste-auswahl.component';

@NgModule({
    imports: [
        CommonModule,
        RedelisteAuswahlRoutingModule,
        ReactiveFormsModule,
    ],
    declarations: [
        RedelisteAuswahlComponent,
    ],
    exports: [
        RedelisteAuswahlComponent,
    ]
})  
export class RedelisteAuswahlModule { }
