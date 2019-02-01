export class LoginData {
    email: string;
    password: string;
    constructor() {
        this.email = "";
        this.password = "";
    }
}

export class RegisterData {
    constructor() {
        this.email = "";
        this.password = "";
        this.role_id = "0";
        this.department_id = "0";
        this.year = "1";
    }
    email: string;
    password: string;
    firstname: string;
    lastname: string;
    year: string;
    role_id: string;
    phone_number: string;
    department_id: string;
}