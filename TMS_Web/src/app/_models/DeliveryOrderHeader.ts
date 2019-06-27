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
        public StatusDate:Date ;
        public HoldReason: number;
        public HoldDate:Date ;
        public BrokerName:string  ;
        public BrokerId:string ;
        public Brokerkey: string;
        public BrokerRefNo:string  ;
        public PortofOriginKey: string ;
        public PortofDestinationKey: string ;        
        public CarrierKey: string ;
        public VesselName:string  ;
        public BillofLading:string  ;
        public BookingNo:string ;
        public CutOffDate:Date ;
        public Priority:number;
        public IsHazardous:boolean;
        public CreatedBy: string ;
        public CreatedDate:Date;
        public ordertypedescription:string ;
        public statusdescription:string ;

        public orderdetails:Order_details[];
        

}