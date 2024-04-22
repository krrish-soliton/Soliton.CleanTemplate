namespace Soliton.CleanTemplate.WebApi.Dto
{
    public class SolitonUserCreationRequest
    {
        public string Name { get; set; } = string.Empty;

        public string EmailId { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
    }
}
