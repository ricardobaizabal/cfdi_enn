<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_inventarios_informacion" Codebehind="informacion.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <fieldset>
        <legend></legend>
        <table border="0" cellpadding="2" cellspacing="2" align="center" style="width:980px;" id="panel">
            <tr>
                <td align="center" valign="top">
                    <asp:Image ID="imgProveedores" runat="server" ImageUrl="~/images/icons/INVENTARIOS.gif" />
                </td>
                <td style="width:30px;">&nbsp;</td>
                <td valign="top">
                    <strong class="item" style="font-size:14px;">El módulo de inventarios es un componente opcional de este sistema el cual le presta los siguientes beneficios:</strong>
                    <br />
                    <ul class="item">
                        <li>Módulo de productos extendido con existencias, máximos y mínimos, punto de reorden etc.</li>
                        <li>Familias y Subfamilias de productos</li>
                        <li>Kardex de producto</li>
                        <li>Descarga de inventario al generar facturas</li>
                        <li>Ajustes de inventario</li>
                        <li>Reporte de existencia</li>
                        <li>Reporte de resurtido</li>
                        <li>Reporte de productos más vendidos</li>
                        <li>Estructura de datos preparada para venta por internet, tienda virtual no incuído en este módulo</li>
                    </ul>
                    <br />
                    <strong class="item" style="font-size:14px;">¿Cómo lo obtengo?</strong>
                    <br />
                    <ul class="item">
                        <li>El módulo de inventarios tiene un costo de setup de <strong>$2,200 MXN</strong> más iva</li>
                        <li>Se entregará factura sin excepción</li>
                        <li>El proceso de habilitación del módulo será de un día hábil previa confirmación de pago</li>
                        <li>El pago será en una sola exhibición</li>
                        <li>Para solicitar el módulo de inventarios por favor escríbanos a <a href="mailto:info@linkium.mx">info@linkium.mx</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

