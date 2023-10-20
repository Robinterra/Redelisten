import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';

import { Teilnehmer } from './teilnehmer';
import { TEILNEHMER } from './mock-teilnehmer';
import { MessageService } from './message.service';

@Injectable({ providedIn: 'root' })
export class TeilnehmerService {

  constructor(private messageService: MessageService) { }

  getTeilnehmers(): Observable<Teilnehmer[]> {
    const teilnehmers = of(TEILNEHMER);
    this.messageService.add('TeilnehmerService: fetched teilnehmer');
    return teilnehmers;
  }

  getTeilnehmer(id: number): Observable<Teilnehmer> {
    // For now, assume that a hero with the specified `id` always exists.
    // Error handling will be added in the next step of the tutorial.
    const teilnehmer = TEILNEHMER.find(h => h.id === id)!;
    this.messageService.add(`TeilnehmerService: fetched teilnehmer id=${id}`);
    return of(teilnehmer);
  }
}
