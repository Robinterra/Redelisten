import { Component } from '@angular/core';

import { Redeliste } from '../redeliste';

@Component({
  selector: 'app-redeliste-erstellen-form',
  templateUrl: './redeliste-erstellen-form.component.html',
  styleUrls: ['./redeliste-erstellen-form.component.css']
})

export class RedelisteErstellenFormComponent {

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