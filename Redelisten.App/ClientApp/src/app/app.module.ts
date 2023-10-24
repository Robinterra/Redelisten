import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { RedelisteAuswahlModule } from './redeliste-auswahl/redeliste-auswahl.module';
import { RedelisteTeilnahmeComponent } from './redeliste-teilnahme/redeliste-teilnahme.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    RedelisteAuswahlModule,
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    RedelisteTeilnahmeComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }