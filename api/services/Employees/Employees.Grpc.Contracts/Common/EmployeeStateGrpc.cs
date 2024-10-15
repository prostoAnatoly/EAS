namespace Employees.Grpc.Contracts.Common;

public enum EmployeeStateGrpc
{
    None = 0,

    /// <summary>
    /// Действующий
    /// </summary>
    Active = 1,

    /// <summary>
    /// Уволенный
    /// </summary>
    Dismissed = 2,
}
