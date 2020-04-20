// Structure of our activity 
// Allows strong typing for Objects
// Used for type checking
export interface IActivity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: string;
    city: string;
    venue: string;
}