export class Device {
    Id: string;
    NickName: string;
}

export class Gps {
    constructor(lattitude?: number, longtitude?: number) {
        this.Lattitude = lattitude;
        this.Longtitude = longtitude;
    }
    Id: string;
    DateTime: Date;
    Lattitude: number;
    Longtitude: number;
    CurrentSpeed: number;
    AverageSpeed: number;
    MaxSpeed: number;
    SpeedAccuracy: number;
}

export class HardwareStatus {
    Id: string;
    DateTime: Date;
    MainPower: number;
    Battery: number;
    McuTemperature: number;
    IsMoving: boolean;
    Signals: Signal[];
}

export class Signal {
    Index: number;
    Value: number;
}

export class DateTimeWindow {
    StartDateTime: Date;
    EndDateTime: Date;
}