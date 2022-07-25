using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel
{
    public class VisitorLogs
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string VisitorName { get; set; }
        
        [Required]
        public string VisitorCNIC { get; set; }
        
        [Required]
        public string VisitorCardNo { get; set; }
        
        [Required]
        public string StudentName { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}