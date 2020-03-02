import { Address } from "./address";

export class Company {
  compkey: string;
  compid : string;
  compname: string;
  ParentCompanyKey: string;
  addrkey:string;
  status : number; 
  Address:Address;
}
