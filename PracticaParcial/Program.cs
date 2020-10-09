using Entity;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcial
{
    class Program
    {
        static void Main(string[] args)
        {
            String identificacion = String.Empty;
            String nombre = String.Empty;
            String tipoDeAfilacion = String.Empty;
            double salarioPaciente = 0;
            double valorServicio = 0;
            LiquidacionCuotaModeradora liquidacion;
            LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService;
            liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();


            char opcion;

            Console.WriteLine("\t MENU");
            Console.WriteLine("1. Ingresar liquidacion");
            //agregar a consulta liquidacion todas las ramas distintas de consulta de esta misma 
            Console.WriteLine("2. Consultar liquidaciones");
            Console.WriteLine("3. Eliminar liquidaciones");
            Console.WriteLine("4. Salir");
            opcion = Convert.ToChar(Console.ReadLine());

            switch (opcion) {
                case '1':
                    Console.Write("Numero identificacion: ");
                    identificacion = Console.ReadLine();

                    if (liquidacionCuotaModeradoraService.Buscar(identificacion).Verificacion)
                    {
                        Console.Write("Nombre: ");
                        nombre = Console.ReadLine();
                        Console.Write("Tipo de afiliacion: ");
                        tipoDeAfilacion = Console.ReadLine();
                        Console.Write("Salario paciente: ");
                        salarioPaciente = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Costo del servicio: ");
                        valorServicio = Convert.ToDouble(Console.ReadLine());
                        liquidacion = new LiquidacionCuotaModeradora(nombre, tipoDeAfilacion, salarioPaciente, valorServicio);
                        liquidacion.NumeroId = identificacion;
                        Console.Write(liquidacionCuotaModeradoraService.Guardar(liquidacion));
                    } else
                    {
                        liquidacion = liquidacionCuotaModeradoraService.Buscar(identificacion).liquidacion;
                        Console.WriteLine($"nombre: {liquidacion.NombrePaciente}");
                        Console.WriteLine($"Tipo de afiliacion: {liquidacion.TipoDeAfiliacion}");
                        Console.WriteLine($"Salario paciente: {liquidacion.SalarioPaciente}");
                        Console.Write("Costo del servicio: ");
                        valorServicio = Convert.ToDouble(Console.ReadLine());
                        liquidacion.ValorServicio = valorServicio;
                        liquidacion.NumeroId = identificacion;
                        Console.Write(liquidacionCuotaModeradoraService.Guardar(liquidacion));
                    }


                    break;

                case '2':
                    List<LiquidacionCuotaModeradora> liquidaciones = liquidacionCuotaModeradoraService.ConsultaTotal();
                    foreach (var item in liquidaciones)
                    {
                        Console.WriteLine($"Numero de identificaicon: {item.NumeroId}");
                        Console.WriteLine($"Nombre paciente: {item.NombrePaciente}");
                        Console.WriteLine($"Salario del paciente: {item.SalarioPaciente}");
                        Console.WriteLine($"Tipo de afiliacion: {item.TipoDeAfiliacion}");
                        Console.WriteLine($"Fecha de liquidacion: {item.Fecha}");
                        Console.WriteLine($"Costo del servicio: {item.ValorServicio}");
                        Console.WriteLine($"valor a pagar: {item.CostoLiquidacion}");
                        Console.WriteLine($"Numero de liquidacion: {item.NumeroLiquidacion}\n\n");
                    }
                    break;

                case '3':
                    int numeroLiquidacion = 0;
                    Console.Write("Digite el numero de liquidacion que desea eliminar: ");
                    numeroLiquidacion = Convert.ToInt32(Console.ReadLine());
                    Console.Write(liquidacionCuotaModeradoraService.EliminarLiquidacion(numeroLiquidacion));
                    break;

                case '4':
                    break;
            }

            Console.ReadKey();
        }
    }
}
