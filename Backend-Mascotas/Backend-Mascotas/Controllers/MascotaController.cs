using AutoMapper;
using Backend_Mascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mascotas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MascotaController : ControllerBase
	{
		private readonly AplicationDbContext _context;

		private readonly IMapper _mapper;

		public MascotaController(AplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var listMascotas = await _context.Mascota.ToListAsync();
				return Ok(listMascotas);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var mascota = await _context.Mascota.FindAsync(id);
				if(mascota == null)
				{
					return NotFound();
				}
				return Ok(mascota);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var mascota = await _context.Mascota.FindAsync(id);

				if (mascota == null)
				{
					return NotFound(mascota);
				}

				_context.Mascota.Remove(mascota);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(Mascota mascota)
		{
			try
			{
				mascota.FechaCreacion = DateTime.Now;
				_context.Add(mascota);
				await _context.SaveChangesAsync();
				return CreatedAtAction("Get", new { id = mascota.Id }, mascota);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Mascota mascota)
		{
			try
			{
				if(id != mascota.Id)
				{
					return BadRequest();
				}

				var mascotaItem = await _context.Mascota.FindAsync(id);

				if (mascotaItem == null)
				{
					return NotFound();
				}

				mascotaItem.Nombre = mascota.Nombre;
				mascotaItem.Edad = mascota.Edad;
				mascotaItem.Raza = mascota.Raza;
				mascotaItem.Color = mascota.Color;
				mascotaItem.Peso = mascota.Peso;
				

				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch(Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}
	} 
}

