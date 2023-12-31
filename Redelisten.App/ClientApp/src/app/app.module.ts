import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';

// import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';

// import { InMemoryDataService } from './in-memory-data.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { SelectionModule } from './selection/selection.module';
import { ParticipationComponent } from './participation/participation.component';

// import { MessagesComponent } from './messages/messages.component';
// import { HomeComponent } from './home/home.component';
// import { CounterComponent } from './counter/counter.component';
// import { FetchDataComponent } from './fetch-data/fetch-data.component';

@NgModule({
  imports: [
    // BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    SelectionModule,

    /*
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
    */
    // The HttpClientInMemoryWebApiModule module intercepts HTTP requests
    // and returns simulated server responses.
    // Remove it when a real server is ready to receive requests.

  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    ParticipationComponent,
    // MessagesComponent,
    // HomeComponent,
    // CounterComponent,
    // FetchDataComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
