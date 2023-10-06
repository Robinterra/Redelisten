import { Component } from '@angular/core';

import { Redeliste } from '../redeliste';

@Component({
  selector: 'app-liste-beitreten-form',
  templateUrl: './liste-beitreten-form.component.html',
  styleUrls: ['./liste-beitreten-form.component.css']
})

export class ListeBeitretenFormComponent {

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