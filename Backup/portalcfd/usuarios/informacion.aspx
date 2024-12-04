<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_usuarios_informacion" Codebehind="informacion.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <fieldset>
        <legend></legend>
        <table border="0" cellpadding="2" cellspacing="2" align="center" style="width:980px;" id="panel">
            <tr>
                <td align="center" valign="top">
                    <asp:Image ID="imgProveedores" runat="server" ImageUrl="~/images/icons/MisUsuarios.jpg" />
                </td>
                <td style="width:30px;">&nbsp;</td>
                <td valign="top">
                    <strong class="item" style="font-size:14px;">El módulo de usuarios es un componente opcional de este sistema que le otorga estos beneficios:</strong>
                    <br />
                    <ul class="item">
                        <li>Accesos independientes para sus usuarios que facturan.</li>
                        <li>Reporte de facturas emitidas por usuario</li>
                        <li>Reporte de ingresos diferenciado por usuario</li>
                        <li>Hasta 5 usuarios simultáneos diferenciados</li>
                        <li>Módulo de administración de usuarios y contraseñas</li>
                    </ul>
                    <br />
                    <strong class="item" style="font-size:14px;">¿Cómo lo obtengo?</strong>
                    <br />
                    <ul class="item">
                        <li>El módulo de usuarios tiene un costo de setup de <strong>$1,800 MXN</strong> más iva</li>
                        <li>Se entregará factura sin excepción</li>
                        <li>El proceso de habilitación del módulo será de un día hábil previa confirmación de pago</li>
                        <li>El pago será en una sola exhibición</li>
                        <li>Para solicitar el módulo de usuarios por favor escríbanos a <a href="mailto:ventas@impresoraunion.com">ventas@impresoraunion.com</a></li>
                    </ul>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

