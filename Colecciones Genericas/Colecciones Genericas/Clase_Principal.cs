using Colecciones_Genericas;
using System;

public class RestauranteApp
{
    private MenuRestaurante menu;
    private GestionadorPedidos gestionadorPedidos;
    private HistorialPedidos historialPedidos;

    public RestauranteApp()
    {
        menu = new MenuRestaurante();
        gestionadorPedidos = new GestionadorPedidos();
        historialPedidos = new HistorialPedidos();

        // Inicializar con datos de ejemplo
        menu.InicializarMenuDefault();
    }

    public void Ejecutar()
    {
        Console.WriteLine("=== SISTEMA DE GESTIÓN DE RESTAURANTE ===");
        Console.WriteLine("Bienvenido al sistema de gestión de pedidos\n");

        while (true)
        {
            MostrarMenuPrincipal();
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarMenu();
                    break;
                case "2":
                    GestionarPedidos();
                    break;
                case "3":
                    ProcesarPedidos();
                    break;
                case "4":
                    MostrarEstadisticas();
                    break;
                case "5":
                    Console.WriteLine("¡Gracias por usar el sistema! Hasta pronto.");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private void MostrarMenuPrincipal()
    {
        Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1.Gestionar Menú");
        Console.WriteLine("2.Gestionar Pedidos");
        Console.WriteLine("3.Procesar Pedidos");
        Console.WriteLine("4.Mostrar Estadísticas");
        Console.WriteLine("5.Salir");
        Console.Write("Seleccione una opción: ");
    }

    private void GestionarMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== GESTIÓN DE MENÚ ===");
            Console.WriteLine("1. Listar platillos");
            Console.WriteLine("2. Agregar platillo");
            Console.WriteLine("3. Eliminar platillo");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    menu.ListarPlatillos();
                    break;
                case "2":
                    AgregarNuevoPlatillo();
                    break;
                case "3":
                    EliminarPlatillo();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    private void AgregarNuevoPlatillo()
    {
        Console.Write("Nombre del platillo: ");
        var nombre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        Console.Write("Categoría: ");
        var categoria = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(categoria))
        {
            Console.WriteLine("La categoría no puede estar vacía.");
            return;
        }

        Console.Write("Precio: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal precio) && precio > 0)
        {
            var platillo = new Platillos(nombre, categoria, precio);
            menu.AgregarPlatillo(platillo);
        }
        else
        {
            Console.WriteLine("Precio no válido. Debe ser un número mayor a 0.");
        }
    }

    private void EliminarPlatillo()
    {
        Console.Write("Nombre del platillo a eliminar: ");
        var nombre = Console.ReadLine();
        menu.EliminarPlatillo(nombre);
    }

    private void GestionarPedidos()
    {
        while (true)
        {
            Console.WriteLine("\n=== GESTIÓN DE PEDIDOS ===");
            Console.WriteLine("1. Crear nuevo pedido");
            Console.WriteLine("2. Mostrar pedidos en espera");
            Console.WriteLine("3. Mostrar próximo cliente");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearNuevoPedido();
                    break;
                case "2":
                    gestionadorPedidos.MostrarPedidosEnEspera();
                    break;
                case "3":
                    gestionadorPedidos.MostrarProximoCliente();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    private void CrearNuevoPedido()
    {
        Console.Write("Nombre del cliente: ");
        var cliente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(cliente))
        {
            Console.WriteLine("El nombre del cliente no puede estar vacío.");
            return;
        }

        var numeroPedido = gestionadorPedidos.ObtenerNumeroPedido();
        var pedido = new Pedido(numeroPedido, cliente);

        Console.WriteLine("\nPlatillos disponibles:");
        menu.ListarPlatillos();

        Console.WriteLine("\nAgregar platillos al pedido (escribe 'fin' para terminar):");

        while (true)
        {
            Console.Write("Ingrese el nombre del platillo: ");
            var nombrePlatillo = Console.ReadLine();

            if (nombrePlatillo.ToLower() == "fin")
                break;

            var platillo = menu.BuscarPlatillo(nombrePlatillo);
            if (platillo != null)
            {
                pedido.AgregarPlatillo(platillo);
                Console.WriteLine($"✓ {platillo.Nombre} agregado al pedido");
            }
            else
            {
                Console.WriteLine("✗ Platillo no encontrado en el menú.");
            }
        }

        if (pedido.Platillos.Count > 0)
        {
            gestionadorPedidos.AgregarPedido(pedido);
            Console.WriteLine($"\n✅ Pedido #{pedido.NumeroPedido} creado exitosamente!");
            Console.WriteLine($"Total: ${pedido.CalcularTotal()}");
        }
        else
        {
            Console.WriteLine("El pedido no tiene platillos. No se agregará a la cola.");
        }
    }

    private void ProcesarPedidos()
    {
        Console.WriteLine("\n=== PROCESAMIENTO DE PEDIDOS ===");

        if (!gestionadorPedidos.TienePedidos())
        {
            Console.WriteLine("No hay pedidos para procesar.");
            return;
        }

        Console.WriteLine($"Procesando {gestionadorPedidos.CantidadPedidosEnEspera()} pedidos...\n");

        while (gestionadorPedidos.TienePedidos())
        {
            var pedido = gestionadorPedidos.ProcesarSiguientePedido();
            if (pedido != null)
            {
                Console.WriteLine($"⏳ Procesando: {pedido}");

                // Simular tiempo de preparación
                System.Threading.Thread.Sleep(1500);

                historialPedidos.AgregarPedidoEntregado(pedido);
            }
        }

        Console.WriteLine("\nTodos los pedidos han sido procesados y entregados!");
        historialPedidos.MostrarUltimosPedidos();
    }

    private void MostrarEstadisticas()
    {
        Console.WriteLine("\n=== ESTADÍSTICAS DEL SISTEMA ===");
        Console.WriteLine($"Platillos en menú: {menu.CantidadPlatillos()}");
        Console.WriteLine($"Pedidos en espera: {gestionadorPedidos.CantidadPedidosEnEspera()}");
        Console.WriteLine($"Pedidos entregados: {historialPedidos.CantidadPedidosEntregados()}");

        var ultimoPedido = historialPedidos.ObtenerUltimoPedidoEntregado();
        if (ultimoPedido != null)
        {
            Console.WriteLine($"\nÚltimo pedido entregado:");
            Console.WriteLine($"   • Número: #{ultimoPedido.NumeroPedido}");
            Console.WriteLine($"   • Cliente: {ultimoPedido.Cliente}");
            Console.WriteLine($"   • Total: ${ultimoPedido.CalcularTotal()}");
        }
    }
}