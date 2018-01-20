import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-inline-form-label',
  templateUrl: './inline-form-label.component.html',
  styleUrls: ['./inline-form-label.component.css']
})
export class InlineFormLabelComponent implements OnInit {

  @Input() label: string;
  @Input() value: string;

  constructor() { }

  ngOnInit() {
  }

}
