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

  private teilnehmersUrl = 'api/teilnehmers';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }

  getTeilnehmers(): Observable<Teilnehmer[]> {
    // const teilnehmers = of(TEILNEHMER);
    return this.http.get<Teilnehmer[]>(this.teilnehmersUrl)
      .pipe(
        tap(_ => this.log('fetched teilnehmers')),
        catchError(this.handleError<Teilnehmer[]>('getTeilnehmers', []))
      );
    // this.messageService.add('TeilnehmerService: fetched teilnehmer');
    // return teilnehmers;
  }

  /** GET teilnehmer by id. Return `undefined` when id not found */
  getHeroNo404<Data>(id: number): Observable<Teilnehmer> {
    const url = `${this.teilnehmersUrl}/?id=${id}`;
    return this.http.get<Teilnehmer[]>(url)
      .pipe(
        map(teilnehmers => teilnehmers[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? 'fetched' : 'did not find';
          this.log(`${outcome} teilnehmer id=${id}`);
        }),
        catchError(this.handleError<Teilnehmer>(`getTeilnehmer id=${id}`))
      );
  }

  /** GET hero by id. Will 404 if id not found */
  getTeilnehmer(id: number): Observable<Teilnehmer> {
    const url = `${this.teilnehmersUrl}/${id}`;
    return this.http.get<Teilnehmer>(url).pipe(
      tap(_ => this.log(`fetched teilnehmer id=${id}`)),
      catchError(this.handleError<Teilnehmer>(`getTeilnehmer id=${id}`))
    );
  }

  /* GET teilnehmers whose name contains search term */
  searchTeilnehmers(term: string): Observable<Teilnehmer[]> {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Teilnehmer[]>(`${this.teilnehmersUrl}/?name=${term}`).pipe(
      tap(x => x.length ?
         this.log(`found teilnehmers matching "${term}"`) :
         this.log(`no teilnehmers matching "${term}"`)),
      catchError(this.handleError<Teilnehmer[]>('searchTeilnehmers', []))
    );
  }

  //////// Save methods //////////

  /** POST: add a new teilnehmer to the server */
  addTeilnehmer(teilnehmer: Teilnehmer): Observable<Teilnehmer> {
    return this.http.post<Teilnehmer>(this.teilnehmersUrl, teilnehmer, this.httpOptions).pipe(
      tap((newTeilnehmer: Teilnehmer) => this.log(`added teilnehmer w/ id=${newTeilnehmer.id}`)),
      catchError(this.handleError<Teilnehmer>('addTeilnehmer'))
    );
  }

  /** DELETE: delete the teilnehmer from the server */
  deleteTeilnehmer(id: number): Observable<Teilnehmer> {
    const url = `${this.teilnehmersUrl}/${id}`;

    return this.http.delete<Teilnehmer>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted teilnehmer id=${id}`)),
      catchError(this.handleError<Teilnehmer>('deleteTeilnehmer'))
    );
  }

  updateTeilnehmer(teilnehmer: Teilnehmer): Observable<any> {
    return this.http.put(this.teilnehmersUrl, teilnehmer, this.httpOptions).pipe(
      tap(_ => this.log(`updated teilnehmer id=${teilnehmer.id}`)),
      catchError(this.handleError<any>('updateTeilnehmer'))
    );
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
  
  /** Log a TeilnehmerService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`TeilnehmerService: ${message}`);
  }
}
