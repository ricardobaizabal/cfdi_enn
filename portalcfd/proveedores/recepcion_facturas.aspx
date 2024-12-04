<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_proveedores_recepcion_facturas" Codebehind="recepcion_facturas.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
    function OnRequestStart(target, arguments) {
        if ((arguments.get_eventTarget().indexOf("facturaslist") > -1) || (arguments.get_eventTarget().indexOf("btnCargarXML") > -1)) {
             arguments.set_enableAjax(false);
         }
     }
     function OnClientAdded(sender, args) {
         if (sender.getFileInputs().length == sender.get_maxFileCount()) {
             $telerik.$(".ruDelete").remove();
             $telerik.$(".ruAdd").remove();
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
    <fieldset>
        <legend style="padding-right:6px; color:Black">
            <asp:Label ID="Label1" runat="server" Text="Recepción de Facturas" Font-Bold="true" CssClass="item"></asp:Label>
        </legend>
        <table width="100%" cellpadding="3" cellspacing="3" border="0">
            <tr style="height:10px;">
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="item" style="width:15%"><strong>Proveedor:</strong></td>
                <td class="item" style="width:30%">
                    <asp:DropDownList ID="proveedorid" runat="server" Width="100%" AutoPostBack="true" CssClass="box"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="proveedorid" ErrorMessage="Seleccione un proveedor" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item" style="width:15%"><strong>Moneda:</strong></td>
                <td class="item" style="width:30%">
                    <asp:DropDownList ID="monedaid" runat="server" Width="50%" CssClass="box"></asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="monedaid" ErrorMessage="Seleccione un tipo de moneda" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item" style="width:15%"><strong>O. Compra:</strong></td>
                <td class="item" style="width:30%">
                    <asp:DropDownList ID="ordencompraid" runat="server" Width="50%" CssClass="box"></asp:DropDownList>
                </td>
                <td>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ordencompraid" ErrorMessage="Seleccione una órden de compra" InitialValue="0" ForeColor="Red" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr valign="top">
                <td class="item" style="width:15%"><strong>Archivo(s) XML:</strong></td>
                <td class="item" style="width:30%">
                    <telerik:RadUpload ID="archivosXML" allowedfileextensions=".xml" runat="server" OnClientAdded="OnClientAdded" Culture="es-MX" Localization-Add="Agregar" Localization-Select="Seleccione" Localization-Remove="Remover" Localization-Delete="Eliminar" ControlObjectsVisibility="RemoveButtons, AddButton, DeleteSelectedButton">
                    </telerik:RadUpload>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width:15%">&nbsp;</td>
                <td style="width:30%" align="right">
                    <asp:Button ID="btnCargarXML" runat="server" Text="Guardar Facturas" CssClass="item" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr style="height:10px;">
                <td colspan="3">
                    <asp:Label ID="lblMensaje" CssClass="item" Font-Bold="true" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height:10px;">
                <td colspan="3">
                    <asp:Label ID="lblError" CssClass="item" Font-Bold="true" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="IdFacturaProveedor" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:Panel ID="pListadoFacturas" runat="server" Visible="true">
        <br />
        <fieldset>
            <legend style="padding-right:6px; color:Black">
                <asp:Label ID="lblultimasfacturasTitle" runat="server" Text="Últimas Facturas Recibidas" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%" cellpadding="3" cellspacing="3" border="0">
                <tr style="height:10px;">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="facturaslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                            Skin="Simple" AllowFilteringByColumn="false">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Facturas" NoMasterRecordsText="No se encontraron facturas" AllowMultiColumnSorting="False">
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
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVer" runat="server" Text="Ver" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdVer"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Eliminar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdEliminar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
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
            <br />
        </fieldset>
        <br />
        <br />
        <asp:Panel ID="pDetalleProveedor" runat="server" Visible="False">
            <fieldset>
            <legend style="padding-right:6px; color:Black">
                <asp:Label ID="lblDetalleFactura" runat="server" Text="Detalle Factura" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table border="0" cellpadding="3" cellspacing="5" width="100%">
                <tr>
                    <td colspan="3">
                        <asp:Panel ID="panelPDF" runat="server" Visible="False" Width="100%">
                            <table width="100%" cellpadding="3" cellspacing="3" border="0">
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td class="item" style="width:25%;">
                                        <strong>Factura formato PDF:</strong>
                                    </td>
                                    <td class="item">
                                        <telerik:RadUpload ID="archivoPDF" Width="350px" MaxFileInputsCount="1" allowedfileextensions=".pdf" runat="server" OnClientAdded="OnClientAdded" Culture="es-MX" Localization-Add="Agregar" Localization-Select="Seleccione" Localization-Remove="Remover" Localization-Delete="Eliminar" ControlObjectsVisibility="RemoveButtons, AddButton, DeleteSelectedButton">
                                        </telerik:RadUpload>
                                    </td>
                                    <td class="item" style="width:10%">
                                        <asp:Button ID="btnCargaPDF" runat="server" CausesValidation="false" Text="Guardar PDF" CssClass="item" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMensajePDF" CssClass="item" Font-Bold="true" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:50%;" colspan="2">Proveedor</td>
                    <td class="item" style="font-weight:bold; width:25%;">RFC</td>
                    <td class="item" style="font-weight:bold; width:25%;">Régimen</td>
                </tr>
                <tr>
                    <td class="item" colspan="2"><asp:Label ID="lblProveedor" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblRFC" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblRegimen" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:25%;">Calle</td>
                    <td class="item" style="font-weight:bold; width:25%;">No. Ext.</td>
                    <td class="item" style="font-weight:bold; width:25%;">No. Int.</td>
                    <td class="item" style="font-weight:bold; width:25%;">Colonia</td>
                </tr>
                <tr>
                    <td class="item"><asp:Label ID="lblCalle" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblNoExt" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblNoInt" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblColonia" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:25%;">País</td>
                    <td class="item" style="font-weight:bold; width:25%;">Estado</td>
                    <td class="item" style="font-weight:bold; width:25%;">Municipio</td>
                    <td class="item" style="font-weight:bold; width:25%;">CP</td>
                </tr>
                <tr>
                    <td class="item"><asp:Label ID="lblPais" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblMunicipio" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblCP" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:25%;">Fecha Emisión</td>
                    <td class="item" style="font-weight:bold; width:25%;">Lugar Expedición</td>
                    <td class="item" style="font-weight:bold; width:25%;">Método de Pago</td>
                    <td class="item" style="font-weight:bold; width:25%;">Forma de Pago</td>
                </tr>
                <tr>
                    <td class="item"><asp:Label ID="lblFechaEmision" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblLugarExpedicion" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblFormaPago" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblMetodoPago" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:25%;">Serie</td>
                    <td class="item" style="font-weight:bold; width:25%;">Folio</td>
                    <td class="item" style="font-weight:bold; width:25%;">UUID</td>
                    <td class="item" style="font-weight:bold; width:25%;"></td>
                </tr>
                <tr>
                    <td class="item"><asp:Label ID="lblSerie" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblFolio" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblUUID" runat="server" Text=""></asp:Label></td>
                    <td class="item"></td>
                </tr>
                <tr>
                    <td class="item" style="font-weight:bold; width:25%;">Importe</td>
                    <td class="item" style="font-weight:bold; width:25%;">Impuestos</td>
                    <td class="item" style="font-weight:bold; width:25%;">Total</td>
                    <td class="item" style="font-weight:bold; width:25%;"></td>
                </tr>
                <tr>
                    <td class="item"><asp:Label ID="lblImporte" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblImpuestos" runat="server" Text=""></asp:Label></td>
                    <td class="item"><asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                    <td class="item"></td>
                </tr>
                <tr>
                    <td style="height:20px;" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" colspan="4" style="font-weight:bold;">Sello Digital CFDI</td>
                </tr>
                <tr>
                    <td class="item" colspan="4">
                        <asp:TextBox ID="lblSelloCFDI" Width="100%" BorderStyle="None" CssClass="textarea" TextMode="MultiLine" ReadOnly="true" Rows="4" runat="server" Text=""></asp:TextBox></td>
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="4" style="font-weight:bold;">Sello Digital SAT</td>
                </tr>
                <tr>
                    <td class="item" colspan="4">
                        <asp:TextBox ID="lblSelloSAT" Width="100%" BorderStyle="None" CssClass="textarea" TextMode="MultiLine" ReadOnly="true" Rows="4" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
            </table>
            </fieldset>
            </asp:Panel>
    </asp:Panel>
    </telerik:RadAjaxPanel>
</asp:Content>

