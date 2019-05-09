import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { Order_details } from "../../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker";

@Component({
  selector: "app-container",
  templateUrl: "./container.component.html",
  styleUrls: ["./container.component.scss"]
})
export class ContainerComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  @Input() public ContainerDetails: Array<Order_details> = [];
  @Input() isContainerAttributeVisible : boolean=false;
  private AddContainerDetails: Array<Order_details> = [];
 private newAttribute: any = {};  

 @Output() ContainerDetailsOutput = new EventEmitter<any>();

  selectedcontainer: Order_details;
  constructor() {}

  ngOnInit() {
    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );
    

  }

  addFieldValue() {
    this.AddContainerDetails.push(this.newAttribute);
    this.newAttribute = {};
    this.ContainerDetailsOutput.emit(this.AddContainerDetails);    
}

deleteFieldValue(index) {
    this.ContainerDetails.splice(index, 1);
}
}
