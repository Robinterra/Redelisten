import { Component } from '@angular/core';
import { Teilnehmer } from '../teilnehmer';

import { FormControl, FormGroup } from '@angular/forms';
// import { NgModule } from '@angular/core';

// import { RedelisteAuswahlFormComponent } from '../redeliste-auswahl-form/redeliste-auswahl-form.component';

// import { TeilnehmerService } from '../teilnehmer.service';
// import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent {

  redelisteAuswahlForm = new FormGroup({ 
    listName: new FormControl(''),
  });

  // TODO: Work on updating the model

  nam = new FormControl('Han');

  updateName() {
    // this.nam.patchValue(this.redelisteAuswahlForm.value.name);
    this.nam.setValue('Nancy');
    // this.nam.setValue('Nancy');
  }

  model = new Teilnehmer(11111, 'Hugo');
  
  joinList() {
    this.updateName();
  }

  submitted = false;

  onSubmit() {
    this.updateName();
    this.submitted = true;
    //     console.warn(this.redelisteAuswahlForm.value);    
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }


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