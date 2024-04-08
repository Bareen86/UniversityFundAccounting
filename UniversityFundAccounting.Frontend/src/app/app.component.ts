import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { ICorpuse } from './Models/ICorpuse';
import { GetCorpusesQueryResult } from './Models/result-request/corpuse/GetCorpusesQueryResult';
import { CorpuseService } from './Services/corpuse.service';
import { AddCorpuseRowComponent } from './components/add-corpuse-row/add-corpuse-row.component';
import { CorpuseComponent } from './components/corpuse/corpuse.component';
import { AudienceService } from './Services/audience.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NgIf, CorpuseComponent, ButtonsModule, AddCorpuseRowComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [CorpuseService, AudienceService]
})
export class AppComponent implements OnInit{
  
  showForm : boolean = false;

  corpuses : ICorpuse[] = [];
  constructor(private corpuseService : CorpuseService) {}

  ngOnInit(): void {
    this.corpuseService.GetCorpuses().subscribe((result : GetCorpusesQueryResult) => {
      this.corpuses = result.objResult;
    })
  }

  public OnButtonClick() : void {
    this.showForm = !this.showForm;
  }

  DeleteCorpuseByIdHandler(id : number) {
    this.corpuses = this.corpuses.filter(corpuse => corpuse.id != id)
  }

  NewCorpuseEventHandler(occured : boolean) {
    this.corpuseService.GetCorpuses().subscribe((corpuses) => {
      this.corpuses = corpuses.objResult;
    })
  }
}
