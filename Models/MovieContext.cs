using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
	public class MovieContext : DbContext
	{
		public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Movie>()
				.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(60)
				.IsUnicode();

			builder.Entity<Movie>()
				.Property(m => m.Genre)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode();

			builder.Entity<Movie>()
				.Property(m => m.Year)
				.IsRequired()
				.HasMaxLength(4)
				.IsUnicode();

			seed(builder);

			builder.Entity<Movie>();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("MovieList");
		}

		public static void seed(ModelBuilder builder)
		{
			builder.Entity<Movie>().HasData(
				new Movie { Id = 1, Title = "The Shawshank Redemption", Genre = "Drama", Year = 1994 },
				new Movie { Id = 2, Title = "The Godfather", Genre = "Crime", Year = 1972 },
				new Movie { Id = 3, Title = "The Godfather: Part II", Genre = "Crime", Year = 1974 },
				new Movie { Id = 4, Title = "The Dark Knight", Genre = "Action", Year = 2008 },
				new Movie { Id = 5, Title = "Harry Potter: The Order of the Phoenix", Genre = "Fantasy", Year = 2007 },
				new Movie { Id = 6, Title = "The Lord of the Rings: The Return of the King", Genre = "Fantasy", Year = 2003 },
				new Movie { Id = 7, Title = "Pulp Fiction", Genre = "Crime", Year = 1994 }
			);

			Console.WriteLine("SEEDING DATABASE");

			foreach (var movie in builder.Model.GetEntityTypes())
			{
				Console.WriteLine($"Entity: {movie.ToString()}");
				Console.WriteLine($"Properties: {movie.GetProperties().Count()}");
			}
		}
	}
}