<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Home" Codebehind="Home.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #panel 
        {
            width:800px;
        }

        #panel td
        {
            text-align: center;
            line-height:28px;
            font-family: Verdana;
            font-size: 14px;
            color: #333333;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- LiveZilla Tracking Code (ALWAYS PLACE IN BODY ELEMENT) --><div id="livezilla_tracking" style="display:none"></div>
    <script type="text/javascript">
        var script = document.createElement("script");script.async=true;script.type="text/javascript";var src = "http://apps.linkium.mx/Chat/server.php?a=71201&rqst=track&output=jcrpt&intid=YWRtaW5pc3RyYXRvcg__&ovlc=IzAwODBjMA__&ovlt=wr9Db21vIHB1ZWRvIGF5dWRhcmxlPw__&ovlto=RW4gYnJldmUgbG8gY29udGFjdGFyZW1vcw__&ovlsx=NQ__&ovlsy=NQ__&ovlsb=NQ__&ovlsc=IzY5Njk2OQ__&ovlw=Mjgw&ovlh=NTAw&eca=Mg__&ecw=Mjgw&ech=MTgw&ecfi=Mw__&ecfo=NjA_&echm=MQ__&eci=aHR0cDovL2FwcHMubGlua2l1bS5teC9pbWcvYXRlbmNpb25fbGlua2l1bV9vbi5qcGc_&ecio=aHR0cDovL2FwcHMubGlua2l1bS5teC9pbWcvYXRlbmNpb25fbGlua2l1bV9vZmYuanBn&nse="+Math.random();setTimeout("script.src=src;document.getElementById('livezilla_tracking').appendChild(script)",1);</script><noscript><img src="http://apps.linkium.mx/Chat/server.php?a=71201&amp;rqst=track&amp;output=nojcrpt" width="0" height="0" style="visibility:hidden;" alt=""></noscript>
    <!-- http://www.LiveZilla.net Tracking Code -->
    <br /><br />
    <fieldset>
        <legend></legend>
        <table border="0" cellpadding="2" cellspacing="2" align="center" style="width:980px;" id="panel">
            <tr>
                <td align="center"><asp:ImageButton ID="lnk1" runat="server" PostBackUrl="~/portalcfd/Clientes.aspx" ImageUrl="~/images/inicio/clientes.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk2" runat="server" PostBackUrl="~/portalcfd/almacen/Conceptos.aspx" ImageUrl="~/images/inicio/productos.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk3" runat="server" PostBackUrl="~/portalcfd/CFD.aspx" ImageUrl="~/images/inicio/comprobantes.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk4" runat="server" PostBackUrl="~/portalcfd/Folios.aspx" ImageUrl="~/images/inicio/folios.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk10" runat="server" PostBackUrl="~/portalcfd/Datos.aspx" ImageUrl="~/images/inicio/empresas.jpg" /></td>
                
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td align="center"><asp:ImageButton ID="lnk9" runat="server" PostBackUrl="~/portalcfd/proveedores/proveedores.aspx" ImageUrl="~/images/inicio/proveedores.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk8" runat="server" PostBackUrl="~/portalcfd/almacen/abastecimiento.aspx" ImageUrl="~/images/inicio/inventario.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk5" runat="server" PostBackUrl="~/portalcfd/Reportes.aspx" ImageUrl="~/images/inicio/reportes.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk6" runat="server" PostBackUrl="~/portalcfd/Datos.aspx" ImageUrl="~/images/inicio/certificados.jpg" /></td>
                <td align="center"><asp:ImageButton ID="lnk11" runat="server" PostBackUrl="~/portalcfd/Salir.aspx" ImageUrl="~/images/inicio/salir.jpg" /></td>
                
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

