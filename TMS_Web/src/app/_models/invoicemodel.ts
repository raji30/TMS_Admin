import { DeliveryOrderHeader } from "./DeliveryOrderHeader";
import { Invoice } from "./invoice";
import { Address } from "./address";
import { Invoicedetails } from "./invoicedetails";

export class Invoicemodel {
  public order: DeliveryOrderHeader;
  public BillFrom: Address;
  public BillTo: Address;
  public Pickup: Address;
  public Delivery: Address;
  public Broker: Address;
  public invoice: Invoice;
  public invoicedetails: Invoicedetails;
}
