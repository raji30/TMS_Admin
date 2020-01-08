import { Order_details } from "./order_details";
import { Address } from "./address";
import { Comments } from "./comments";

export class DeliveryOrderHeader {         
        public OrderKey: string;
        public OrderNo:string ;
        public CustKey:string ;
        public OrderDate:Date ;
        public BillToAddress: string ;
        public BillToAddr: string ;
        public SourceAddress: string ;
        public SourceAddr: string ;
        public DestinationAddress: string ;
        public DestinationAddr: string ;
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
        public carrier: string ;
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
        public Comment:string  ;
        public CommentBO: Comments;
        public BillToAddressBO : Address;
        public SourceAddressBO : Address;
        public DestinationAddressBO : Address;
        public  ReturnAddressBO : Address;
        public BrokerAddressBO : Address;

        public orderdetails:Order_details[];
        

}