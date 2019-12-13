import { Item } from "./item";

export class Ratesheet {
  ratekey: string;
  customerkey: string;
  // item: Item[];
  item: Array<Item> = [];
  itemkey: string;
  description: string;  
  unitprice: number;
  userkey: string;
  createdate: string;
  lastupdatedate: string;
}
