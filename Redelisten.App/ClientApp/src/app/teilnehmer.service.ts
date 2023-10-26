import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Teilnehmer } from './teilnehmer';
// import { TEILNEHMER } from './mock-teilnehmer';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root',
})

export class TeilnehmerService {

  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }

  private teilnehmerUrl = 'api/teilnehmers';  // URL to web api

  getTeilnehmers(): Observable<Teilnehmer[]> {
    // const teilnehmers = of(TEILNEHMER);

    return this.http.get<Teilnehmer[]>(this.teilnehmerUrl)
      .pipe(
        catchError(this.handleError<Teilnehmer[]>('getTeilnehmers', []))
      );
    // this.messageService.add('TeilnehmerService: fetched teilnehmer');
    // return teilnehmers;
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 *
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** GET hero by id. Will 404 if id not found */
  getTeilnehmer(id: number): Observable<Teilnehmer> {
    const url = `${this.teilnehmerUrl}/${id}`;
    return this.http.get<Teilnehmer>(url).pipe(
      tap(_ => this.log(`fetched teilnehmer id=${id}`)),
      catchError(this.handleError<Teilnehmer>(`getTeilnehmer id=${id}`))
    );
  }

  updateTeilnehmer(teilnehmer: Teilnehmer): Observable<any> {
    return this.http.put(this.teilnehmerUrl, teilnehmer, this.httpOptions).pipe(
      tap(_ => this.log(`updated teilnehmer id=${teilnehmer.id}`)),
      catchError(this.handleError<any>('updateTeilnehmer'))
    );
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  /** Log a TeilnehmerService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`TeilnehmerService: ${message}`);
  }

}
