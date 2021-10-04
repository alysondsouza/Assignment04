namespace Assignment4.Entities
{

    //Many-to-Many Relation 
    public class TagTask
    {
        public int TagId { get; set; }
        public int TaskId { get; set; }

        public Tag Tag { get; set; }

        public Task Task { get; set; }

    }
}