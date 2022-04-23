using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRuebaweb.Models
{
    public class Paciente
    {
        private string id;
        private string nombre;
        private string apellidos;
        private DateTime fechnacimiento;
        private int regimen;
        private string semCotizadas;
        private DateTime fechIngresoSistema;
        private DateTime fechIngresoEPS;
        private int eps;
        private string hisClinica;
        private int numEnfermedades;
        private string enfermedadRele;
        private int afiliacion;
        private double costTratamientos;

        public Paciente(string id, string nombre, string apellidos, DateTime fechnacimiento, int regimen,
            string semCotizadas, DateTime fechIngresoSistema, DateTime fechIngresoEPS, int eps, 
            string hisClinica, int numEnfermedades, string enfermedadRele, int afiliacion, double costTratamientos)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.fechnacimiento = fechnacimiento;
            this.regimen = regimen;
            this.semCotizadas = semCotizadas;
            this.fechIngresoSistema = fechIngresoSistema;
            this.fechIngresoEPS = fechIngresoEPS;
            this.eps = eps;
            this.hisClinica = hisClinica;
            this.numEnfermedades = numEnfermedades;
            this.enfermedadRele = enfermedadRele;
            this.afiliacion = afiliacion;
            this.costTratamientos = costTratamientos;
        }

        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public DateTime Fechnacimiento { get => fechnacimiento; set => fechnacimiento = value; }
        public int Regimen { get => regimen; set => regimen = value; }
        public string SemCotizadas { get => semCotizadas; set => semCotizadas = value; }
        public DateTime FechIngresoSistema { get => fechIngresoSistema; set => fechIngresoSistema = value; }
        public DateTime FechIngresoEPS { get => fechIngresoEPS; set => fechIngresoEPS = value; }
        public int Eps { get => eps; set => eps = value; }
        public string HisClinica { get => hisClinica; set => hisClinica = value; }
        public int NumEnfermedades { get => numEnfermedades; set => numEnfermedades = value; }
        public string EnfermedadRele { get => enfermedadRele; set => enfermedadRele = value; }
        public int Afiliacion { get => afiliacion; set => afiliacion = value; }
        public double CostTratamientos { get => costTratamientos; set => costTratamientos = value; }
    }
}