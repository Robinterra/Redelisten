import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from "@angular/router";

import { forbiddenListNameValidator } from '../shared/forbidden-list-name.directive';
import { Observable } from 'rxjs';
import { HttpClient } from  '@angular/common/http';
import { Injectable } from  '@angular/core';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css']
})
@Injectable({
  providedIn:  'root'
  })

export class SelectionComponent implements OnInit {

  selectionForm!: FormGroup;

  constructor(private router: Router, private http: HttpClient) {}

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
    alert(this.selectionForm.value.listName);
    //this.router.navigate(['/participation']);
    // POST: /api/redeliste/create
    // JSON: { name: listName }
    let redeliste = new CreateRedeliste();
    redeliste.name = this.selectionForm.value.listName;
    this.createRedeliste(redeliste);

  }

  createRedeliste(redeliste: CreateRedeliste) {
    // POST: /api/redeliste/create
    this.http.post('/api/redeliste/create', redeliste).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );

  }

  updateProfile() {
    this.selectionForm.patchValue({
      listName: 'ListeEins',
    });
  }

}
class CreateRedeliste {
  name!: string;
}