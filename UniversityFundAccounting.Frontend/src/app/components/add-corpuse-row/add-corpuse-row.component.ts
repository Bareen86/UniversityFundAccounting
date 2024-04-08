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
  
  constructor(private corpuseService : CorpuseService) {}

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

  private IsThereEmptyInputs() : boolean {
    let validationResult = false;
    if (!this.corpuseName){
      this.corpuseNameError = 'Имя корпуса не должно быть пустым'
      validationResult = true;
    } else this.corpuseNameError = '';
    if (!this.corpuseAddress){
      this.corpuseAddressError = 'Адрес не должен быть пустым'
      validationResult = true;
    } else this.corpuseAddressError = '';
    if (!this.corpuseFloorsNumber){
      this.corpuseFloorsNumberError = 'В корпусе должен быть минимум 1 этаж'
      validationResult = true;
    } else this.corpuseFloorsNumberError = '';
    return validationResult;
  }

  public CreateCorpuse(corpuse : ICreateCorpuse) : void {
    let thereIsEmptyInputs : boolean = this.IsThereEmptyInputs();
    if (thereIsEmptyInputs === false){
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
            case 'Адрес не должен быть пустым':
              this.corpuseAddressError = 'Адрес не должен быть пустым';
              break;
            case 'В корпусе должен быть минимум 1 этаж':
              this.corpuseFloorsNumberError = 'В корпусе должен быть минимум 1 этаж';
              break;
          }
        }
      )
    }
  }
}
