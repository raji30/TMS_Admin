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
  @Input() isContainerAttributeVisible: boolean = false;
  private AddContainerDetails: Array<Order_details> = [];
  private newAttribute: any = {};

  @Output() ContainerDetailsOutput = new EventEmitter<any>();
  containersizelist: Containersize[];
  selectedcontainer: Order_details;

  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  ContainerSize: string;
  containerSizeDesc: string;
  ContainerNo: string;
  Chassis: string;
  SealNo: string;
  Weight: string;
  Comments: string;
  CommentItems = new Array();

  hazard:string;trixale:string;overweight:string;needstobescaled:string;

  constructor(private service: MasterService) {}

  ngOnInit() {
    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );

    this.service
      .getContainerSizeList()
      .subscribe(
        data => (this.containersizelist = data),
        error => console.log(error),
        () => console.log("Get containersizelist complete")
      );

    this.dropdownList = [
      { item_id: 1, item_text: "Hazard" },
      { item_id: 2, item_text: "Overweight" },
      { item_id: 3, item_text: "Triaxle" },
      { item_id: 4, item_text: "Needs to be scaled" }
    ];
    this.selectedItems = [
      // { item_id: 3, item_text: 'Pune' },
      // { item_id: 4, item_text: 'Navsari' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: "item_id",
      textField: "item_text",
      selectAllText: "Select All",
      unSelectAllText: "UnSelect All",
      itemsShowLimit: 4,
      allowSearchFilter: false
    };
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
    this.newAttribute.ContainerNo = ContainerNo;
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }

  Checkbox1_Change(values: any) {
    //Checkbox1_Change_Hazard
    if (values.currentTarget.checked) {
      this.CommentItems.push("Hazard");
      console.log("Hazard Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Hazard");
      this.CommentItems.splice(index, 1);
      console.log("Hazard Removed", this.CommentItems);
    }
  }
  Checkbox2_Change(values: any) {
    //Checkbox2_Change_OverWeight
    if (values.currentTarget.checked) {
      this.CommentItems.push("Over Weight");
      console.log("Over Weight Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Over Weight");
      this.CommentItems.splice(index, 1);
      console.log("Over Weight Removed", this.CommentItems);
    }
  }
  Checkbox3_Change(values: any) {
    //Checkbox3_Change_Triaxle
    console.log(values.currentTarget.checked);

    if (values.currentTarget.checked) {
      this.CommentItems.push("Triaxle");
      console.log("Triaxle Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Triaxle");
      this.CommentItems.splice(index, 1);
      console.log("Triaxle Removed", this.CommentItems);
    }
  }
  Checkbox4_Change(values: any) {
    if (values.currentTarget.checked) {
      this.CommentItems.push("Needs to be scaled");
      console.log("Needs to be scaled Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Needs to be scaled");
      this.CommentItems.splice(index, 1);
      console.log("Needs to be scaled Removed", this.CommentItems);
    }
  }

  add() {
    var containerDetails: any = {};
    containerDetails.ContainerNo = this.ContainerNo;
    containerDetails.ContainerSize = this.ContainerSize;
    containerDetails.containerSizeDesc = this.containerSizeDesc;
    containerDetails.Chassis = this.Chassis;
    containerDetails.SealNo = this.SealNo;
    containerDetails.Weight = this.Weight;

    containerDetails.Comments = this.Comments = this.CommentItems.toString();
    this.AddContainerDetails.push(containerDetails);
    this.ContainerDetailsOutput.emit(this.AddContainerDetails);

    this.ContainerNo = undefined;
    this.ContainerSize = undefined;
    this.containerSizeDesc =undefined;
    this.Chassis = undefined;
    this.SealNo = undefined;
    this.Weight = undefined;
    this.Comments = undefined;
    this.CommentItems = [];

    this.hazard = undefined;
    this.trixale= undefined;
    this.overweight= undefined;
    this.needstobescaled= undefined;
  }

  drpcontainersizeChanged(value:any)
  {
    this.containerSizeDesc = this.containersizelist.find(x => x.containersize == value).description;   
  }
}
