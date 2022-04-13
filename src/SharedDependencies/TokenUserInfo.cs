using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPhase.SharedDependencies;
public class TokenUserInfo
{
    public int UserId { get; set; }

    public int PositionId { get; set; }

    public int OrganizationId { get; set; }

    public Guid SessionId { get; set; }

    public string UserName { get; set; }

    public int RoleId { get; set; }

    public string client_id { get; set; }
}
