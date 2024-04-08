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
import { AudienceComponent } from '../audience/audience.component';
import { IAudience } from '../../Models/IAudience';
import { AudienceService } from '../../Services/audience.service';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { AddCorpuseRowComponent } from '../add-corpuse-row/add-corpuse-row.component';
import { AddAudienceRowComponent } from '../../add-audience-row/add-audience-row.component';

@Component({
  selector: 'app-corpuse',
  standalone: true,
  imports: [NgFor, InputsModule, FormsModule, IconsModule, LabelModule,
    AudienceComponent, ButtonsModule, AddCorpuseRowComponent, AddAudienceRowComponent],
  templateUrl: './corpuse.component.html',
  styleUrl: './corpuse.component.css',
  providers: [CorpuseService]
})
export class CorpuseComponent {

  constructor(private corpuseService : CorpuseService, private audienceService : AudienceService) {}

  editState : boolean = false;
  readOnlyState : boolean = true;
  showAudiencesState : boolean = false;
  audiences : IAudience[] = [];
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
  showAddAudienceForm = false;
  

  private IsThereEmptyInputs() : boolean {
    let validationResult = false;
    if (!this.corpuse.name){
      this.corpuseNameError = 'Имя корпуса не должно быть пустым'
      validationResult = true;
    } else this.corpuseNameError = '';
    if (!this.corpuse.address){
      this.corpuseAddressError = 'Адрес не должен быть пустым'
      validationResult = true;
    } else this.corpuseAddressError = '';
    if (!this.corpuse.floorsNumber){
      this.corpuseFloorsNumberError = 'В корпусе должен быть минимум 1 этаж'
      validationResult = true;
    } else this.corpuseFloorsNumberError = '';
    return validationResult;
  }
  
  public UpdateCorpuse( id : number, corpuseData : IUpdateCorpuse ) : void {
    let thereIsEmptyInputs : boolean = this.IsThereEmptyInputs();
    if (thereIsEmptyInputs === false){
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
              case 'Адрес не должен быть пустым':
                this.corpuseAddressError = 'Адрес не должен быть пустым';
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
  }

  public ShowAddAudience() : void {
    this.showAddAudienceForm = !this.showAddAudienceForm
  }

  public ShowAudiences() : void {
    if (this.showAudiencesState === false){
      this.audienceService.GetCorpuseAudiences(this.corpuse.id).subscribe((result) => {
        this.audiences = result.objResult;
        this.showAudiencesState = !this.showAudiencesState;
      })
    } else  this.showAudiencesState = !this.showAudiencesState;
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
    this.corpuse.floorsNumber = this.corpuseFloorsNumberCopy;
    this.corpuseNameError = '';
    this.corpuseAddressError = '';
    this.corpuseFloorsNumberError = '';
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

  DeleteAudienceByIdHandler(id : number) {
    this.audiences = this.audiences.filter(audience => audience.id != id)
  }

  NewAudienceEventHandler(occured : boolean) {
    this.audienceService.GetCorpuseAudiences(this.corpuse.id).subscribe((audience) => {
      this.audiences = audience.objResult;
    })
  }
}
