import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
// import { RedelisteAuswahlFormComponent } from './redeliste-auswahl/redeliste-auswahl-form/redeliste-auswahl-form.component';

@NgModule({
  imports: [
    BrowserModule,
    // ReactiveFormsModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    // RedelisteAuswahlFormComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
