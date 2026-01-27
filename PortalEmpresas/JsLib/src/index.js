import { getCurrentTime } from "./time_lib";
import { generarPDF } from "./jsPdf_lib";
import { generarContrasena } from "./contraseñaGen";
import { redirectBlank } from "./Redirect_Blank";
import { writeCookie, readCookie , deleteCookie} from "./cookies";
import { wasmReload } from "./utilidades";
import { generarPDFCheques } from "./jsPdf_lib";
import { getJsonFromUrl, getJsonFromUrlOtherTimeOut, postJsonToUrl, postJsonToUrlOtherTimeOut } from "./consult_Api";
import { scrollBottom, scrollTop, scroll} from "./scroll";
import { generarPDFCotizacion , obtenerPDFCotizacionFile} from "./jsPdf_Corizacion";
import { mostrarPdfEnIframe } from "./pdfViewer";
import { focuselement, setValue, isFocused } from "./utility";

//prueba getCurrentTime
export function GetCurrentTime() {
  return getCurrentTime();
}

//Para generar PDF, se Puede agregar mas funciones en src jsPdf_lib
export function GenerarPDF(
  nombreDelArchivo,
  datosCliente,
  Elements,
  cabecera,
  selectedDireccionFact
) {
  return generarPDF(
    nombreDelArchivo,
    datosCliente,
    Elements,
    cabecera,
    selectedDireccionFact
  );
}

export function GenerarPDFCheques(Cheques) {
  return generarPDFCheques(Cheques);
}

//genera una contraseña segun el largo que se le de sin simbolos
export function GenerarContrasena(longitud) {
  return generarContrasena(longitud);
}

//Redirige a una pafina en blanco segun el url que se le de
export function RedirectBlank(url) {
  return redirectBlank(url);
}

//para generar cookies(se le manda el nombre del valor, el valor y el tiempo de duracion) y leerlas
//Todo: Migrar a Typescript para generar interfaces y generar mayor seguridad
export function WriteCookie(name, value, days) {
  return writeCookie(name, value, days);
}

export function ReadCookie(cname) {
  return readCookie(cname);
}

export function WasmReload() {
  return wasmReload();
}

export function GetJsonFromUrl(url, headers = {}) {
  return getJsonFromUrl(url, headers);
}

export function GetJsonFromUrlOtherTimeOut(url, headers = {}) {
  return getJsonFromUrlOtherTimeOut(url, headers);
} 

export function PostJsonToUrl(url, data, headers = {}) {
  return postJsonToUrl(url, data, headers);
} 

export function PostJsonToUrlOtherTimeOut(url, data, headers = {}) {
  return postJsonToUrlOtherTimeOut(url, data, headers);
}

export function ScrollBottom(element) {
 return scrollBottom(element);
}

export function ScrollTop(element) {
 return scrollTop(element);
}

export function Scroll(id) {
 return scroll(id);
}

export function BorrarCookies(cname) {
  return deleteCookie(cname);
}
export function GenerarPDFCotizacion() {
  return generarPDFCotizacion();
}
export function ObtenerPDFCotizacionFile(cliente, Vendedor , productos ,ResponseCotizacionIdoperacion, busquedaResultIdoperacion, totales ) {
  return obtenerPDFCotizacionFile(cliente, Vendedor , productos ,ResponseCotizacionIdoperacion, busquedaResultIdoperacion, totales );
}

export function MostrarPdfEnIframe(base64, id) {
  return mostrarPdfEnIframe(base64, id);
}
export function Focuselement(elementId) {
  return focuselement(elementId);
}
export function IsFocused(elementId) {
  return isFocused(elementId);
}
export function SetValue(elementId,value) {
  return setValue(elementId,value);
}