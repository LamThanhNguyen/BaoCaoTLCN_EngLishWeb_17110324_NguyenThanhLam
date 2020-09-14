export interface User {
    id: number;
    userName: string;
    knownAs: string;
    age: number;
    created: Date;
    lastActive: Date;
    city: string;
    dateOfBirth: any;
    country: string;
    roles?: string[];
}