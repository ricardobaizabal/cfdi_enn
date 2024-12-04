<%@ Control Language="VB" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_usercontrols_portalcfd_Menu_PortalCFD" Codebehind="Menu_PortalCFD.ascx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<style type="text/css">
#container {
   width:100%;
   text-align:center;
}

#left {
    float:left;
}

#center {
    margin:0 auto;
}

#right {
    float:right;

}
    </style>
<telerik:RadMenu ID="RadMenu1" runat="server" Width="100%" Skin="Sitefinity" 
    style="z-index:3000">
    <Items>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Inicio" NavigateUrl="~/portalcfd/Home.aspx">
        </telerik:RadMenuItem>

        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Clientes" NavigateUrl="~/portalcfd/Clientes.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Proveedores">
            <Items>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Catálogo proveedores" NavigateUrl="~/portalcfd/proveedores/proveedores.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Catálogo cuentas de banco" NavigateUrl="~/portalcfd/proveedores/cuentas_banco.aspx"></telerik:RadMenuItem>
                
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Ordenes de compra anteriores" NavigateUrl="~/portalcfd/proveedores/ordenes_compra.aspx"></telerik:RadMenuItem>
                
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Ordenes de compra" NavigateUrl="~/portalcfd/OrdenesCompra.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Ordenes de compra emitidas" NavigateUrl="~/portalcfd/OrdenesCompraEmitidas.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Recepción de Facturas" NavigateUrl="~/portalcfd/proveedores/recepcion_facturas.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Facturas Recibidas" NavigateUrl="~/portalcfd/proveedores/facturas_recibidas.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Módulo de Gastos" NavigateUrl="~/portalcfd/proveedores/gastos.aspx" ToolTip="Registro de gastos no fiscales"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Cuentas Por Pagar" NavigateUrl="~/portalcfd/proveedores/cuentas_por_pagar.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Reporte de Egresos" NavigateUrl="~/portalcfd/proveedores/egresos.aspx"></telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Almacén" NavigateUrl="#">
            <Items>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Conceptos" NavigateUrl="~/portalcfd/almacen/Conceptos.aspx">
                </telerik:RadMenuItem>                
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Inventarios">
                    <Items>
                        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Entradas de almacen" NavigateUrl="~/portalcfd/almacen/entradas.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Ajustes de inventario" NavigateUrl="~/portalcfd/almacen/ajustes.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Kardex de producto" NavigateUrl="~/portalcfd/almacen/kardex.aspx"></telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>                
            </Items>
        </telerik:RadMenuItem>
        
         <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Facturación" NavigateUrl="#">
            <Items>
                <telerik:RadMenuItem Text="Nueva factura" NavigateUrl="~/portalcfd/facturar.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Facturas emitidas" NavigateUrl="~/portalcfd/CFD.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Nueva cotización" NavigateUrl="~/portalcfd/AgregarCotizacion.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Cotizaciones emitidas" NavigateUrl="~/portalcfd/Cotizaciones.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Nueva remisión" NavigateUrl="~/portalcfd/AgregarRemision.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Remisiones emitidas" NavigateUrl="~/portalcfd/Remisiones.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Nuevo Complemento de Pagos" NavigateUrl="~/portalcfd/ComplementoDePagos.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Complementos de Pago Emitidos" NavigateUrl="~/portalcfd/Complementosemitidos.aspx"></telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Reportes" NavigateUrl="~/portalcfd/Reportes.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Configuración">
            <Items>
                <telerik:RadMenuItem Text="Mis Datos" NavigateUrl="~/portalcfd/Datos.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Firma & Certificados" NavigateUrl="~/portalcfd/Configuracion.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Folios" NavigateUrl="~/portalcfd/folios.aspx">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Mis Usuarios" NavigateUrl="~/portalcfd/usuarios/usuarios.aspx">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Catalogos Sistema" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem Text="Unidades de Medida" NavigateUrl="~/portalcfd/Unidad.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Productos o Servicios" NavigateUrl="~/portalcfd/claveproducto.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Cuentas de Beneficiario" NavigateUrl="~/portalcfd/CuentasBeneficiario.aspx"></telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Salir" NavigateUrl="~/portalcfd/Salir.aspx">
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>
<br />
<br />
<div id="container">
  <div id="left">
      <asp:Label ID="lblUsuario" runat="server" CssClass="item"></asp:Label>
  </div>
  <div id="right">
      <asp:Label ID="lblCertificado" runat="server" CssClass="item"></asp:Label>
  </div>
  <div id="center">
      <asp:LinkButton ID="lnkConsultarFolios" runat="server" CssClass="item">Consultar Folios Disponible</asp:LinkButton>
  </div>
</div>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" BorderStyle="None" BorderWidth="0px" VisibleStatusbar="True" VisibleTitlebar="False">
    <Windows> 
        <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="380px" Height="250px" Skin="Simple">
        <ContentTemplate>
        <table style="width:100%; height:100%;" align="center" cellpadding="0" cellspacing="3" border="0">
            <tr>
                <td style="height: 5px" colspan="2">
                   <telerik:RadGrid ID="FoliosGrid" runat="server" Width="100%" ShowStatusBar="True"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                        Skin="Simple">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView Width="100%" Name="Folios" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="tipo" HeaderText="Documento" UniqueName="tipo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="folioDisponible" HeaderText="Folios Disponibles" UniqueName="folioDisponible" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
