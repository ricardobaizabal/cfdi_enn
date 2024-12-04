<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="egresos.aspx.vb" Inherits="LinkiumCFDI.egresos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblTitle" runat="server" Font-Bold="true" CssClass="item" Text="Reporte de Egresos"></asp:Label>
        </legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td class="item">
                    Seleccione el rango de fechas que desee consultar:<br /><br />
                    <table style="width:80%" border="0">
                        <tr>
                            <td>Proveedor: </td>
                            <td>
                                <asp:DropDownList ID="proveedorid" runat="server" Width="90%" CssClass="box"></asp:DropDownList>
                            </td>
                            <td>Desde: </td>
                            <td>
                                <telerik:RadDatePicker ID="fechaini" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                            </td>
                            <td>Hasta: </td>
                            <td>
                                <telerik:RadDatePicker ID="fechafin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>Banco: </td>
                            <td>
                                <asp:DropDownList ID="bancoid" runat="server" Width="90%" AutoPostBack="true" CssClass="box">
                                </asp:DropDownList>
                            </td>
                            <td>Cuenta: </td>
                            <td>
                                <asp:DropDownList ID="cuentaid" runat="server" CssClass="box">
                                </asp:DropDownList>
                            </td>
                            <td>Moneda: </td>
                            <td>
                                <asp:DropDownList ID="monedaid" runat="server" CssClass="box">
                                    <asp:ListItem Value="0" Text="--Todos--"></asp:ListItem>
                                    <asp:ListItem Value="MXN" Text="MXN"></asp:ListItem>
                                    <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right">
                                <asp:Button ID="btnGenerate" runat="server" Text="Generar" CssClass="boton" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="facturaslist" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" GridLines="None" ShowFooter="True" 
                        PageSize="50" ShowStatusBar="True"  ExportSettings-ExportOnlyData="False" 
                        Skin="Simple" Width="100%">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="Reporte_egresos">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="CuentasPorPagar" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Left" DataField="no_documento" HeaderText="No. Dcomuento" UniqueName="no_documento" AllowFiltering="false">
                                </telerik:GridBoundColumn>               
                                <telerik:GridBoundColumn DataField="proveedor" HeaderText="Proveedor" UniqueName="proveedor" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="tipo" HeaderText="Documento" UniqueName="tipo" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="fecha_promesa_pago" HeaderText="Promesa de Pago" DataFormatString="{0:d}" UniqueName="fecha_promesa_pago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="fecha_pago" HeaderText="F. de Pago" DataFormatString="{0:d}" UniqueName="fecha_pago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="left" DataField="metodo_pago" HeaderText="Método de Pago" UniqueName="metodo_pago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="banco" HeaderText="Banco" UniqueName="banco" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="no_cheque" HeaderText="No. Cheque" UniqueName="no_cheque" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="total_dolares" HeaderText="Total en Divisa" DataFormatString="{0:c}" UniqueName="total_dolares" AllowFiltering="false">
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" DataField="total_pesos" HeaderText="Total" DataFormatString="{0:c}" UniqueName="total_pesos" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="moneda" HeaderText="Moneda" UniqueName="moneda" AllowFiltering="false">
                                </telerik:GridBoundColumn>--%>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 5px">&nbsp;</td>
            </tr>
        </table>
        <br />
    </fieldset>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
</telerik:RadAjaxLoadingPanel>
</asp:Content>
