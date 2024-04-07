import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ICorpuse } from '../../Models/ICorpuse';
import { NgFor } from '@angular/common';
import { InputsModule } from "@progress/kendo-angular-inputs";
import { FormsModule } from '@angular/forms';
import { IconsModule } from "@progress/kendo-angular-icons";
import { CorpuseService } from '../../Services/corpuse.service';
import { IUpdateCorpuse } from '../../Models/command/corpuse/IUpdateCorpuse';
import { IValidationResult } from '../../Models/IValidationResult';
import { LabelModule } from '@progress/kendo-angular-label';

@Component({
  selector: 'app-corpuse',
  standalone: true,
  imports: [NgFor, InputsModule, FormsModule, IconsModule, LabelModule],
  templateUrl: './corpuse.component.html',
  styleUrl: './corpuse.component.css',
  providers: [CorpuseService]
})
export class CorpuseComponent {

  constructor(private corpuseService : CorpuseService) {}

  editState : boolean = false;
  readOnlyState : boolean = true;
  @Input() corpuse !: ICorpuse;
  @Output() deleteCorpuseEvent = new EventEmitter<number>();
  validation : IValidationResult = {
    isFail: false
  };
  corpuseNameError = '';
  corpuseAddressError = '';
  corpuseFloorsNumberError = '';
  corpuseNameCopy = '';
  corpuseAddressCopy = '';
  corpuseFloorsNumberCopy = 1;
  
  public UpdateCorpuse( id : number, corpuseData : IUpdateCorpuse ) : void {
    if (this.CorpuseParamsAreEqual() === false){
      this.corpuseService.UpdateCorpuse(id, corpuseData).subscribe(
        (data) => {
          this.editState = !this.editState;
          this.readOnlyState = !this.readOnlyState;
          this.ResetErrorParemetres();
        },
        (err) => {
          this.corpuseService.handleError(err);
          this.validation = err.error.validationResult;
          this.ResetErrorParemetres();
          switch(this.validation.error) {
            case 'Имя корпуса не должно быть пустым':
              this.corpuseNameError = 'Имя корпуса не должно быть пустым';
              break;
            case 'Адресс не должен быть пустым':
              this.corpuseAddressError = 'Адресс не должен быть пустым';
              break;
            case 'Такой корпус уже есть в указанном адресе':
              this.corpuseAddressError = 'Такой корпус уже есть в указанном адресе'
              break;
            case 'В корпусе должен быть минимум 1 этаж':
              this.corpuseFloorsNumberError = 'В корпусе должен быть минимум 1 этаж';
              break;
          }
        }
      )
    }
    else {
      this.editState = !this.editState;
      this.readOnlyState = !this.readOnlyState;
    }
  }

  private CorpuseParamsAreEqual() : boolean {
    if ( this.corpuse.name === this.corpuseNameCopy && this.corpuse.address === this.corpuseAddressCopy
          && this.corpuse.floorsNumber === this.corpuseFloorsNumberCopy )
      return true
    else {
      return false;
    }
    
  }

  private CopyCorpuseParams() : void {
    this.corpuseNameCopy = this.corpuse.name;
    this.corpuseAddressCopy = this.corpuse.address;
    this.corpuseFloorsNumberCopy = this.corpuse.floorsNumber;
  }

  private RestoreCorpuseParams() : void {
    this.corpuse.name = this.corpuseNameCopy;
    this.corpuse.address = this.corpuseAddressCopy;
    this.corpuse.floorsNumber = this.corpuseFloorsNumberCopy
  }

  private ResetErrorParemetres() : void {
    this.corpuseNameError = '';
    this.corpuseAddressError = '';
    this.corpuseFloorsNumberError = ''
  }

  public SetEditState() : void {
    this.editState = !this.editState;
    this.readOnlyState = !this.readOnlyState;
    this.CopyCorpuseParams();
  }

  public DeleteCorpuse(id : number) : void {
    this.corpuseService.DeleteCorpuse(id).subscribe();
    this.deleteCorpuseEvent.emit(id);
  }

  public CancelUpdate() : void {
    this.editState = !this.editState;
    this.readOnlyState = !this.readOnlyState;
    this.RestoreCorpuseParams();
  }
}
