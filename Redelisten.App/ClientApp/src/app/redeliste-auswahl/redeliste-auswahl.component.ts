import { Component } from '@angular/core';
import { Teilnehmer } from '../teilnehmer';
// import { FormControl } from '@angular/forms';
// import { FormsModule } from '@angular/forms';
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

  // name = new FormControl('');
  model = new Teilnehmer(11111, 'Hugo');
  
  joinList() {

  }

  submitted = false;

  onSubmit() {
    this.submitted = true;    
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