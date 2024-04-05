import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ICorpuse } from "../Models/ICorpuse";

@Injectable({providedIn: "root"})
export class CorpuseService {
    private baseUrl = 'http://localhost:5000/api/corpuses'

    constructor(private http: HttpClient){

    }
    
    fetchCorpuses() : Observable<ICorpuse[]> {
      return this.http.get<ICorpuse[]>(`${this.baseUrl}`)
    }

    createCorpuse(){

    }

    deleteCorpuse(){

    }
}