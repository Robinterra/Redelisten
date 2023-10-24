import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from "@angular/router";

import { forbiddenListNameValidator } from '../shared/forbidden-list-name.directive';

@Component({
  selector: 'app-redeliste-auswahl',
  templateUrl: './redeliste-auswahl.component.html',
  styleUrls: ['./redeliste-auswahl.component.css']
})

export class RedelisteAuswahlComponent implements OnInit {

  redelisteAuswahlForm!: FormGroup;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.redelisteAuswahlForm = new FormGroup({
      listName: new FormControl('', [
        Validators.required,
        Validators.minLength(4),
        forbiddenListNameValidator(/bobby/i)
        // Unbekannter Listenname. Check implementieren
      ])
    });
  }

  /*
  onSubmit() {
    if (this.redelisteAuswahlForm.valid) {
      console.log(this.redelisteAuswahlForm.value);
      // add your form submission logic here
    }
  }
  */

  navigateToRedelisteTeilnahme() {
    this.router.navigate(['/redeliste-teilnahme']);
  }

  updateProfile() {
    this.redelisteAuswahlForm.patchValue({
      listName: 'Nancy',
    });
  }

}