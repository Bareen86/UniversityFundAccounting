import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IconsModule } from '@progress/kendo-angular-icons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { AudienceType } from '../Models/AudienceType';
import { AudienceService } from '../Services/audience.service';
import { ICreateAudience } from '../Models/command/audience/ICreateAudience';
import { IValidationResult } from '../Models/IValidationResult';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";

@Component({
  selector: 'app-add-audience-row',
  standalone: true,
  imports: [InputsModule, FormsModule, IconsModule, LabelModule, DropDownsModule],
  templateUrl: './add-audience-row.component.html',
  styleUrl: './add-audience-row.component.css'
})
export class AddAudienceRowComponent {

  constructor(private audienceService : AudienceService){}

  @Output() newAudienceEvent = new EventEmitter<boolean>();
  @Input() corpuseId !: number;
  name = '';
  audienceType : AudienceType = AudienceType.lecture; 
  capacity = 1;
  floor = 1;
  audienceNumber = 1;
  validation : IValidationResult = {
    isFail: false
  };
  nameError = '';
  capacityError = '';
  floorError = '';
  audienceNumberError = '';
  audienceTypeList : string[] = ['лекционное', 'для практических занятий', 'спортзал', 'другое']
  public selectedValue = "лекционное";

  private ResetParemetres() : void {
    this.name = '';
    this.audienceType = AudienceType.lecture;
    this.capacity = 1;
    this.floor = 1;
    this.audienceNumber = 1;
    this.nameError = '';
    this.capacityError = '';
    this.floorError = '';
    this.audienceNumberError = '';
  }

  private ResetErrors() : void {
    this.nameError = '';
    this.capacityError = '';
    this.floorError = '';
    this.audienceNumberError = '';
  }

  private GetAudienceType() : AudienceType {
    switch (this.selectedValue) {
      case 'лекционное':
        this.audienceType = AudienceType.lecture; 
        break;
      case 'для практических занятий':
        this.audienceType = AudienceType.practice;
        break;
      case 'спортзал':
        this.audienceType = AudienceType.sportHall;
        break;
      case 'другое':
        this.audienceType = AudienceType.other;
        break;
    }
    return this.audienceType;
  }

  private IsThereEmptyNumberInputs() : boolean {
    let validationResult = false;
    if (!this.name){
      this.nameError = 'Имя аудитории не должно быть пустым';
      validationResult = true
    } else this.nameError = '';
    if (!this.capacity){
      this.capacityError = 'Вместимость должна быть больше 0'
      validationResult = true;
    } else this.capacityError = '';
    if (!this.floor){
      this.floorError = 'Этаж должен быть больше 0'
      validationResult = true;
    } else this.floorError = '';
    if (!this.audienceNumber){
      this.audienceNumberError = 'Номер аудитории должен быть больше 0'
      validationResult = true;
    } else this.audienceNumberError = '';
    return validationResult;
  }

  public CreateAudience(audience : ICreateAudience) : void {
    let thereIsEmptyInputs : boolean = this.IsThereEmptyNumberInputs();
    if (thereIsEmptyInputs === false){
      audience.audienceType = this.GetAudienceType();
    this.audienceService.CreateAudience(audience).subscribe(
      (data) => {
        this.validation = data;
        this.newAudienceEvent.emit(true);
        this.ResetParemetres();
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
    );
    }
  }
}
