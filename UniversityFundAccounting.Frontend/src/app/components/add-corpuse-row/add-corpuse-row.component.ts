import { Component, EventEmitter, Output } from '@angular/core';
import { InputsModule } from "@progress/kendo-angular-inputs";
import { FormsModule } from '@angular/forms';
import { IconsModule } from "@progress/kendo-angular-icons";
import { CorpuseService } from '../../Services/corpuse.service';
import { ICreateCorpuse } from '../../Models/command/corpuse/ICreateCorpuse';
import { IValidationResult } from '../../Models/IValidationResult';
import { LabelModule } from '@progress/kendo-angular-label';

@Component({
  selector: 'app-add-corpuse-row',
  standalone: true,
  imports: [InputsModule, FormsModule, IconsModule, LabelModule ],
  templateUrl: './add-corpuse-row.component.html',
  styleUrl: './add-corpuse-row.component.css',
  providers: [CorpuseService]
})
export class AddCorpuseRowComponent {
  @Output() newCorpuseEvent = new EventEmitter<boolean>();

  corpuseName = '';
  corpuseAddress = '';
  corpuseFloorsNumber : number = 0;
  validation : IValidationResult = {
    isFail: false
  };

  constructor(private corpuseService : CorpuseService) {}

  private resetParemetres() : void {
    this.corpuseName = ' ',
    this.corpuseAddress = ' ',
    this.corpuseFloorsNumber = 0
  }

  public addCorpuse(corpuse : ICreateCorpuse) : void {
    this.corpuseService.addCorpuse(corpuse).subscribe(
      (result : IValidationResult) => {
        this.validation.isFail = false;
        this.newCorpuseEvent.emit(true);
        this.resetParemetres();
      },
      (errormsg : string) => {
        this.validation.isFail = true;
      }
    )
  }
}
