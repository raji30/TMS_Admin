import { Component, OnInit, EventEmitter, Input, Output } from "@angular/core";
import { Address } from "../../../_models/address";
import { City } from "../../../_models/city";
import { CityService } from "../../../_services/city.service";
import { identifierModuleUrl } from "@angular/compiler";

@Component({
  selector: "app-address",
  templateUrl: "./address.component.html",
  styleUrls: ["./address.component.scss"],
})
export class AddressComponent implements OnInit {
  @Input() Address: Address;
  @Output() AddressOut = new EventEmitter<Address>();

  citylist: City[];
  CityFilter: City[];
  city:City;
  citySelected: string;
  IsNoEntry: boolean = false;
  constructor(private cityService: CityService) {}

  ngOnInit() {
    this.loadAllCity();   
  }

  BindCity()
  {
    if(this.Address != null|| this.Address!= undefined )
    {
      //this.citySelected = this.citylist.find((city) =>city.citykey).cityname;   
      this.city = this.citylist.find(x => x.cityname == this.Address.City);   
      this.citySelected = this.city.cityname;
      console.log("City Selected : ",this.citySelected);
    }
  }

  loadAllCity() {
    this.cityService.GetCity().subscribe(
      (data) => (this.citylist = this.CityFilter = data,this.BindCity()),
      (error) => console.log(error),
      () => console.log("Get citylist", this.citylist)
    );
  }

  onCitySelect(citySelected: City): void {
    this.citySelected = citySelected.cityname;
    this.Address.City = citySelected.citykey;
    this.AddressOut.emit(this.Address);
  }

  onCitySearchChange(searchValue: string): void {
    if (!searchValue) {
      this.LoadCitylist();
    } // when nothing has typed
    this.CityFilter = this.citylist.filter(
      (city) =>
        city.cityname.toLowerCase().indexOf(searchValue.toLowerCase()) !== -1
    );
    if (this.CityFilter.length == 0) {
      this.IsNoEntry = true;
    } else {
      this.IsNoEntry = false;
    }
  }
  LoadCitylist() {
    this.CityFilter = Object.assign([], this.citylist);
  }

  onAddress1KeyPress(event: any) {
    this.Address.Address1 = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onAddress2KeyPress(event: any) {
    this.Address.Address2 = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onStateKeyPress(event: any) {
    this.Address.State = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onZipKeyPress(event: any) {
    this.Address.Zip = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onCountryKeyPress(event: any) {
    this.Address.Country = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onWebsiteKeyPress(event: any) {
    this.Address.Website = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onPhone1KeyPress(event: any) {
    this.Address.Phone = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onPhone2KeyPress(event: any) {
    this.Address.Phone2 = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onFaxKeyPress(event: any) {
    this.Address.Fax = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onEmail1KeyPress(event: any) {
    this.Address.Email = event.target.value;
    this.AddressOut.emit(this.Address);
  }
  onEmail2KeyPress(event: any) {
    this.Address.Email2 = event.target.value;
    this.AddressOut.emit(this.Address);
  }
}
