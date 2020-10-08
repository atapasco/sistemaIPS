using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LiquidacionCuotaModeradora
    {

        public LiquidacionCuotaModeradora(String nombrePaciente, String tipoAfiliacion,double salarioPaciente, double valorServicio)
        {
            DateTime fecha = DateTime.Today;
            NombrePaciente = nombrePaciente;
            ValorServicio = valorServicio;
            TipoDeAfiliacion = tipoAfiliacion;
            SalarioPaciente = salarioPaciente;
            Fecha = fecha.ToShortDateString();
        }
        public LiquidacionCuotaModeradora()
        {

        }

        public int NumeroLiquidacion { set; get; }
        public String NumeroId { set; get; }
        public String NombrePaciente { set; get; }
        public String TipoDeAfiliacion { set; get; }
        public double SalarioPaciente { set; get; }
        public double ValorServicio { set; get; }
        public String Fecha { set; get; }
        public double CostoLiquidacion { set; get; }

         
        public override String ToString()
        {
            return $"Identificacion: {NumeroId}\nNombre: {NombrePaciente}\n" +
                   $"Tipo de afiliacion: {TipoDeAfiliacion}\nSalario: {SalarioPaciente}\n" +
                   $"Costo Servicio: {ValorServicio}\nNumero de Liquidacion: {NumeroLiquidacion}\n" +
                   $"Fecha: {Fecha}\nCosto Liquidacion: {CostoLiquidacion}";
        }
    }
}
