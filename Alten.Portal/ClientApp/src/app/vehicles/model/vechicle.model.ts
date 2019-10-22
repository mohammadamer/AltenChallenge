export interface IVechicleModel {
    id:number;
    VehicleId: number;
    VIN: string;
    RegistrationNo: string;
    CustomerID: number;
    customerName:string;
    isconnected: boolean;
    lastPingTime:Date;
}
