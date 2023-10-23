import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
// import { RedelisteAuswahlComponent } from './redeliste-auswahl/redeliste-auswahl.component';
import { RedelisteAuswahlModule } from './redeliste-auswahl/redeliste-auswahl.module';

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
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }