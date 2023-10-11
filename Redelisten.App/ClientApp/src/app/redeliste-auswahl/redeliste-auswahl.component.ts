import { Component } from '@angular/core';

import { Redeliste } from '../redeliste';

@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent {

  model = new Redeliste('Klaus');

  submitted = false;

  onSubmit() { this.submitted = true; }

  newRedeliste() {
    this.model = new Redeliste('Peter');
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }

}