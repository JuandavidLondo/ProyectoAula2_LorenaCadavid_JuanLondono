using PRuebaweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRuebaweb.Controllers
{
    public class PacienteController : Controller
    {
        public static int ContadorSaludables = 0, contContri = 0, contSubsi = 0, ContadorCotizantes = 0, ContadorBeneficiarios = 0,CantidadPacientes = 0,ContadorCancer = 0;
        public static int contNinos = 0, contAdole = 0, contJoven = 0, contAdulto = 0, contAdultMayor = 0, contAnciano = 0;
        public static string MayorCosto = "";
        public static List<Paciente> paciente = new List<Paciente>();
        public static DateTime fechaActual = DateTime.Now;
        public static double CostoSura = 0, CostoNueva = 0, CostoSalud = 0, CostoSanitas = 0, CostoSavia = 0, NumeroMayorCosto = 0, anos = 0, dias = 0, CostoTotal = 0;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Paciente()
        {
            try
            {
                string id, nombre, apellidos, semCotizadas, hisClinica, enfermedadRele;
                DateTime fechnacimiento, fechIngresoEPS, fechIngresoSistema;
                int regimen, eps, numEnfermedades, afiliacion;
                double costTratamientos;
                id = Request.Form["id"].ToString();
                nombre = Request.Form["nombre"].ToString();
                apellidos = Request.Form["apellidos"].ToString();
                fechnacimiento = Convert.ToDateTime(Request.Form["fechaNaci"]);
                if (nombre == " " || id == " " || apellidos == " " || fechnacimiento == null)
                {
                    ViewData["mensaje"] = "Faltan Datos Necesarios";
                }
                else
                {


                    TimeSpan diferencia = fechaActual - fechnacimiento;
                    dias = diferencia.TotalDays;
                    anos = Math.Floor(dias / 365);
                    if (anos <= 12)
                    {
                        contNinos++;
                    }
                    else if (anos <= 18)
                    {
                        contAdole++;
                    }
                    else if (anos <= 30)
                    {
                        contJoven++;
                    }
                    else if (anos <= 55)
                    {
                        contAdulto++;
                    }
                    else if (anos <= 75)
                    {
                        contAdultMayor++;
                    }
                    else if (anos > 75)
                    {
                        contAnciano++;
                    }
                    regimen = Convert.ToInt16(Request.Form["seleccionarRegimen"]);
                    if (regimen == 1)
                    {
                        contContri++;
                    }
                    else if (regimen == 2)
                    {
                        contSubsi++;
                    }
                    semCotizadas = Request.Form["semCoti"].ToString();
                    fechIngresoSistema = Convert.ToDateTime(Request.Form["fechIngrSis"]);
                    fechIngresoEPS = Convert.ToDateTime(Request.Form["fechIngrEps"]);
                    eps = Convert.ToInt16(Request.Form["seleccionarEps"]);
                    hisClinica = Request.Form["histoClini"].ToString();
                    numEnfermedades = Convert.ToInt16(Request.Form["cantEnfer"]);
                    if (numEnfermedades == 0)
                    {
                        ContadorSaludables++;
                    }
                    enfermedadRele = Request.Form["enfeRele"].ToString();
                    if (enfermedadRele == "cancer")
                    {
                        ContadorCancer++;
                    }
                    afiliacion = Convert.ToInt16(Request.Form["seleccionarAfiliacion"]);
                    if (afiliacion == 1)
                    {
                        ContadorCotizantes++;
                    }
                    else if (afiliacion == 2)
                    {
                        ContadorBeneficiarios++;
                    }
                    costTratamientos = Convert.ToDouble(Request.Form["costTrata"]);
                    CostoTotal += costTratamientos;
                    if (eps == 1)
                    {
                        CostoSura += costTratamientos;
                    }
                    else if (eps == 2)
                    {
                        CostoNueva += costTratamientos;
                    }
                    else if (eps == 3)
                    {
                        CostoSalud += costTratamientos;
                    }
                    else if (eps == 4)
                    {
                        CostoSanitas += costTratamientos;
                    }
                    else
                    {
                        CostoSavia += costTratamientos;
                    }
                    if (costTratamientos > NumeroMayorCosto)
                    {
                        NumeroMayorCosto = costTratamientos;
                        MayorCosto = id + " " + nombre + " " + apellidos;
                    }


                    paciente.Add(new Paciente(id, nombre, apellidos, fechnacimiento, regimen, semCotizadas, fechIngresoSistema,
                        fechIngresoEPS, eps, hisClinica, numEnfermedades, enfermedadRele, afiliacion, costTratamientos));
                    CantidadPacientes++;
                }
            }
            catch (FormatException)
            {
                return View("Error_Formato");
            }
            return View("Index");
        }
        public ActionResult Index_cambiar_Eps()
        {
            return View();
        }
        public ActionResult Cambiar_Eps()
        {
           
            Paciente pacienteactual;
            double dias, meses;
            string buscid, mensaje;
            buscid = Request.Form["buscId"].ToString();
            if (buscid == " ")
            {
                ViewData["mensaje"] = "Debe ingresar un ID";
            }
            else
            {
                try
                {
                    pacienteactual = paciente.Find(paciente => paciente.Id.Contains(buscid));
                    TimeSpan diferencia = fechaActual - pacienteactual.FechIngresoEPS;
                    dias = diferencia.TotalDays;
                    meses = dias / 30;
                    if (meses >= 3)
                    {
                        pacienteactual.Eps = Convert.ToInt16(Request.Form["NuevEps"]);
                        ViewData["mensaje"] = "Eps Cambiada con exito";
                    }
                    else if (meses < 3)
                    {
                        ViewData["mensaje"] = "este paciente no puede cambiar de eps";
                    }
                }
                catch (NullReferenceException)
                {
                    return View("Error_Nulo");
                }
                
                
            }
            
            return View("Index_cambiar_Eps");
        }
        public ActionResult Index_cambiar_Regimen()
        {
            return View();
        }
        public ActionResult Cambiar_Regimen()
        {
            try 
            { 
            Paciente pacienteactual;
            string buscid = "";
            int NuevoRegimen = 0;
            buscid =Request.Form["buscarId"].ToString();
                if (buscid == " ")
                {
                    ViewData["mensaje"] = "Debe ingresar un ID";
                }
                else
                {
                    pacienteactual = paciente.Find(paciente => paciente.Id.Contains(buscid));
                    if (pacienteactual.Regimen == 1)
                    {
                        contContri--;
                    }
                    else if (pacienteactual.Regimen == 2)
                    {
                        contSubsi--;
                    }
                    NuevoRegimen = Convert.ToInt16(Request.Form["CambioRegimen"]);
                    if (NuevoRegimen == 1)
                    {
                        contContri++;
                    }
                    else if (NuevoRegimen == 2)
                    {
                        contSubsi++;
                    }
                    pacienteactual.Regimen = NuevoRegimen;
                    ViewData["mensaje"] = "Operacion Exitosa";
                }    
            }
            catch (Exception)
            {
                return View("Error_Nulo");
            }
            return View("Index_cambiar_Regimen");
        }
        public ActionResult Index_cambiar_Historia_Clinica()
        {
            return View();
        }
        public ActionResult Cambiar_Historia_Clinica()
        {
            Paciente pacienteactual;
            string buscid = "";
            buscid = Request.Form["buscaId"].ToString();
            if (buscid == " ")
            {
                ViewData["mensaje"] = "Debe ingresar un ID";
            }
            else
            {
                try
                {
                    pacienteactual = paciente.Find(paciente => paciente.Id.Contains(buscid));
                    pacienteactual.HisClinica = Request.Form["NuevaHistoria"].ToString();
                    if (pacienteactual.Eps == 1)
                    {
                        CostoSura -= pacienteactual.CostTratamientos;
                        CostoTotal -= pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 2)
                    {
                        CostoNueva -= pacienteactual.CostTratamientos;
                        CostoTotal -= pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 3)
                    {
                        CostoSalud -= pacienteactual.CostTratamientos;
                        CostoTotal -= pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 4)
                    {
                        CostoSanitas -= pacienteactual.CostTratamientos;
                        CostoTotal -= pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 5)
                    {
                        CostoSavia -= pacienteactual.CostTratamientos;
                        CostoTotal -= pacienteactual.CostTratamientos;
                    }
                    pacienteactual.CostTratamientos = Convert.ToDouble(Request.Form["NuevoCosto"]);
                    if (pacienteactual.Eps == 1)
                    {
                        CostoSura += pacienteactual.CostTratamientos;
                        CostoTotal += pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 2)
                    {
                        CostoNueva += pacienteactual.CostTratamientos;
                        CostoTotal += pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 3)
                    {
                        CostoSalud += pacienteactual.CostTratamientos;
                        CostoTotal += pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 4)
                    {
                        CostoSanitas += pacienteactual.CostTratamientos;
                        CostoTotal += pacienteactual.CostTratamientos;
                    }
                    else if (pacienteactual.Eps == 5)
                    {
                        CostoSavia += pacienteactual.CostTratamientos;
                        CostoTotal += pacienteactual.CostTratamientos;
                    }
                    ViewData["mensaje"] = "Cambio Realizado Con Exito";
                }
                catch (NullReferenceException)
                {
                    return View("Error_Nulo");
                }
            }
            return View("Index_cambiar_Historia_Clinica");
        }
        public ActionResult Mostrar_Estadisticas()
        {
            try
            {
                ViewData["PorcentajeSura"] = "Sura:" + (CostoSura * 100) / CostoTotal + "%";
                ViewData["PorcentajeNueva"] = "Nueva Eps: " + (CostoNueva * 100) / CostoTotal + "%";
                ViewData["PorcentajeSalud"] = "Salud Total: " + (CostoSalud * 100) / CostoTotal + "%";
                ViewData["PorcentajeSanitas"] = "Sanitas: " + (CostoSanitas * 100) / CostoTotal + "%";
                ViewData["PorcentajeSavia"] = "Savia Salud: " + (CostoSavia * 100) / CostoTotal + "%";
                ViewData["CostoSura"] = "Sura: " + CostoSura;
                ViewData["CostoNueva"] = "Nueva Eps: " + CostoNueva;
                ViewData["CostoSalud"] = "Salud Total: " + CostoSalud;
                ViewData["CostoSanitas"] = "Sanitas: " + CostoSanitas;
                ViewData["CostoSavia"] = "Savia Salud: " + CostoSavia;
                ViewData["PacientesSaludables"] = "El porcentaje de pasientes saludables es de: " + (ContadorSaludables * 100) / CantidadPacientes + "%";
                ViewData["MayorCosto"] = "El paciente con mayor costo en sus tratamientos es: " + MayorCosto + " con un costo de: " + NumeroMayorCosto;
                ViewData["PorcentajeNinos"] = "El porcentaje de niños es de: " + (contNinos * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeAdolecentes"] = "El porcentaje de adolecentes es de: " + (contAdole * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeJoven"] = "El porcentaje de jovenes es de: " + (contJoven * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeAdultos"] = "El porcentaje de adultos es de: " + (contAdulto * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeAdultoMayor"] = "El porcentaje de adultos mayores es de: " + (contAdultMayor * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeAnciano"] = "El porcentaje de ancianos es de: " + (contAnciano * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeContributivo"] = "Contributivo:" + (contContri * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeSubsidiado"] = "Subsidiado:" + (contSubsi * 100) / CantidadPacientes + "%";
                ViewData["PorcentajeCotizante"] = "Cotizante:" + (ContadorCotizantes * 100) / CantidadPacientes + "%";
                ViewData["Porcentajebeneficiario"] = "Beneficiario:" + (ContadorBeneficiarios * 100) / CantidadPacientes + "%";
                ViewData["CantidadCancer"] = "La cantidad de pasientes que padecen de Cancer es de :" + ContadorCancer;
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
            return View();
        }   
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Error_Nulo()
        {
            return View();
        }
        public ActionResult Error_Formato()
        {
            return View();
        }

    }
}