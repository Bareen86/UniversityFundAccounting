import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CorpuseService } from './Services/corpuse.service';
import { NgFor } from '@angular/common';
import { ICorpuse } from './Models/ICorpuse';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [CorpuseService]
})
export class AppComponent implements OnInit{
  
  corpuses : ICorpuse[] = [];
  constructor(private corpuseService : CorpuseService) {}

  ngOnInit(): void {
    this.corpuseService.fetchCorpuses().subscribe((corpuses) => {
      this.corpuses = corpuses;
    })
  }
}
