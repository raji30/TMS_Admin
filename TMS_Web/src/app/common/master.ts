import { Address } from "../_models/address";

export class Containersize {
  // name: string;
  // value: number;
  containersize: number;
  description: string;
}
export class Priority {
  name: string;
  value: number;
}

export class OrderType {
  name: string;
  value: number;
}

export class Status {
  description: string;
  status: number;
}

export class AddressType {
  name: string;
  value: number;
}

export class HoldReason {
  name: string;
  value: number;
}

export class Source {
  name: string;
  value: number;
}

export class Carrier {
  CarrierKey: string;
  CarrierId: string;
  CarrierName: string;
  isstreamline: boolean;
  Address: Address;
  ScacCode: string;
  LicensePlate: string;
  LicensePlateExpiryDate: string;
  Status: number;
  StatusDate: string;
}

export class LoadDischargePort {
  name: string;
  value: number;
}
export class DischargePort {
  name: string;
  value: number;
}
