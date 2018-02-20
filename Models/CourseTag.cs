namespace MultivisionCoreAPI.Models
{
    public class CourseTag
    {
        public CourseTag(string value) 
        {
            Value = value;
        }

        public long Id { get; set; }
        public string Value { get; set; }
    }
}