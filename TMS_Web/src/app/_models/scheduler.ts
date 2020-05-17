import { AccountingOptions } from './accountingOptions'

export class Scheduler {
    public  OrderDetailKey : string;
    public  RouteKey : string;
    public  AppDateFrom : string;
    public  AppDateTo : string;
    public  SchedulerNotes : string;
    public  LastFreeDay : string;
    public  Status : number;
    public  LegType : number;
    public  DriverNotes: string;
    public  ScheduleArrival : Date;
    public  ScheduleDeparture : Date;
    public  accountingBO  : AccountingOptions[];  
}
