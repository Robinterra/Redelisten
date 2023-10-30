import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SelectionRoutingModule } from './selection-routing.module';
import { SelectionComponent } from './selection.component';

@NgModule({
    imports: [
        CommonModule,
        SelectionRoutingModule,
        ReactiveFormsModule,
    ],
    declarations: [
        SelectionComponent,
    ],
    exports: [
      SelectionComponent,
    ]
})
export class SelectionModule { }
