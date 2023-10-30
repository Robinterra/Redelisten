import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';

import { Participant } from './participant';
import { PARTICIPANTS } from './mock-participants';
// import { MessageService } from './message.service';

@Injectable({ providedIn: 'root' })
export class ParticipantService {

  // constructor(private messageService: MessageService) { }
  // constructor(private participantService: ParticipantService) { }

  getParticipants(): Observable<Participant[]> {
    const participants = of(PARTICIPANTS);
    // this.messageService.add('ParticipantService: fetched participant');
    return participants;
  }

  getParticipant(id: number): Observable<Participant> {
    // For now, assume that a hero with the specified `id` always exists.
    // Error handling will be added in the next step of the tutorial.
    const participant = PARTICIPANTS.find(h => h.id === id)!;
    // this.messageService.add(`ParticipantService: fetched participant id=${id}`);
    return of(participant);
  }

  /** POST: add a new teilnehmer to the server */
  addTeilnehmer(participant: Participant) {


    // const participan = new Participant(1, 'Max Mustermann');
    /*
    return this.http.post<Teilnehmer>(this.teilnehmersUrl, teilnehmer, this.httpOptions).pipe(
      tap((newTeilnehmer: Teilnehmer) => this.log(`added teilnehmer w/ id=${newTeilnehmer.id}`)),
      catchError(this.handleError<Teilnehmer>('addTeilnehmer'))
    );
    */
  }

}
