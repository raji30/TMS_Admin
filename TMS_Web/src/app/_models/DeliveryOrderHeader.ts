import { Order_details } from "./order_details";

export class DeliveryOrderHeader {
         
        public OrderKey: string;
        public OrderNo:string ;
        public CustKey:string ;
        public OrderDate:string ;
        public BillToAddress: string ;
        public SourceAddress: string ;
        public DestinationAddress: string ;
        public ReturnAddress: string ;
        public Source:string ;
        public OrderType:number ;
        public Status:number ;
        public StatusDate:string ;
        public HoldReason: number;
        public HoldDate:string ;
        public BrokerName:string  ;
        public BrokerId:string ;
        public Brokerkey: string;
        public BrokerRefNo:string  ;
        public PortofOriginKey: string ;
        public CarrierKey: string ;
        public VesselName:string  ;
        public BillofLading:string  ;
        public BookingNo:string ;
        public CutOffDate:string ;
        public Priority:number;
        public IsHazardous:boolean;
        public CreatedBy: string ;
        public CreatedDate:string;
        public ordertypedescription:string ;
        public statusdescription:string ;

        public orderdetails:Order_details[];
        

}