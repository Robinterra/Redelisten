import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { Participant } from './participant';
// import { PARTICIPANTS } from './mock-participants';
// import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})

export class ParticipantService {

  // url = 'http://localhost:3000/locations';

  url = 'https://eu-central-1.aws.data.mongodb-api.com/app/data-ecyvd/endpoint/data/v1';

  constructor(private http: HttpClient) { }

  getAllParticipants(): Observable<Participant[]> {
    return this.http.get<Participant[]>(this.url)
    .pipe(
      tap(data => console.log('Returned data:', data)),
      catchError(error => {
        console.error('Error fetching participants', error);
        return throwError(() => error);
      })
    );
  }

  getParticipantById(id: number): Observable<Participant> {
    return this.http.get<Participant>(`${this.url}/${id}`);
  }

  addParticipant(participant: Participant): Observable<Participant> {
    return this.http.post<Participant>(this.url, participant);
  }
  // url = 'http://localhost:3000/participants';

  /*
  async getAllParticipants(): Promise<Participant[]> {
    const data = await fetch(this.url);
    return await data.json() ?? [];
  }

  async getParticipantById(id: number): Promise<Participant | undefined> {
    const data = await fetch(`${this.url}/${id}`);
    return await data.json() ?? {};
  }
  */
  // constructor() { }
  // constructor(private messageService: MessageService) { }
  // constructor(private messageService: MessageService) { }
  // constructor(private participantService: ParticipantService) { }


  /*
  getParticipants(): Observable<Participant[]> {
    const participants = of(PARTICIPANTS);
    // this.messageService.add('ParticipantService: fetched participant');
    return participants;
  }
  */

  /*
  getParticipant(id: number): Observable<Participant> {
    // For now, assume that a hero with the specified `id` always exists.
    // Error handling will be added in the next step of the tutorial.
    const participant = PARTICIPANTS.find(h => h.id === id)!;
    // this.messageService.add(`ParticipantService: fetched participant id=${id}`);
    return of(participant);
  }
  */

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
