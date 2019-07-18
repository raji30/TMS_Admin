import { Time } from "@angular/common";
import { Tms_routes } from "./tms_routes";

export class Order_details {
        OrderDetailKey :string;
        OrderKey:string;
        containerSize:string;
        containerNo:string;
        chassis:string;
        sealNo: string;
        weight:number;
        AppDateFrom:string;
        AppDateTo:string;
        PickupDateTime:string;       
        DropOffDateTime:string;      
        ActualPickupDateTime:string;
        ActualDropOffDateTime:string;       
        status:string;
        statusdate:string;
        holdreason:string;
        holddate:string;
        containerSizeDesc:string;
        StatusDesc:string;
        HoldReasonDesc:string;  
        orderroutes:Tms_routes;
}
