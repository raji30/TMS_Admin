import { Time } from "@angular/common";
import { Tms_routes } from "./tms_routes";
import { Comments } from './comments';

export class Order_details {
        OrderDetailKey :string;
        Id:string;
        OrderKey:string;
        ContainerSize:string;
        ContainerNo:string;
        Chassis:string;
        SealNo: string;
        Weight:number;
        AppDateFrom:string;
        AppDateTo:string;
        PickupDateTime:string;       
        DropOffDateTime:string;      
        ActualPickupDateTime:string;
        ActualDropOffDateTime:string;  
        LastFreeDay:string; 
        SchedulerNotes:string;    
        status:string;
        statusdate:string;
        holdreason:string;
        holddate:string;
        ContainerSizeDesc:string;
        StatusDesc:string;
        HoldReasonDesc:string;  
        orderroutes:Tms_routes;
        Comments:string;       

        CreatedBy:string;
        createdDate:string;
}
