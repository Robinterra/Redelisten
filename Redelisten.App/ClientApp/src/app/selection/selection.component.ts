import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from "@angular/router";

import { forbiddenListNameValidator } from '../shared/forbidden-list-name.directive';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css']
})

export class SelectionComponent implements OnInit {

  selectionForm!: FormGroup;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.selectionForm = new FormGroup({
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

  navigateToParticipation() {
    this.router.navigate(['/participation']);
  }

  updateProfile() {
    this.selectionForm.patchValue({
      listName: 'ListeEins',
    });
  }

}
