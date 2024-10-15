using Eas.Gate.Ui.Models.Common;

namespace Eas.Gate.Ui.Models.Users;

public class MeResponse
{

    public FullNameModel FullName { get; init; }

    public string AvatarUrl { get; init; }

    public string UserName { get; init; }
}
