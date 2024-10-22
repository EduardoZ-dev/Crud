namespace VirtualLockerAPI.DTO_s
{
    public class UsuarioDTO
    {
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string ConfirmarClave { get; set; } = null!;
    }
}
