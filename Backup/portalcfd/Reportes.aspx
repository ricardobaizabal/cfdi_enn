﻿<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Reportes" Codebehind="Reportes.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <asp:Image id="img2" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport2" runat="server" Text="Reporte de facturación" NavigateUrl="~/portalcfd/reportes/ingresos.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        <asp:Image id="img3" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport3" runat="server" Text="Reporte de cobranza" NavigateUrl="~/portalcfd/reportes/cobranza.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        <asp:Image id="img5" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport5" runat="server" Text="Reporte de cartera" NavigateUrl="~/portalcfd/reportes/carteraGraf.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        <asp:Image id="img6" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport6" runat="server" Text="Reporte de notas de crédito" NavigateUrl="~/portalcfd/reportes/notasdecredito.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        
                        <asp:Panel ID="almacenpanel" runat="server" Visible="false">
                            <asp:Image id="img9" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport9" runat="server" Text="Productos más vendidos" NavigateUrl="~/portalcfd/reportes/productos.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        </asp:Panel>
                        
                        <asp:panel ID="userpanel" runat="server" Visible="false">
                            <asp:Image id="img7" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport7" runat="server" Text="Reporte de facturación por usuario" NavigateUrl="~/portalcfd/reportes/facturasxusuario.aspx" CssClass="item"></asp:HyperLink><br /><br />
                            <asp:Image id="img8" runat="server" ImageUrl="~/portalcfd/images/file.png" ImageAlign="Left" />&nbsp;<asp:HyperLink ID="lnkReport8" runat="server" Text="Reporte de actualización de facturas por usuario" NavigateUrl="~/portalcfd/reportes/actualizacionfacturasxusuario.aspx" CssClass="item"></asp:HyperLink><br /><br />
                        </asp:panel>
                        
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>

