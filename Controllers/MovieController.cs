using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MovieController : ControllerBase
{
	private readonly MovieContext _context;
	private readonly ILogger<MovieController> _logger;

	public MovieController(ILogger<MovieController> logger, MovieContext context)
	{
		_logger = logger;
		_context = context;
		_context.Database.EnsureCreated();
	}

	[HttpGet]
	public async Task<ActionResult> FindAll()
	{
		_logger.LogInformation("MOVIE CONTROLLER: MovieController.FindAll()");

		return await Task.FromResult<ActionResult>(Ok(_context.Movies));
	}


	[HttpGet("{id}")]
	public async Task<ActionResult> FindOne(int id)
	{
		_logger.LogInformation("MOVIE CONTROLLER: MovieController.FindOne()");

		Movie? movie = await _context.Movies.FindAsync(id);

		if (movie == null || movie.Id != id)
		{
			return NotFound();
		}

		return Ok(movie);
	}

	[HttpPost]
	public async Task<ActionResult> Create(Movie movie)
	{
		_logger.LogInformation("MOVIE CONTROLLER: MovieController.Create()");

		_context.Movies.Add(movie);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(FindOne), new { id = movie.Id }, movie);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Update(int id, Movie movie)
	{
		_logger.LogInformation("MOVIE CONTROLLER: MovieController.Update()");

		if (id != movie.Id)
		{
			return BadRequest();
		}

		_context.Entry(movie).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		_logger.LogInformation("MOVIE CONTROLLER: MovieController.Delete()");

		Movie? movie = await _context.Movies.FindAsync(id);

		if (movie == null)
		{
			return NotFound();
		}

		_context.Movies.Remove(movie);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
