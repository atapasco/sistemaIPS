using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LiquidacionCuotaModeradoraService
    {

        double salarioMinimo = 877803;
        private LiquidacionCuotaModeradoraRepository liquidacionCuotaModeradoraRepository;

        public LiquidacionCuotaModeradoraService()
        {
            liquidacionCuotaModeradoraRepository = new LiquidacionCuotaModeradoraRepository();
        }

        public String Guardar(LiquidacionCuotaModeradora liquidacion)
        {
            try
            {
                CalcularCuotaModeradora(liquidacion);
                liquidacionCuotaModeradoraRepository.Guardar(liquidacion);
                return "se guardo correctamente";
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    
        public BuscarPersona Buscar(String id)
        {
            List<LiquidacionCuotaModeradora> liquidaciones = liquidacionCuotaModeradoraRepository.ConsultaGeneral();
            BuscarPersona buscarPersona = new BuscarPersona();
            foreach (var liquidacion in liquidaciones)
            {
                if (liquidacion.NumeroId == id)
                {
                    
                    buscarPersona.Verificacion = false;
                    buscarPersona.nombre = liquidacion.NombrePaciente;

                    return buscarPersona;
                }
            }
            buscarPersona.Verificacion = true;
            buscarPersona.nombre = null;

            return buscarPersona;
        }


        public List<LiquidacionCuotaModeradora> ConsultaTotal()
        {
            return liquidacionCuotaModeradoraRepository.ConsultaGeneral();
        }

        public double CalcularCuotaModeradora(LiquidacionCuotaModeradora liquidacion)
        {
            if ((liquidacion.SalarioPaciente < salarioMinimo * 2) && (liquidacion.TipoDeAfiliacion.Equals("contributivo")))
            {
                liquidacion.CostoLiquidacion = liquidacion.ValorServicio * 0.15;
                if (liquidacion.CostoLiquidacion > 250000)
                {
                    liquidacion.CostoLiquidacion = 250000;
                }
            } else if ((liquidacion.SalarioPaciente >= salarioMinimo * 2) && (liquidacion.SalarioPaciente <= salarioMinimo * 5) && (liquidacion.TipoDeAfiliacion.Equals("contributivo")))
            {
                liquidacion.CostoLiquidacion = liquidacion.ValorServicio * 0.20;
                if (liquidacion.CostoLiquidacion > 900000)
                {
                    liquidacion.CostoLiquidacion = 900000;
                }
            } else if ((liquidacion.SalarioPaciente > salarioMinimo * 5) && (liquidacion.TipoDeAfiliacion.Equals("contributivo")))
            {
                liquidacion.CostoLiquidacion = liquidacion.ValorServicio * 0.25;
                if (liquidacion.CostoLiquidacion > 1500000)
                {
                    liquidacion.CostoLiquidacion = 1500000;
                }
            } else if ((liquidacion.SalarioPaciente == 0) && (liquidacion.TipoDeAfiliacion.Equals("subsidiado")))
            {
                liquidacion.CostoLiquidacion = liquidacion.ValorServicio * 0.05;
                if (liquidacion.CostoLiquidacion > 200000)
                {
                    liquidacion.CostoLiquidacion = 200000;
                }
            }

            return liquidacion.CostoLiquidacion;
        }

        public class BuscarPersona
        {
            public BuscarPersona()
            {
            }

            public bool Verificacion { set; get; }
            public String nombre { set; get; }
        }
    }
}
