namespace VirtualLockerAPI.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;

    }
}
