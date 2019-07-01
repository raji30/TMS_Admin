import { Component, OnInit, EventEmitter, Input, Output } from "@angular/core";
import { Broker } from "../../../_models/broker";
import { BrokerService } from "../../../_services/broker.service";
@Component({
  selector: "app-broker",
  templateUrl: "./broker.component.html",
  styleUrls: ["./broker.component.scss"]
})
export class BrokerComponent implements OnInit {
  broker: Broker[];
  brokerName: string = "Select Broker";
  brokerKey: string;
  selectedBroker: Broker;
  @Input() brokerKeyTobind: string;
  @Output() BrokerSelectedOutput = new EventEmitter<string>();
  constructor(private service: BrokerService) {}

  ngOnInit() {
    // this.broker = [{ "BrokerName": "Maersk Line",
    //   "BrokerId": "ML0023",
    //   "BrokerKey":"ae24e3ba-5aad-11e9-94fc-332aa5298740",
    //   "Status":"1"}];

    this.service
      .getbrokers()
      .subscribe(
        data => (this.broker = data),
        error => console.log(error),
        () => console.log("Get brokers complete")
      );
  }

  ngOnChanges() {
    if (this.brokerKeyTobind != undefined) {
      this.service.getbrokers().subscribe(
        (data: any) => {
          this.broker = data;
          this.selectedBroker = this.broker.find(
            x => x.BrokerKey === this.brokerKeyTobind
          );
          this.brokerName = this.selectedBroker.BrokerName;
        },
        error => console.log(error),
        () => console.log("Get customer complete")
      );    
    }
  }

  onSelect(brokerSelected: Broker): void {
    this.selectedBroker = brokerSelected;
    this.brokerName = brokerSelected.BrokerName;
    this.brokerKey = brokerSelected.BrokerKey;
    this.BrokerSelectedOutput.emit(brokerSelected.BrokerKey);
  }
}
