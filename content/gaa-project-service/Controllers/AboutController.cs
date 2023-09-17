using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gaa.Project.Service.Controllers;

/// <summary>
/// ���������� ���������� � ����������.
/// </summary>
[Route("api/about")]
public sealed class AboutController : ControllerBase
{
    /// <summary>
    /// �������� ������ ����������.
    /// </summary>
    /// <returns>������ ����������.</returns>
    [HttpGet("version")]
    [SwaggerResponse(StatusCodes.Status200OK, "������ ����������.", typeof(string))]
    public string? Version()
    {
        return Program.CurrentVersion;
    }
}