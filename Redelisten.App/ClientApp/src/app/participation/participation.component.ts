import { Component, OnInit } from "@angular/core";

import { Participant } from "../participant";
import { ParticipantService } from "../participant.service";
import { PARTICIPANTS } from "../mock-participants";

@Component({
    selector: 'app-participation',
    templateUrl: './participation.component.html',
    styleUrls: ['./participation.component.css']
})

export class ParticipationComponent implements OnInit {
    participants: Participant[] = PARTICIPANTS;

    constructor(private participantService: ParticipantService) { }

    ngOnInit(): void {
        // this.getTeilnehmers();
    }

    getParticipants(): void {
      this.participantService.getParticipants().subscribe(participants => this.participants = this.participants);
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
