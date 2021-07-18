using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackSystem.WebAPI.Model.Option
{
    public class GenericOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptionID { get; set; }

        public string OptionName { get; set; }

        public string OptionValue { get; set; }

        public string Category { get; set; }

        public string OwnerLevel { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}
