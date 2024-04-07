import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { IValidationResult } from "../Models/IValidationResult";
import { ICreateCorpuse } from "../Models/command/corpuse/ICreateCorpuse";
import { GetCorpusesQueryResult } from "../Models/result-request/corpuse/GetCorpusesQueryResult";
import { IUpdateCorpuse } from "../Models/command/corpuse/IUpdateCorpuse";

@Injectable({providedIn: "root"})
export class CorpuseService {
    private baseUrl = 'http://localhost:5000/api/corpuses'

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
    
    GetCorpuses() : Observable<GetCorpusesQueryResult> {
      return this.http.get<GetCorpusesQueryResult>(this.baseUrl)
    }

    public CreateCorpuse(corpuse : ICreateCorpuse) : Observable<IValidationResult> {
      return this.http.post<IValidationResult>(this.baseUrl, corpuse);
    }

    public DeleteCorpuse(id : number) : Observable<any> {
      return this.http.delete(`${this.baseUrl}/${id}`)
    }
  
    public UpdateCorpuse(id : number, corpuseData : IUpdateCorpuse) : Observable<IValidationResult> {
      return this.http.put<IValidationResult>(`${this.baseUrl}/${id}`, corpuseData);
    }
}