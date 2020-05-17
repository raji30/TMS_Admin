import { Address } from './address';

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
  achrequired:boolean;
  paymentterms :number; 
  Address:Address;
  address=Address;
  customer_edit: Address;
}
