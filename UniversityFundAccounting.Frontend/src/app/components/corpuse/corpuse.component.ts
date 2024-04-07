import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ICorpuse } from '../../Models/ICorpuse';
import { NgFor } from '@angular/common';
import { InputsModule } from "@progress/kendo-angular-inputs";
import { FormsModule } from '@angular/forms';
import { IconsModule } from "@progress/kendo-angular-icons";
import { CorpuseService } from '../../Services/corpuse.service';

@Component({
  selector: 'app-corpuse',
  standalone: true,
  imports: [NgFor, InputsModule, FormsModule, IconsModule],
  templateUrl: './corpuse.component.html',
  styleUrl: './corpuse.component.css',
  providers: [CorpuseService]
})
export class CorpuseComponent {

  constructor(private corpuseService : CorpuseService) {}

  readOnly : boolean = true;
  @Input() corpuse !: ICorpuse;
  @Output() deleteCorpuseEvent = new EventEmitter<number>();
  
  public editCorpuse() : void {
    console.log("edit state");
  }

  public deleteCorpuse(id : number) : void {
    this.corpuseService.deleteCorpuse(id).subscribe();
    this.deleteCorpuseEvent.emit(id);
  }
}
