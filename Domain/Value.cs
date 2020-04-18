using System;

namespace Domain
{
    public class Value
    {
        // These properties can be get and set 
        // Through Entity id will automatically be used as a primary key
        // Code first => code first and then generate script for database
        public int Id { get; set;}
        public string Name { get; set; }
    }
}
