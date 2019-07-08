import { Address } from "./address";

export class Customer {
  CustomerKey: string;
  CustId: string;
  CustName: string;
  addrkey :string;
  Status: number;
  CustomerGroup: number;
  StatusDate: string;
  CreditCheck: boolean;
  CreditLimit: number;
  CreditStatus: number;
  Address = Address;
  customer_edit: Address;
}
