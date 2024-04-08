import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IconsModule } from '@progress/kendo-angular-icons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { IAudience } from '../../Models/IAudience';
import { AudienceService } from '../../Services/audience.service';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { AudienceType } from '../../Models/AudienceType';
import { IUpdateAudience } from '../../Models/command/audience/IUpdateAudience';
import { IValidationResult } from '../../Models/IValidationResult';

@Component({
  selector: 'app-audience',
  standalone: true,
  imports: [NgFor, InputsModule, FormsModule, IconsModule, LabelModule, DropDownsModule],
  templateUrl: './audience.component.html',
  styleUrl: './audience.component.css',
})

export class AudienceComponent implements OnInit {
  
  constructor(private audienceService: AudienceService){} 
  ngOnInit(): void {
    switch (this.audience.audienceType){
      case AudienceType.lecture:
        this.selectedValue = 'лекционное';
        break;
      case AudienceType.practice:
        this.selectedValue = 'для практических занятий';
        break;
      case AudienceType.sportHall:
        this.selectedValue = 'спортзал';
        break;
      case AudienceType.other:
        this.selectedValue = 'другое';
        break;
    }
  }

  editState : boolean = false;
  readOnlyState : boolean = true;
  @Input() audience !: IAudience;
  @Output() deleteAudienceEvent = new EventEmitter<number>();
  nameError = '';
  capacityError = '';
  floorError = '';
  audienceNumberError = '';
  audienceTypeList : string[] = ['лекционное', 'для практических занятий', 'спортзал', 'другое']
  public selectedValue = "лекционное";
  audienceNameCopy = '';
  audienceTypeCopy = AudienceType.lecture;
  audienceCapacityCopy = 0
  audienceFloorCopy = 0;
  audienceAudienceNumberCopy = 0;
  validation : IValidationResult = {
    isFail: false
  };


  public SetEditState() : void {
    this.editState = !this.editState;
    this.readOnlyState = !this.readOnlyState;
    this.CopyAudienceParams();
  }

  private CopyAudienceParams() : void {
    this.audienceNameCopy = this.audience.name;
    this.audienceTypeCopy = this.audience.audienceType;
    this.audienceCapacityCopy = this.audience.capacity;
    this.audienceFloorCopy = this.audience.floor;
    this.audienceAudienceNumberCopy = this.audience.audienceNumber
  }

  private IsThereEmptyNumberInputs() : boolean {
    let validationResult = false;
    if (!this.audience.name){
      this.nameError = 'Имя аудитории не должно быть пустым'
      validationResult = true;
    } else this.nameError = '';
    if (!this.audience.capacity){
      this.capacityError = 'Вместимость должна быть больше 0'
      validationResult = true;
    } else this.capacityError = '';
    if (!this.audience.floor){
      this.floorError = 'Этаж должен быть больше 0'
      validationResult = true;
    } else this.floorError = '';
    if (!this.audience.audienceNumber){
      this.audienceNumberError = 'Номер аудитории должен быть больше 0'
      validationResult = true;
    } else this.audienceNumberError = '';
    return validationResult;
  }

  public DeleteAudience(id : number) : void {
    this.audienceService.DeleteAudience(id).subscribe();
    this.deleteAudienceEvent.emit(id)
  }

  private RestoreAudienceParams() : void {
    this.audience.name = this.audienceNameCopy;
    this.audience.audienceType = this.audienceTypeCopy;
    this.audience.capacity = this.audienceCapacityCopy;
    this.audience.floor = this.audienceFloorCopy;
    this.audience.audienceNumber = this.audienceAudienceNumberCopy;
    this.nameError = '';
    this.capacityError = '';
    this.floorError = '';
    this.audienceNumberError = '';
  }

  public CancelUpdate() : void {
    this.editState = !this.editState;
    this.readOnlyState = !this.readOnlyState;
    this.RestoreAudienceParams();
  }

  private AudienceParamsAreEqual() : boolean {
    if ( this.audience.name === this.audienceNameCopy && this.audience.audienceType === this.audienceTypeCopy
          && this.audience.capacity === this.audienceCapacityCopy &&  this.audience.floor === this.audienceFloorCopy
        && this.audience.audienceNumber === this.audienceAudienceNumberCopy)
      return true
    else {
      return false;
    }
  }

  public UpdateAudience(id : number, audience : IUpdateAudience) : void {
    let thereIsEmptyInputs : boolean = this.IsThereEmptyNumberInputs();
    if (thereIsEmptyInputs === false){
      if (this.AudienceParamsAreEqual() === false){
        this.audienceService.UpdateAudience(id, audience).subscribe(
          (data) => {
            this.editState = !this.editState;
            this.readOnlyState = !this.readOnlyState;
            this.ResetErrors();
          },
          (err) => {
            this.audienceService.handleError(err);
            this.validation = err.error.validationResult;
            this.ResetErrors();
            switch(this.validation.error) {
              case 'Такая аудитория в корпусе уже есть':
                this.audienceNumberError = 'Такая аудитория в корпусе уже есть';
                break;
              case 'Имя аудитории не должно быть пустым':
                this.nameError = 'Имя аудитории не должно быть пустым';
                break;
              case 'Вместимость должна быть больше 0':
                this.capacityError = 'Вместимость должна быть больше 0';
                break;
              case 'Этаж должен быть больше 0':
                this.floorError = 'Этаж должен быть больше 0';
                break;
              case 'Номер аудитории должен быть больше 0':
                this.audienceNumberError = 'Номер аудитории должен быть больше 0';
                break;
            }
          }
        )
      } else {
        this.editState = !this.editState;
        this.readOnlyState = !this.readOnlyState;
      }
    }
  }

  private ResetErrors() : void {
    this.nameError = '';
    this.capacityError = '';
    this.floorError = '';
    this.audienceNumberError = '';
  }
}
