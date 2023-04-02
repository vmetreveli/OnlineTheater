namespace OnlineTheater.Applications.Contracts;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public decimal? MoneySpent { get; set; }
    public IEnumerable<PurchasedMovieDto> PurchasedMovies { get; set; }
}