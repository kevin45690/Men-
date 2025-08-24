using Colecciones_Genericas;
using System;
using System.Collections.Generic;
using System.Linq;

public class Pedido
{
    public int NumeroPedido { get; set; }
    public string Cliente { get; set; }
    public List<Platillos> Platillos { get; set; }
    public DateTime FechaHora { get; set; }

    public Pedido(int numeroPedido, string cliente)
    {
        NumeroPedido = numeroPedido;
        Cliente = cliente;
        Platillos = new List<Platillos>();
        FechaHora = DateTime.Now;
    }

    public void AgregarPlatillo(Platillos platillo)
    {
        Platillos.Add(platillo);
    }

    public decimal CalcularTotal()
    {
        return Platillos.Sum(p => p.Precio);
    }

    public override string ToString()
    {
        return $"Pedido #{NumeroPedido} - {Cliente} - Total: ${CalcularTotal()} - {FechaHora:HH:mm}";
    }

    public string DetallesCompletos()
    {
        var detalles = $"Pedido #{NumeroPedido}\nCliente: {Cliente}\nFecha: {FechaHora:yyyy-MM-dd HH:mm}\n\nPlatillos:\n";

        foreach (var platillo in Platillos)
        {
            detalles += $"  • {platillo}\n";
        }

        detalles += $"\nTotal: ${CalcularTotal()}";
        return detalles;
    }
}