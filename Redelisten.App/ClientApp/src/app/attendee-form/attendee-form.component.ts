import { Component } from '@angular/core';

import { Attendee } from '../attendee';

@Component({
  selector: 'app-attendee-form',
  templateUrl: './attendee-form.component.html',
  styleUrls: ['./attendee-form.component.css']
})

export class AttendeeFormComponent {

  adminStatuses = [true, false];
  
  model = new Attendee('18', 'Dr. IQ', true);

  submitted = false;

  onSubmit() { this.submitted = true; }

  newAttendee() {
    this.model = new Attendee('42', 'test', true);
  }

  showFormControls(form: any) {
    return form && form.controls.name &&
    form.controls.name.value;
  }

}