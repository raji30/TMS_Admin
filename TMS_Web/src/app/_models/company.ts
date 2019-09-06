import { Address } from "./address";

export class Company {
  CompanyKey: string;
  CompanyName: string;
  ParentCompanyKey: string;
  AddressBO = Address;
}
