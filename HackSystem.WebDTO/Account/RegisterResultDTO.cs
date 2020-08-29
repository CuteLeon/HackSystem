using System.Collections.Generic;

namespace HackSystem.WebDTO.Account
{
    public class RegisterResultDTO
    {
        public bool Successful { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
