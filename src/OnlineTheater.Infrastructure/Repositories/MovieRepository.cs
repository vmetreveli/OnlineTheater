using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Infrastructure.Repositories;

public sealed class MovieRepository:RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(DbContext context) : base(context)
    {
    }
}