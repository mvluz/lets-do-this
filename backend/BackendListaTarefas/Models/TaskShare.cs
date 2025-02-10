using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TaskShare
    {
        public TaskShare(){}
        public TaskShare(string name, string details, bool completed)
        {
            Name = name;
            Details = details;
            Completed = completed;
        }

        public string? Id { get; set; }        
        public string Name { get; set; } = string.Empty;        
        public string Details { get; set; } = string.Empty;
        public bool Completed { get; set; } = false;
    }
}
