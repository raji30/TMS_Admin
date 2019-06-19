import { Address } from "./address";

export class Customer {
  CustomerKey: string;
  CustId: string;
  CustName: string;
  Status: number;
  CustomerGroup: number;
  StatusDate: string;
  CreditCheck: boolean;
  CreditLimit: number;
  CreditStatus: number;
  AddressBO = Address;
}
