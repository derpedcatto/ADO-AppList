using System;

namespace ADO_AppList.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public override string ToString()
        {
            return Id.ToString()[0..3] + "...\t" + Name;
        }
    }
}
