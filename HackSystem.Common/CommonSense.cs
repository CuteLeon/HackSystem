namespace HackSystem.Common
{
    public class CommonSense
    {
        /// <summary>
        /// Group is a collection of Users,
        /// Role is a collection of Claims.
        /// </summary>
        public class Roles
        {
            /// <summary>
            /// Commander Role
            /// </summary>
            public const string CommanderRole = "Commander";

            /// <summary>
            /// Hack Role
            /// </summary>
            public const string HackerRole = "Hacker";
        }

        /// <summary>
        /// Group is a collection of Users,
        /// Role is a collection of Claims.
        /// </summary>
        public class Claims
        {
            /// <summary>
            /// Professional Claim
            /// </summary>
            public const string ProfessionalClaim = "Professional";
        }
    }
}
