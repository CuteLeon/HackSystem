using System.Collections.Generic;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.WebDataTransfer.Account;

    public class AccountInfoDTO
    {
        public int Level { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual IList<QueryUserBasicProgramMapDTO> UserProgramMaps { get; set; }
    }
