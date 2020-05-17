import { Invoicedetails } from "./invoicedetails";

export class Invoice {
  Invoicekey: string;
  InvoiceNo: number;
  InvoiceDate: Date;
  CustKey: string;
  CustName: string;
  BilltoAddrKey: string;
  BilltoAddrCopy: string;
  InvoiceAmt: number;
  DueDate: Date;
  InvoiceType: number;
  OrderKey: string;
  StatusDesc:string;
  nextaction:string;
  invoicedetails:Invoicedetails[];
}
