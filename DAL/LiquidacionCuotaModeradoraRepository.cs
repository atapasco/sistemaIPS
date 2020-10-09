using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entity;

namespace DAL
{
    public class LiquidacionCuotaModeradoraRepository
    {
        public void Guardar(LiquidacionCuotaModeradora persona)
        {
            TextWriter escribirArchivo;
            persona.NumeroLiquidacion = NumeroDeLiquidacion();
            FileStream file = new FileStream("DatosLiquidacion.txt", FileMode.Append);
            escribirArchivo = new StreamWriter(file);
            escribirArchivo.WriteLine($"{persona.NumeroId};{persona.NombrePaciente};{persona.SalarioPaciente};" +
                                      $"{persona.TipoDeAfiliacion};{persona.CostoLiquidacion};" +
                                      $"{persona.NumeroLiquidacion};{persona.Fecha};{persona.ValorServicio}");
            escribirArchivo.Close();
        }

        public List<LiquidacionCuotaModeradora> ConsultaGeneral()
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            FileStream file = new FileStream("DatosLiquidacion.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {
                LiquidacionCuotaModeradora liquidacion = Organizador(linea);
                liquidaciones.Add(liquidacion); 
            }
            reader.Close();
            file.Close();
            return liquidaciones;
        }

        public int NumeroDeLiquidacion()
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            liquidaciones = ConsultaGeneral();
            int numeroDeLiquidacion = 0;
            foreach(var liquidacion in liquidaciones)
            {
                numeroDeLiquidacion = liquidacion.NumeroLiquidacion;
            }
            return numeroDeLiquidacion + 1;
        }

        public LiquidacionCuotaModeradora Organizador(String linea)
        {
            LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora();
            string[] matrizPersona = linea.Split(';');
            liquidacion.NumeroId = matrizPersona[0];
            liquidacion.NombrePaciente = matrizPersona[1];
            liquidacion.SalarioPaciente = Convert.ToDouble(matrizPersona[2]);
            liquidacion.TipoDeAfiliacion = matrizPersona[3];
            liquidacion.CostoLiquidacion = Convert.ToDouble(matrizPersona[4]);
            liquidacion.NumeroLiquidacion = Convert.ToInt32(matrizPersona[5]);
            liquidacion.Fecha = matrizPersona[6];
            liquidacion.ValorServicio = Convert.ToDouble(matrizPersona[7]);

            return liquidacion;
        }

        public String EliminarLiquidacion(int numeroDeLiquidacion)
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            liquidaciones = ConsultaGeneral();
            FileStream file = new FileStream("DatosLiquidacion.txt", FileMode.Create);
            file.Close();
            foreach (var liquidacion in liquidaciones)
            {
                if (numeroDeLiquidacion != liquidacion.NumeroLiquidacion)
                {
                    Guardar(liquidacion);
                }
            }

            foreach (var liquidacion in liquidaciones)
            {
                if (numeroDeLiquidacion == liquidacion.NumeroLiquidacion)
                {
                    return "Liquidacion Eliminada";
                }
            }

            return "Error numero de liquidacion no encontrado, por favor intente nuevamente";
        }
    }
}
