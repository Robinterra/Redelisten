import { Component } from '@angular/core';
// import { NgModule } from '@angular/core';

import { Teilnehmer } from '../teilnehmer';

@Component({
  selector: 'app-redeliste-auswahl-form',
  templateUrl: './redeliste-auswahl-form.component.html',
  styleUrls: ['./redeliste-auswahl-form.component.css']
})

export class RedelisteAuswahlFormComponent {

  model = new Teilnehmer(11111, 'Torben');
  submitted = false;

  onSubmit() {
    this.submitted = true;
  }


  // const myTeilnehmer = new Teilnehmer(2222, 'Klausi');

  newTeilnehmer() {
    this.model = new Teilnehmer(11111, 'Torben');
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value; // Dr. IQ
  }

}