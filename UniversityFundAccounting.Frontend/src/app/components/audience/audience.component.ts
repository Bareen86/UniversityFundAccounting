import { Component, OnInit } from '@angular/core';
import { CorpuseService } from '../../Services/corpuse.service';
import { NgFor } from '@angular/common';
import { ICorpuse } from '../../Models/ICorpuse';

@Component({
  selector: 'app-audience',
  standalone: true,
  imports: [NgFor],
  templateUrl: './audience.component.html',
  styleUrl: './audience.component.css',
})

export class AudienceComponent {
  
}
