import { Component } from '@angular/core';
import { Teilnehmer } from '../teilnehmer';

import { FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent {

  redelisteAuswahlForm = this.fb.group({
    listName: new FormControl(''),
    // listName: ['', Validators.required],
  });

  constructor(private fb: FormBuilder) { }

  // listName = new FormControl('');
  
  /*
  redelisteAuswahlForm = new FormGroup({
    listName: new FormControl(''),
  });
  */

  /*
  redelisteAuswahlForm: FormGroup;
  */
  /*
  redelisteAuswahlForm = this.fb.group({
    listName: ['', Validators.required],
    teilnehmerName: [''],
  });
  */

  // TODO: Work on updating the model
  // nam = new FormControl('Han');

  updateName() {
    // this.nam.patchValue(this.redelisteAuswahlForm.value.name);
    // this.nam.setValue('Nancy');
    // this.nam.setValue('Nancy');
  }

  model = new Teilnehmer(11111, 'Hugo');
  
  joinList() {
    // this.redelisteAuswahlForm.setValue({listName: 'Redeliste 1'});
    this.updateName();
  }

  submitted = false;

  onSubmit() {
    this.submitted = true; 
  }

  /*
  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }
  */

  // constructor(private fb: FormBuilder) { }

  // model = new RedelisteTeilnahme('Klaus');
  /*
  model = new RedelisteTeilnahme('');

  submitted = false;

  onSubmit() { this.submitted = true; }

  newRedeliste() {
    this.model = new RedelisteTeilnahme('Peter');
  }
  */

  /*
  btnClick= function () {
    this.router.navigateByUrl('/user');
  };

  submitted = false;


  onSubmit() {
    this.submitted = true;
    
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }
  */

}