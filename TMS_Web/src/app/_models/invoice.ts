import { Invoicedetails } from "./invoicedetails";

export class Invoice {
  Invoicekey: string;
  InvoiceNo: number;
  InvoiceDate: string;
  CustKey: string;
  BilltoAddrKey: string;
  BilltoAddrCopy: string;
  InvoiceAmt: number;
  DueDate: string;
  InvoiceType: number;
  OrderDetailKey: string;
  invoicedetails:Invoicedetails[];
}
