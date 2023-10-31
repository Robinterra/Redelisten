import { Component, OnInit } from "@angular/core";

import { Participant } from "../participant";
import { ParticipantService } from "../participant.service";
// import { PARTICIPANTS } from "../mock-participants";

@Component({
    selector: 'app-participation',
    templateUrl: './participation.component.html',
    styleUrls: ['./participation.component.css']
})

export class ParticipationComponent implements OnInit {

    // participants: Participant[] = PARTICIPANTS;

    participants: Participant[] = [];

    constructor(private participantService: ParticipantService) { }

    ngOnInit(): void {
        // this.getTeilnehmers();
        this.getParticipants();
    }

    getParticipants(): void {
      // this.participants = this.participantService.getParticipants();
      // this.participantService.getParticipants().subscribe(participants => this.participants = this.participants);
      this.participantService.getAllParticipants().then((participantsList: Participant[]) => {
        this.participants = participantsList;
        alert(this.participants[0].userID + ' ' + this.participants[0].name);
      });
    }

    add(name: string): void {
      name = name.trim();
      if (!name) { return; }

      /*
      this.participantService.addParticipant({ name } as Participant)
        .subscribe(participant => {
          this.participants.push(participant);
        });
      */
    }

    delete(participant: Participant): void {
      // this.teilnehmers = this.teilnehmers.filter(t => t !== teilnehmer);
      // this.teilnehmerService.deleteTeilnehmer(teilnehmer.id).subscribe();
    }

    /*
    getTeilnehmers(): void {
        this.teilnehmerService.getTeilnehmers().subscribe(teilnehmers => this.teilnehmers = teilnehmers);
    }
    */

}
