import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// import { HttpClient, HttpHeaders } from '@angular/common/http';

import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryDataService } from './in-memory-data/in-memory-data.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { RedelisteAuswahlModule } from './redeliste-auswahl/redeliste-auswahl.module';
import { RedelisteTeilnahmeComponent } from './redeliste-teilnahme/redeliste-teilnahme.component';
import { MessagesComponent } from './messages/messages.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    RedelisteAuswahlModule,
    HttpClientModule,
    HttpClientInMemoryWebApiModule.forRoot(
      InMemoryDataService, { dataEncapsulation: false }
    )
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    RedelisteTeilnahmeComponent,
    MessagesComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }