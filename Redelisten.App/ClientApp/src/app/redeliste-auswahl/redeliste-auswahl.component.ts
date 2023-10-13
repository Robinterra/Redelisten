import { Component } from '@angular/core';

@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent {
  
  // model = new RedelisteTeilnahme('Klaus');
  model = new RedelisteTeilnahme('');
  
  submitted = false;

  onSubmit() { this.submitted = true; }

  newRedeliste() {
    this.model = new RedelisteTeilnahme('Peter');
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }

}