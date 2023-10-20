import { Component } from '@angular/core';
import { TeilnehmerService } from '../teilnehmer.service';
import { Router, RouterModule } from '@angular/router';



@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent {
  
  

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