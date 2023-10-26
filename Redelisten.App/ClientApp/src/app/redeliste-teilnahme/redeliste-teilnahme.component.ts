import { Component, OnInit } from "@angular/core";

import { Teilnehmer } from "../teilnehmer";
import { TeilnehmerService } from "../teilnehmer.service";
import { MessageService } from "../message.service";

@Component({
    selector: 'app-redeliste-teilnahme',
    templateUrl: './redeliste-teilnahme.component.html',
    styleUrls: ['./redeliste-teilnahme.component.css']
})

export class RedelisteTeilnahmeComponent implements OnInit {
    
    teilnehmers: Teilnehmer[] = [];

    constructor(private teilnehmerService: TeilnehmerService, private messageService: MessageService) { }

    // TODO: Work on the list .css and then the services that allow the list to be updated.
    ngOnInit(): void {
        this.getTeilnehmers();
    }

    getTeilnehmers(): void {
        this.teilnehmerService.getTeilnehmers().subscribe(teilnehmers => this.teilnehmers = teilnehmers);
    }

    add(name: string): void {
        name = name.trim();
        if (!name) { return; }
        this.teilnehmerService.addTeilnehmer({ name } as Teilnehmer)
          .subscribe(teilnehmer => {
            this.teilnehmers.push(teilnehmer);
          });
      }
    
      delete(teilnehmer: Teilnehmer): void {
        this.teilnehmers = this.teilnehmers.filter(t => t !== teilnehmer);
        this.teilnehmerService.deleteTeilnehmer(teilnehmer.id).subscribe();
      }

}