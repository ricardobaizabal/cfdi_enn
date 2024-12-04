<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="facturas_recibidas.aspx.vb" Inherits="LinkiumCFDI.facturas_recibidas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
    function OnRequestStart(target, arguments) {
        if ((arguments.get_eventTarget().indexOf("facturaslist") > -1)) {
             arguments.set_enableAjax(false);
         }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <br />
        <fieldset>
            <legend style="padding-right:6px; color:Black">
                <asp:Label ID="Label1" runat="server" Text="Filtros" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" border="0">
                <tr style="height:10px;">
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:5%;">Desde:</td>
                    <td class="item" style="font-weight:bold; width:15%;">
                        <telerik:RadDatePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td class="item" style="font-weight:bold; width:5%;">Hasta:</td>
                    <td class="item" style="font-weight:bold; width:15%;">
                        <telerik:RadDatePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td class="item" style="font-weight:bold; width:8%;">Proveedor:</td>
                    <td class="item" style="font-weight:bold; width:30%;">
                        <asp:DropDownList ID="proveedorid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td class="item" style="font-weight:bold; width:22%;">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                    </td>
                </tr>
                <tr><td colspan="7" style="height:30px;">&nbsp;</td></tr>
            </table>
        </fieldset>
        <br />   
        <br />
        <fieldset>
            <legend style="padding-right:6px; color:Black">
                <asp:Label ID="Label2" runat="server" Text="Listado de Facturas" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" cellpadding="3" cellspacing="3" border="0">
                <tr style="height:10px;">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="facturaslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            Skin="Simple" AllowFilteringByColumn="false">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Facturas" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha_emision" HeaderText="Emisión" UniqueName="fecha_emision" DataFormatString="{0:d}" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="emisor" HeaderText="Proveedor" UniqueName="emisor" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="lugar_expedicion" HeaderText="Lugar Expedición" UniqueName="lugar_expedicion" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="serie" HeaderText="Serie" UniqueName="serie" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="folio" HeaderText="Folio" UniqueName="folio" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="uuid" HeaderText="UUID" UniqueName="uuid" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="XML">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkXML" runat="server" Text="xml" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdXML"></asp:LinkButton>
                                            <asp:Label ID="lblXML" runat="server" Text='<%# Eval("xml") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="PDF">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPDF" runat="server" Text="pdf" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdPDF"></asp:LinkButton>
                                            <asp:Label ID="lblPDF" runat="server" Text='<%# Eval("pdf") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr style="height:10px;">
                    <td>&nbsp;</td>
                </tr>                
            </table>
        </fieldset> 
    </telerik:RadAjaxPanel>
</asp:Content>