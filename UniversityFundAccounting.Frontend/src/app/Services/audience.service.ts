import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { GetAudiencesQueryResult } from '../Models/result-request/audience/GetAudiencesQueryResult';
import { ICreateAudience } from '../Models/command/audience/ICreateAudience';
import { IValidationResult } from '../Models/IValidationResult';
import { IUpdateAudience } from '../Models/command/audience/IUpdateAudience';

@Injectable({providedIn: 'root'})
export class AudienceService {
  private baseUrl = 'http://localhost:5000/api/audiences'

  constructor(private http: HttpClient){

  }

  public handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Bad request'));
  }

  GetCorpuseAudiences() : Observable<GetAudiencesQueryResult> {
    return this.http.get<GetAudiencesQueryResult>(this.baseUrl)
  }

  public CreateAudience(corpuse : ICreateAudience) : Observable<IValidationResult> {
    return this.http.post<IValidationResult>(this.baseUrl, corpuse);
  }

  public DeleteAudience(id : number) : Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`)
  }

  public UpdateAudience(id : number, audienceData : IUpdateAudience) : Observable<IValidationResult> {
    return this.http.put<IValidationResult>(`${this.baseUrl}/${id}`, audienceData);
  }
}
