using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts;

public class PageData
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
}