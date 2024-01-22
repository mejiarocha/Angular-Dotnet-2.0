using Microsoft.EntityFrameworkCore;

namespace Backend_Mascotas.Models
{
	public class AplicationDbContext: DbContext
	{
		public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
		{

		}


		//linea que le da el nombre a la tabla para correr la migracion 
		public DbSet<Mascota> Mascota { get; set;}
	}
}
