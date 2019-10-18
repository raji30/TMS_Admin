import { DeliveryOrderHeader } from "./DeliveryOrderHeader";
import { Invoice } from "./invoice";
import { Address } from "./address";
import { Invoicedetails } from "./invoicedetails";
import { Order_details } from "./order_details";

export class Invoicemodel {
  public order: DeliveryOrderHeader;
  public orderDetails:Order_details[];
  public BillFrom: Address;
  public BillTo: Address;
  public Pickup: Address;
  public Delivery: Address;
  public Broker: Address;
  public invoice: Invoice;
  public invoicedetails: Invoicedetails[];
}
