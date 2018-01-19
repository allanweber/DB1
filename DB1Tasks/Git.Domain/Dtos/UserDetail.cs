using System;

namespace Git.Domain.Dtos
{
    public class UserDetail: User
    {
        public string HtmlUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
