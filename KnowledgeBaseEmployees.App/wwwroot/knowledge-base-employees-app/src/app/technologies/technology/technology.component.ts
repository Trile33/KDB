import { Component, OnInit, Input } from '@angular/core';
import { Technology} from '../../models/technology.model';

@Component({
  selector: 'app-technology',
  templateUrl: './technology.component.html',
  styleUrls: ['./technology.component.css']
})
export class TechnologyComponent implements OnInit {

  @Input() technology: Technology[];

  constructor() { 
  }

  ngOnInit() {
  }

}
