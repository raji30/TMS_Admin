import { Component, OnInit } from '@angular/core';
import { DeliveryOrderHeader } from '../../../_models/DeliveryOrderHeader';
import { Order_details } from '../../../_models/order_details';
import { InvoiceService } from '../../../_services/invoice.service';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {
  Data: DeliveryOrderHeader[]; 
  constructor( private service:InvoiceService) { }

  ngOnInit() {

    this.service.GetOrderstoGenerateInvoice().subscribe(
      data => {
        this.Data = data;        
      },
      error => console.log(error),
      () => console.log("Get Data", this.Data)
    );

  }

}
