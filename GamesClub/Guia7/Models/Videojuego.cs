namespace GamesClub.Models
{
    public class Videojuego
    {
        public string codVideojuego { get; set; }
        public string titulo { get; set; }
        public string genero { get; set; }
        public string plataforma { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string desarrollador { get; set; }
        public string publisher { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; }
    }
}
