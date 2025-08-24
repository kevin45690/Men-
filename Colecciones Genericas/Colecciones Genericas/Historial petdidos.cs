using Colecciones_Genericas;
using System;
using System.Collections.Generic;
using System.Linq;

public class HistorialPedidos
{
    private Stack<Pedido> pedidosEntregados;

    public HistorialPedidos()
    {
        pedidosEntregados = new Stack<Pedido>();
    }

    public void AgregarPedidoEntregado(Pedido pedidos)
    {
        pedidosEntregados.Push(pedidos);
        Console.WriteLine($"✓ Pedido entregado: #{pedidos.NumeroPedido} - {pedidos.Cliente}");
    }

    public void MostrarUltimosPedidos(int cantidad = 5)
    {
        Console.WriteLine($"\n=== ÚLTIMOS {cantidad} PEDIDOS ENTREGADOS ===");
        if (pedidosEntregados.Count == 0)
        {
            Console.WriteLine("No hay pedidos en el historial.");
            return;
        }

        var ultimosPedidos = pedidosEntregados.Take(cantidad).ToList();
        for (int i = 0; i < ultimosPedidos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ultimosPedidos[i]}");
        }
    }

    public void MostrarDetallesUltimoPedido()
    {
        if (pedidosEntregados.Count > 0)
        {
            var ultimo = pedidosEntregados.Peek();
            Console.WriteLine("\n=== DETALLES DEL ÚLTIMO PEDIDO ===");
            Console.WriteLine(ultimo.DetallesCompletos());
        }
        else
        {
            Console.WriteLine("No hay pedidos en el historial.");
        }
    }

    public Pedido ObtenerUltimoPedidoEntregado()
    {
        if (pedidosEntregados.Count > 0)
        {
            return pedidosEntregados.Peek();
        }
        return null;
    }

    public int CantidadPedidosEntregados()
    {
        return pedidosEntregados.Count;
    }
}
