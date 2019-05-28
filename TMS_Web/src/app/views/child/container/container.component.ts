import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { Order_details } from "../../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker";
import { MasterService } from "../../../_services/master.service";
import { Containersize } from "../../../common/master";

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
  containersizelist: Containersize[];
  selectedcontainer: Order_details;
  constructor(private service: MasterService) {}

  ngOnInit() {
    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" },
         );

         this.service
    .getContainerSizeList()
    .subscribe(
      data => (this.containersizelist = data),
      error => console.log(error),
      () => console.log("Get containersizelist complete")
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
onSelectedcontainersize(ContainerNo: string) {
  this.newAttribute.ContainerNo= ContainerNo;
}
}
