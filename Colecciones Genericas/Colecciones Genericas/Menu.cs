using Colecciones_Genericas;
using System;
using System.Collections.Generic;
using System.Linq;

public class MenuRestaurante
{
    private List<Platillos> platillos;

    public MenuRestaurante()
    {
        platillos = new List<Platillos>();
    }

    public void AgregarPlatillo(Platillos platillo)
    {
        platillos.Add(platillo);
        Console.WriteLine($"✓ Platillo agregado: {platillo}");
    }

    public void ListarPlatillos()
    {
        Console.WriteLine("\n=== MENÚ DEL RESTAURANTE ===");
        if (platillos.Count == 0)
        {
            Console.WriteLine("No hay platillos en el menú.");
            return;
        }

        var categorias = platillos.GroupBy(p => p.Categoria);
        foreach (var categoria in categorias)
        {
            Console.WriteLine($"\n--- {categoria.Key.ToUpper()} ---");
            foreach (var platillo in categoria)
            {
                Console.WriteLine($"  • {platillo}");
            }
        }
    }

    public bool EliminarPlatillo(string nombre)
    {
        var platillo = platillos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        if (platillo != null)
        {
            platillos.Remove(platillo);
            Console.WriteLine($"Platillo eliminado: {platillo.Nombre}");
            return true;
        }
        Console.WriteLine($" No se encontró el platillo: {nombre}");
        return false;
    }

    public Platillos BuscarPlatillo(string nombre)
    {
        return platillos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public List<Platillos> ObtenerTodosPlatillos()
    {
        return platillos;
    }

    public int CantidadPlatillos()
    {
        return platillos.Count;
    }

    public void InicializarMenuDefault()
    {
        // Agregar platillos iniciales al menú
        AgregarPlatillo(new Platillos("Hamburguesa Clásica", "Plato Principal", 12.99m));
        AgregarPlatillo(new Platillos("Pizza Margherita", "Plato Principal", 15.50m));
        AgregarPlatillo(new Platillos("Ensalada César", "Entrada", 8.75m));
        AgregarPlatillo(new Platillos("Sopa del Día", "Entrada", 6.99m));
        AgregarPlatillo(new Platillos("Pastel de Chocolate", "Postre", 5.25m));
        AgregarPlatillo(new Platillos("Helado de Vainilla", "Postre", 4.50m));
        AgregarPlatillo(new Platillos("Refresco", "Bebida", 2.50m));
        AgregarPlatillo(new Platillos("Café", "Bebida", 3.00m));
    }
}
