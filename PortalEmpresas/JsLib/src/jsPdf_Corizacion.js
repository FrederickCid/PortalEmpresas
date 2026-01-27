import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";
import { formatCLP } from "./utilidades";


/**
 * Descarga el PDF de cotización.
 */
export async function generarPDFCotizacion() {
  const doc = crearCotizacionPDF();
  doc.save("cotizacion_profesional.pdf");
}

/**
 * Envía el PDF de cotización por correo electrónico.
 * Modifica el endpoint y lógica según tu backend.
 */
export function obtenerPDFCotizacionFile(cliente, Vendedor , productos ,ResponseCotizacionIdoperacion, busquedaResultIdoperacion, totales) {
    const doc = crearCotizacionPDF(cliente, Vendedor, productos, ResponseCotizacionIdoperacion, busquedaResultIdoperacion, totales);
  const dataUri = doc.output('datauristring');
  const base64 = dataUri.split(',')[1];
  return base64;
}

/**
 * Crea y retorna el objeto jsPDF con la cotización.
 */
export function crearCotizacionPDF(cliente, Vendedor , productos ,ResponseCotizacionIdoperacion, busquedaResultIdoperacion, totales ) {
  // Datos de ejemplo
  const numero = String((ResponseCotizacionIdoperacion?.nroDocumento > 0 ? ResponseCotizacionIdoperacion?.nroDocumento : busquedaResultIdoperacion?.nroDocumento)) || "0001";
  const fecha = "08-07-2025";
  const idOperacion = String(ResponseCotizacionIdoperacion?.idOperacion > 0 ? ResponseCotizacionIdoperacion?.idOperacion  : busquedaResultIdoperacion?.idOperacion) ;

  const doc = new jsPDF({
    orientation: "portrait",
    unit: "mm",
    format: "letter",
  });

  // ENCABEZADO
  const imageUrl = "https://i.imgur.com/jP1J8um.png";
  doc.addImage(imageUrl, 'PNG', -8, -19, 80, 60);
  doc.setFontSize(16).setFont('helvetica', 'bold');
  doc.text(`COTIZACIÓN NRO. ${numero}`, 70, 15);

  // Código de barra en la esquina superior derecha
  doc.setFontSize(20).setFont('courier', 'bold');
  doc.text(idOperacion, 200, 15, { align: 'right' });

  doc.setFontSize(10).setFont('helvetica', 'normal');
  doc.text("CLIENTE:", 10, 25);
  doc.setFont('helvetica', 'bold');
  doc.text(`${cliente.idCliente} - ${cliente.nombre}`, 30, 25);

  // doc.setFont('helvetica', 'normal');
  // doc.text("FECHA COTIZACIÓN:", 10, 32);
  // doc.text(fecha, 50, 32);

  // DATOS DEL VENDEDOR (arriba, debajo del cliente)
  doc.setFont('helvetica', 'normal');
  doc.text("Vendedor:", 10, 32);
  doc.setFont('helvetica', 'bold');
  doc.text(Vendedor.nombre, 30, 32);

  // TABLA DE PRODUCTOS con columna Dsct %
  autoTable(doc, {
    startY: 45,
    head: [['#', 'CÓDIGO', 'DESCRIPCIÓN ARTÍCULO','Precio' ,'Dsct %','P.Dsct', 'CANT.', 'TOTAL']],
    body: productos.map((p, i) => [
      (i + 1).toString(),
      p.codigo,
      p.descripcion,
      formatCLP(p.precio),
      `${p.descuento}%`,
      formatCLP(p.precioVenta),
      p.cantidad,
      formatCLP(p.total)
    ]),
    theme: 'grid',
    styles: { fontSize: 9, cellPadding: 2 },
    headStyles: { fillColor: [41, 128, 185], textColor: 255 },
    margin: { top: 10, left: 10, right: 10, bottom: 35 },
  });

  // TOTALES
  const totalY = doc.previousAutoTable.finalY + 10;
  doc.setFont('helvetica', 'normal').setFontSize(10);
  doc.text(`Total unidades: ${totales.unidades}`, 10, totalY);

  autoTable(doc, {
    startY: totalY,
    margin: { left: 130 },
    body: [
      ['SUB-TOTAL', formatCLP(totales.subtotal)],
      ['DESCUENTOS', `${totales.descuento == 0 ? '0' : totales.descuento + '%'}`],
      ['I.V.A.', formatCLP(totales.iva)],
      [{ content: 'TOTAL BRUTO', styles: { fontStyle: 'bold' } }, { content: formatCLP(totales.total), styles: { fontStyle: 'bold' } }],
    ],
    theme: 'plain',
    styles: { fontSize: 10, halign: 'right', cellPadding: 2 },
  });

  // PIE DE FIRMA
  const pieY = doc.previousAutoTable.finalY + 20;
  doc.setFont('helvetica', 'normal').setFontSize(10);
  doc.text("Atendido por :", 10, pieY);
  doc.setFont('helvetica', 'bold');
  doc.text(Vendedor.nombre, 40, pieY);

  // FOOTER EN TODAS LAS PÁGINAS
  const totalPages = doc.internal.getNumberOfPages();
  for (let i = 1; i <= totalPages; i++) {
    doc.setPage(i);
    const footerY = doc.internal.pageSize.height - 15;
    doc.setFontSize(9).setFont('helvetica', 'normal');
    if (i === 1) {
      doc.text(`Fecha de impresión: ${new Date().toLocaleDateString()}`, 10, footerY +5, { align: 'left' });
      doc.text(`Hora de impresión: ${new Date().toLocaleTimeString()}`, 10, footerY , { align: 'left' });
    }
    doc.setFont('helvetica', 'bold');
    doc.text(`PÁGINA: ${i} de ${totalPages}`, 200, footerY+10, { align: 'right' });
    doc.text("Este documento es una cotización y no constituye una factura.", 10, footerY+10, { align: 'left' });
  }

  return doc;
}


