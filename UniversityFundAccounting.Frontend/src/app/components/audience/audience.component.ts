import { NgFor } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IconsModule } from '@progress/kendo-angular-icons';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { IAudience } from '../../Models/IAudience';
import { AudienceService } from '../../Services/audience.service';

@Component({
  selector: 'app-audience',
  standalone: true,
  imports: [NgFor, InputsModule, FormsModule, IconsModule, LabelModule],
  templateUrl: './audience.component.html',
  styleUrl: './audience.component.css',
})

export class AudienceComponent {
  
  constructor(http: AudienceService){} 

  editState : boolean = false;
  readOnlyState : boolean = true;
  @Input() audience !: IAudience

}
