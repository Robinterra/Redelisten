import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';


import { RedelisteAuswahlComponent } from './redeliste-auswahl/redeliste-auswahl.component';
import { RedelisteTeilnahmeComponent } from './redeliste-teilnahme/redeliste-teilnahme.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    RedelisteAuswahlComponent,
    RedelisteTeilnahmeComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
