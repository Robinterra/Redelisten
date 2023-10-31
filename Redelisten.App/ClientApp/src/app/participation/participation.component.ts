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
      this.participantService.getAllParticipants().subscribe(participants => this.participants = participants);
      // this.participants = this.participantService.getAllParticipants();
      // this.participantService.getParticipants().subscribe(participants => this.participants = this.participants);
      /*
      this.participantService.getAllParticipants().then((participantsList: Participant[]) => {
        this.participants = participantsList;
      });
      */
    }

    add(name: string): void {
      name = name.trim();
      if (!name) {
        return;
      }
      this.participantService.addParticipant({ name } as Participant).subscribe(participant => {
        this.participants.push(participant);
      });

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
