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

    /*
    getTeilnehmers(): void {
        this.teilnehmerService.getTeilnehmers().subscribe(teilnehmers => this.teilnehmers = teilnehmers);
    }
    */

}
