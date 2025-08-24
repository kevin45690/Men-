using System;
namespace Colecciones_Genericas
{
    public class Platillos
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }


        public Platillos(string nombre, string categoria,  decimal precio)
        {
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;

        }

        public override string ToString()
        {
            return $"{Nombre}({Categoria})- ${Precio}";
        }

    }
}
