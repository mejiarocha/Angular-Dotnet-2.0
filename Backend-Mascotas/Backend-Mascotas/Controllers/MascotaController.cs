using AutoMapper;
using Backend_Mascotas.Models;
using Backend_Mascotas.Models.DTO;
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

				var listMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listMascotas);

				return Ok(listMascotasDTO);
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

				// objeto mappeado al dto para devolver
				var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);

				return Ok(mascotaDTO);
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
		public async Task<IActionResult> Post(MascotaDTO mascotaDTO)
		{
			try
			{
				var mascota = _mapper.Map<Mascota>(mascotaDTO);

				mascota.FechaCreacion = DateTime.Now;
				_context.Add(mascota);
				await _context.SaveChangesAsync();

				var mascotaItemDTO = _mapper.Map<MascotaDTO>(mascota);

				return CreatedAtAction("Get", new { id = mascotaItemDTO.Id }, mascotaItemDTO);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, MascotaDTO mascotaDTO)
		{
			try
			{

				var mascota = _mapper.Map<Mascota>(mascotaDTO);

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

