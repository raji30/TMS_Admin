import { Address } from "./address";

export class User {
    userkey: string;
    UserId: string;
    username: string;
    password : string;
    firstname: string;
    lastname: string;
    addrkey: string;
    company:string;
    address?: Address;
}