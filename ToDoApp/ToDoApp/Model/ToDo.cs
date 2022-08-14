using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Model
{
 
    [Table("ToDo")]
    public class ToDo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime AddDate { get; set; }

        public DateTime CompletedDate { get; set; }


    }
}
