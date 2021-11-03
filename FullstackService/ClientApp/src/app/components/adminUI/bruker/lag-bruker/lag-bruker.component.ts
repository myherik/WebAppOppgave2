import { Component, OnInit } from '@angular/core';
import { Bruker } from 'src/app/interface/bruker';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-lag-bruker',
  templateUrl: './lag-bruker.component.html',
  styleUrls: ['./lag-bruker.component.css']
})
export class LagBrukerComponent implements OnInit {

  bruker: Bruker = {
    brukernavn: "",
    passord: "",
  }

  constructor(private brukerService: BrukerService) { }

  ngOnInit() {
  }

  registrer() {
    this.brukerService.lagBruker(this.bruker).subscribe(bruker => {
      console.log(bruker)
    }, err => console.log(err))
  }

}