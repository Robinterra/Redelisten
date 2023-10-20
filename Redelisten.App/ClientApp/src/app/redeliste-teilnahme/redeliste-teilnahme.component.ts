import { Component, OnInit } from "@angular/core";

import { Teilnehmer } from "../teilnehmer";
import { TeilnehmerService } from "../teilnehmer.service";

@Component({
    selector: 'app-redeliste-teilnahme',
    templateUrl: './redeliste-teilnahme.component.html',
    styleUrls: ['./redeliste-teilnahme.component.css']
})

export class RedelisteTeilnahmeComponent implements OnInit {
    teilnehmers: Teilnehmer[] = [];
    
    constructor(private teilnehmerService: TeilnehmerService) { }

    ngOnInit(): void {
        this.getTeilnehmers();
    }

    getTeilnehmers(): void {
        this.teilnehmerService.getTeilnehmers().subscribe(teilnehmers => this.teilnehmers = teilnehmers);
    }

}