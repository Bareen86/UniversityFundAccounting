import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IconsModule } from "@progress/kendo-angular-icons";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { LabelModule } from '@progress/kendo-angular-label';
import { IValidationResult } from '../../Models/IValidationResult';
import { ICreateCorpuse } from '../../Models/command/corpuse/ICreateCorpuse';
import { CorpuseService } from '../../Services/corpuse.service';

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
  corpuseFloorsNumber = 1;
  validation : IValidationResult = {
    isFail: false
  };
  corpuseNameError = '';
  corpuseAddressError = '';
  corpuseFloorsNumberError = '';

  constructor(private corpuseService : CorpuseService) {}

  private ResetParemetres() : void {
    this.corpuseName = '';
    this.corpuseAddress = '';
    this.corpuseFloorsNumber = 1;
    this.corpuseNameError = '';
    this.corpuseAddressError = '';
    this.corpuseFloorsNumberError = ''
  }

  private ResetErrors() : void {
    this.corpuseNameError = '';
    this.corpuseAddressError = '';
    this.corpuseFloorsNumberError = ''
  }

  public AddCorpuse(corpuse : ICreateCorpuse) : void {
    this.corpuseService.CreateCorpuse(corpuse).subscribe(
      (data) => {
        this.validation = data;
        this.newCorpuseEvent.emit(true);
        this.ResetParemetres();
      },
      (err) => {
        this.corpuseService.handleError(err);
        this.validation = err.error.validationResult;
        this.ResetErrors();
        switch(this.validation.error) {
          case 'Имя корпуса не должно быть пустым':
            this.corpuseNameError = 'Имя корпуса не должно быть пустым';
            break;
          case 'Такой корпус уже есть в указанном адресе':
            this.corpuseAddressError = 'Такой корпус уже есть в указанном адресе';
            break;
          case 'Адресс не должен быть пустым':
            this.corpuseAddressError = 'Адресс не должен быть пустым';
            break;
          case 'В корпусе должен быть минимум 1 этаж':
            this.corpuseFloorsNumberError = 'В корпусе должен быть минимум 1 этаж';
            break;
        }
      }
    )
  }
}
