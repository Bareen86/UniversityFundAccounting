import { Component, OnInit } from '@angular/core';
import { ICorpuse } from '../../Models/ICorpuse';
import { CorpuseService } from '../../Services/corpuse.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-corpuse',
  standalone: true,
  imports: [NgFor],
  templateUrl: './corpuse.component.html',
  styleUrl: './corpuse.component.css'
})
export class CorpuseComponent implements OnInit{
  corpuses : ICorpuse[] = [];
  constructor(private corpuseService : CorpuseService) {}

  ngOnInit(): void {
    this.corpuseService.fetchCorpuses().subscribe((corpuses) => {
      this.corpuses = corpuses;
      console.log(corpuses);
    })
  }
}
