using System.Data;

namespace PlacementCellManagementAPI.Models
{
    public class BaseResponse
    {
        public bool IsError { get; set; } = false;
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class Response : BaseResponse
    {
        public DataTable? Data { get; set; }
    }
}
