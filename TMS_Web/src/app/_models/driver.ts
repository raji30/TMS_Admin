import { Address } from "./address";

export class Driver {
  DriverKey: string;
  DriverId: string;
  drivernotes?: string;
  FirstName: string;
  LastName?: string;
  Address?: Address;
  CarrierKey?: string;
  DriversLicenseNo?: string;
  LicenseExpiryDate?: string;
  CreateDate?: string;
  Status?: number;
  StatusDate?: string;
  VendorKey?: string;
}
