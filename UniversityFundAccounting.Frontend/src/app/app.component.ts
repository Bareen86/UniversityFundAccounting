import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CorpuseService } from './Services/corpuse.service';
import { NgFor, NgIf } from '@angular/common';
import { ICorpuse } from './Models/ICorpuse';
import { CorpuseComponent } from './components/corpuse/corpuse.component';
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { AddCorpuseRowComponent } from './components/add-corpuse-row/add-corpuse-row.component';
import { GetAudiencesQueryResult } from './Models/result-request/audience/GetAudiencesQueryResult';
import { GetCorpusesQueryResult } from './Models/result-request/corpuse/GetCorpusesQueryResult';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, CorpuseComponent, ButtonsModule, AddCorpuseRowComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [CorpuseService]
})
export class AppComponent implements OnInit{
  
  showForm : boolean = false;

  corpuses : ICorpuse[] = [];
  constructor(private corpuseService : CorpuseService) {}

  ngOnInit(): void {
    this.corpuseService.fetchCorpuses().subscribe((result : GetCorpusesQueryResult) => {
      this.corpuses = result.objResult;
    })
  }

  public OnButtonClick() : void {
    this.showForm = !this.showForm;
  }

  deleteCorpuseByIdHandler(id : number) {
    this.corpuses = this.corpuses.filter(corpuse => corpuse.id != id)
  }

  newCorpuseEventHandler(occured : boolean) {
    this.corpuseService.fetchCorpuses().subscribe((corpuses) => {
      this.corpuses = corpuses.objResult;
    })
  }
}
