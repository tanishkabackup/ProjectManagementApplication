export enum Roles {
    Manager = 0,
    Employee = 1
}

export interface RegisterRequest {
  Email: string;
  Password: string;
  ConfirmPassword: string;
  FirstName:string;
  LastName:string;
  OrganizationName:string;
  Domain:string;
  Role:Roles;
}

