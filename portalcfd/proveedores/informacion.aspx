<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_proveedores_informacion" Codebehind="informacion.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <fieldset>
        <legend></legend>
        <table border="0" cellpadding="2" cellspacing="2" align="center" style="width:980px;" id="panel">
            <tr>
                <td align="center" valign="top">
                    <asp:Image ID="imgProveedores" runat="server" ImageUrl="~/images/icons/PROVEEDORES.gif" />
                </td>
                <td style="width:30px;">&nbsp;</td>
                <td valign="top">
                    <strong class="item" style="font-size:14px;">El módulo de proveedores es un componente opcional de este sistema el cual le presta los siguientes beneficios:</strong>
                    <br />
                    <ul class="item">
                        <li>Recepción de facturas formato CFD y CFDI, así como CBB y Factura convencional</li>
                        <li>Catálogo de proveedores</li>
                        <li>Asociación de facturas recibidas con proveedores de la base de datos</li>
                        <li>Reporte de cuentas por pagar con filtros de proveedor ordenable por fechas</li>
                        <li>Reporte de facturas pagadas con filtros de proveedor y rangos de fecha</li>
                        <li>Los reportes se pueden descargar en excel</li>
                        <li>Carga la factura desde el XML y lo agrega a la base de datos, solo en el caso de CFD y CFDI</li>
                        <li>Muestra el detalle de la factura en pantalla con partidas</li>
                        <li>Se puede asociar el PDF que envía el cliente para que quede almacenado en el sistema para reimpresión</li>
                        <li>Opción de agregar comentarios dentro del registro de la factura en el sistema</li>
                    </ul>
                    <br />
                    <strong class="item" style="font-size:14px;">¿Cómo lo obtengo?</strong>
                    <br />
                    <ul class="item">
                        <li>El módulo de proveedores tiene un costo de setup de <strong>$1,800 MXN</strong> más iva</li>
                        <li>Se entregará factura sin excepción</li>
                        <li>El proceso de habilitación del módulo será de un día hábil previa confirmación de pago</li>
                        <li>El pago será en una sola exhibición</li>
                        <li>Para solicitar el módulo de proveedores por favor escríbanos a <a href="mailto:ventas@impresoraunion.com">ventas@impresoraunion.com</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

