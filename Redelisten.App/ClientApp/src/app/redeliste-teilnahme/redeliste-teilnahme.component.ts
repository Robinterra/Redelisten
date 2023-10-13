import { Component } from "@angular/core";
import { TEILNEHMER } from "../mock-teilnehmer";
import { Teilnehmer } from "../teilnehmer";

@Component({
    selector: 'app-redeliste-teilnahme',
    templateUrl: './redeliste-teilnahme.component.html',
    styleUrls: ['./redeliste-teilnahme.component.css']
})

export class RedelisteTeilnahmeComponent {
    redelisteTeilnehmer = TEILNEHMER;
    selectedTeilnehmer?: Teilnehmer;

    onSelect(teilnehmer: Teilnehmer): void {
        this.selectedTeilnehmer = teilnehmer;
    }

}