import { Component, OnInit, TemplateRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-billingrates',
  templateUrl: './billingrates.component.html',
  styleUrls: ['./billingrates.component.scss']
})
export class BillingratesComponent implements OnInit {
  public billingModal;
  ngOnInit() {
  }

}
