using Referendum.Domain.Enums;
using Newtonsoft.Json;
namespace OnlineTheater.Domains.Entities;

public class Movie:EntityBase
{

    public  string Name { get; set; }



    public virtual LicensingModel LicensingModel { get; set; }
}