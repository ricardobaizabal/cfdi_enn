<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="estatus.aspx.vb" Inherits="LinkiumCFDI.estatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <fieldset class="item">
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEstatus" runat="server" Font-Bold="true" CssClass="item" Text="Estatus del Sistema"></asp:Label>
        </legend>
        <br /><br />
        <table>
        <tr>
            <td>
                <strong>Espacio máximo contratado: </strong> <asp:Label ID="lblEspacioMaximo" runat="server"></asp:Label> <strong>MB</strong><br /><br />
        
                <strong>Espacio utilizado en archivos XML y PDF: </strong> <asp:Label ID="lblEspacioUtilizado" runat="server"></asp:Label> <strong>MB</strong><br /><br />
                
                <strong>Espacio disponible: </strong> <asp:Label ID="lblEspacioDisponible" runat="server"></asp:Label> <strong>MB</strong><br /><br />
                <div style="float:left;">
                    <telerik:RadRadialGauge  
                        ID="RadRadialGauge1" runat="server" Skin="Office2007">
                        <Scale>
                            <Labels Format="{0} MB" />
                            <Ranges>
                              <telerik:GaugeRange Color="#8dcb2a" From="1" To="100" />
                              <telerik:GaugeRange Color="#ffc700" From="101" To="150" />
                              <telerik:GaugeRange Color="#c20000" From="151" To="200" />
                             </Ranges>
                        </Scale>
                    </telerik:RadRadialGauge>
                </div>
            </td>
        </tr>
        <%--<tr>
            <td>
                <br /><br />
        
                <hr />
                
                <strong>Timbres adquiridos: </strong> <asp:Label id="lblTimbres" runat="server"></asp:Label><br /><br />
                
                <strong>Timbres utilizados por facturación: </strong> <asp:Label id="lblTimbresUtilizadosFac" runat="server"></asp:Label><br /><br />
                
                <strong>Timbres utilizados por cancelaciones: </strong> <asp:Label id="lblTimbresUtilizadosCanc" runat="server"></asp:Label><br /><br />
                
                <strong>Timbres disponibles: </strong> <asp:Label id="lblTimbresDisponibles" runat="server"></asp:Label><br /><br />
        
            </td>
        </tr>
        --%>
    </fieldset>
        </table>

</asp:Content>
