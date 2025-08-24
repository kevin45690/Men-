using Colecciones_Genericas;
using System;
using System.Collections.Generic;

public class GestionadorPedidos
{
    private Queue<Pedido> pedidosEnEspera;
    private int contadorPedidos;

    public GestionadorPedidos()
    {
        pedidosEnEspera = new Queue<Pedido>();
        contadorPedidos = 1;
    }

    public void AgregarPedido(Pedido pedido)
    {
        pedidosEnEspera.Enqueue(pedido);
        Console.WriteLine($"✓ Pedido agregado a la cola: #{pedido.NumeroPedido} - {pedido.Cliente}");
    }

    public Pedido ProcesarSiguientePedido()
    {
        if (pedidosEnEspera.Count > 0)
        {
            return pedidosEnEspera.Dequeue();
        }
        return null;
    }

    public void MostrarProximoCliente()
    {
        if (pedidosEnEspera.Count > 0)
        {
            var proximo = pedidosEnEspera.Peek();
            Console.WriteLine($"\nPróximo cliente en turno:");
            Console.WriteLine($"• Cliente: {proximo.Cliente}");
            Console.WriteLine($"• Pedido: #{proximo.NumeroPedido}");
            Console.WriteLine($"• Total: ${proximo.CalcularTotal()}");
        }
        else
        {
            Console.WriteLine("No hay pedidos en espera.");
        }
    }

    public void MostrarPedidosEnEspera()
    {
        Console.WriteLine("\n=== PEDIDOS EN ESPERA ===");
        if (pedidosEnEspera.Count == 0)
        {
            Console.WriteLine("No hay pedidos en espera.");
            return;
        }

        int i = 1;
        foreach (var pedido in pedidosEnEspera)
        {
            Console.WriteLine($"{i}. {pedido}");
            i++;
        }
    }

    public int ObtenerNumeroPedido()
    {
        return contadorPedidos++;
    }

    public int CantidadPedidosEnEspera()
    {
        return pedidosEnEspera.Count;
    }

    public bool TienePedidos()
    {
        return pedidosEnEspera.Count > 0;
    }
}
