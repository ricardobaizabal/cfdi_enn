<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Facturar" MaintainScrollPositionOnPostback="true" CodeBehind="Facturar.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function changePrice(price, sender){
         var parent = sender.parentNode; //table cell (a parent element for your button)
         var textBox1 = $telerik.findControl(parent, "txtUnitaryPrice");
         console.log('value is' + price);
         textBox1.set_value(price);
         }
    </script>

    <style type="text/css">
        .titulos {
            font-family: verdana;
            font-size: medium;
            color: Purple;
        }
        .style1
        {
        	height:22px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
    </telerik:RadWindowManager>
    <br />
    <asp:Panel ID="panelClients" runat="server">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="3" cellspacing="3" align="left" width="100%">
                <tr>
                    <td class="item" colspan="2">
                        <strong>Seleccione el cliente:</strong>
                    </td>
                    <td width="25%" class="item">
                        <strong>Tipo de documento:</strong>
                    </td>
                    <td width="25%" class="item">
                        <strong>Método de Pago:</strong>
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:DropDownList ID="cmbCliente" runat="server" CausesValidation="false" Width="80%" CssClass="item" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbDocumento" runat="server" CssClass="box" AutoPostBack="false"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbMetodoPago" runat="server" Width="95%" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:RequiredFieldValidator ID="valClienteID" runat="server" InitialValue="0" ErrorMessage="Seleccione el cliente al cual le va a facturar." ControlToValidate="cmbCliente" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="item">
                        <asp:RequiredFieldValidator ID="valSerieId" runat="server" InitialValue="0" ErrorMessage="Requerido" ControlToValidate="cmbDocumento" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="item">
                        <asp:RequiredFieldValidator ID="valMetodoPago" runat="server" InitialValue="0" ErrorMessage="Requerido" ControlToValidate="cmbMetodoPago" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <strong>Moneda:</strong>
                    </td>
                    <td width="25%" class="item">
                        <strong>Tipo Cambio:</strong>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblTipoRelacion" runat="server" Text="Tipo de Relación:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblUUID" runat="server" Text="UUID:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbMoneda" runat="server" Width="90%" AutoPostBack="true" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">
                        <telerik:RadNumericTextBox ID="txtTipoCambio" runat="server" Type="Currency" NumberFormat-DecimalDigits="2" Value="0"></telerik:RadNumericTextBox>
                    </td>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbTipoRelacion" runat="server" Width="100%" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">
                        <telerik:RadTextBox ID="txtFolioFiscal" runat="server" Width="100%"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:RequiredFieldValidator ID="valMoneda" runat="server" InitialValue="0" ErrorMessage="Requerido" CssClass="item" ControlToValidate="cmbMoneda" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
<%--                    <td width="25%" class="item">
                        <asp:RequiredFieldValidator ID="valTipoCambio" runat="server" Enabled="false" ControlToValidate="txtTipoCambio" InitialValue="0" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>--%>
                    <td width="25%" class="item">
                        <asp:RequiredFieldValidator ID="valTipoRelecion" runat="server" InitialValue="0" ErrorMessage="Requerido" ControlToValidate="cmbTipoRelacion" SetFocusOnError="true" Enabled=false></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" colspan="2">
                        <asp:RequiredFieldValidator ID="valFolioFiscal" runat="server" InitialValue="0" ErrorMessage="Requerido" ControlToValidate="cmbTipoRelacion" SetFocusOnError="true" Enabled=false></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <strong>Orden Compra:</strong>
                    </td>
                    <td width="25%" class="item">
                        <strong>Exportación:</strong>&nbsp;<asp:RequiredFieldValidator ID="valExportacion" runat="server" InitialValue="0" ErrorMessage="Requerido" CssClass="item" ControlToValidate="cmbExportacion" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="item">&nbsp;</td>
                    <td width="25%" class="item">&nbsp;</td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <telerik:RadTextBox ID="txtOrdenCompra" runat="server"></telerik:RadTextBox>
                    </td>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbExportacion" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">&nbsp;</td>
                    <td width="25%" class="item">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelSpecificClient" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/portalcfd/images/datClient.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientData" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
                <tr>
                    <td class="item" colspan="2">
                        <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="True" Text="Condiciones:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:Label ID="lblSocialReasonValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblContactValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbCondiciones" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True" Text="Calle:"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Ext."></asp:Label>
                                </td>
                                <td style="width: 40%;">
                                    <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Int."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True" Text="País:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:Label ID="lblStreetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblExtNumberValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                                </td>
                                <td style="width: 54%;">
                                    <asp:Label ID="lblIntNumberValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblColonyValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblCountryValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True" Text="Estado:"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True" Text="Ciudad:"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True" Text="Código Postal:"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:Label ID="lblEstadoValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblTownshipValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblZipCodeValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblRFCValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="valFormaPago" runat="server" ErrorMessage="Requerido" ControlToValidate="cmbFormaPago" CssClass="item" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true" Text="Número de cuenta: (opcional):"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True" Text="Teléfono:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="25%" class="item">
                        <asp:DropDownList ID="cmbFormaPago" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">
                        <telerik:RadTextBox ID="txtNumCtaPago" runat="server">
                        </telerik:RadTextBox>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblTipoContribuyenteValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="25%" class="item">
                        <asp:Label ID="lblContactPhoneValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:Label ID="lblUsoCFDI" runat="server" CssClass="item" Font-Bold="True">Uso de comprobante:</asp:Label>&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="ValUsoComprobante" runat="server" InitialValue="0" ErrorMessage="Requerido" ControlToValidate="cmbUsoCFD" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="25%" class="item">&nbsp;</td>
                    <td width="25%" class="item">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:DropDownList ID="cmbUsoCFD" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td width="25%" class="item">&nbsp;</td>
                    <td width="25%" class="item">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <asp:Label ID="lblInstruccionesEspeciales" runat="server" CssClass="item" Font-Bold="True" Text="Observaciones (Enviar a):"></asp:Label>
                    </td>
                    <td width="25%" class="item">&nbsp;</td>
                    <td width="25%" class="item">&nbsp;</td>
                </tr>
                <tr>
                    <td class="item" colspan="2">
                        <telerik:RadTextBox ID="instrucciones" runat="server" Width="100%" CssClass="item" TextMode="MultiLine" Height="40px"></telerik:RadTextBox>
                    </td>
                    <td class="item" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="serie" runat="server" Value=""></asp:HiddenField>
                        <asp:HiddenField ID="folio" runat="server" Value="0" />
                        <asp:HiddenField ID="tipoidF" runat="server" Value="0" />
                        <%--<asp:HiddenField ID="ImporteIva" runat="server" Value="0" />
                        <asp:HiddenField ID="Curp" runat="server" Value="" />
                        <asp:HiddenField ID="CadenaF" runat="server" Value="" />--%>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelFacturaGlobal" runat="server" Visible="false">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="item" Text="Factura al Público en General - Factura Global"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="2" cellspacing="0" align="left" width="50%">
                <tr>
                    <td class="item" >
                        <strong>Periodicidad:</strong>
                        <asp:RequiredFieldValidator ID="valcmbPeriodicidad" runat="server" ControlToValidate="cmbPeriodicidad" ErrorMessage="Requerido*" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" >
                        <strong>Mes:</strong>
                        <asp:RequiredFieldValidator ID="valcmbMes" runat="server" ControlToValidate="cmbMes" ErrorMessage="Requerido*" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td class="item" >
                        <strong>Año:</strong>
                        <asp:RequiredFieldValidator ID="valtxtAnio" runat="server" ControlToValidate="txtAnio" ErrorMessage="Requerido*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
               </tr>
                <tr>
                    <td class="item" >
                        <asp:DropDownList ID="cmbPeriodicidad" runat="server" CssClass="box" Width="15em"></asp:DropDownList>
                    </td>
                     <td class="item" >
                        <asp:DropDownList ID="cmbMes" runat="server" CssClass="box" Width="15em"></asp:DropDownList>
                    </td>
                    <td class="item" >
                        <telerik:RadNumericTextBox Type="Number" NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" MaxLength="4" ID="txtAnio" runat="server" Width="8em" CssClass="item">
                         </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
         </fieldset>
       <br />
     </asp:Panel>
    <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <asp:HiddenField ID="productoid" runat="server" />
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <td valign="bottom" class="item">
                        <strong>Buscar:</strong>
                        <asp:TextBox ID="txtSearchItem" runat="server" CssClass="box" AutoPostBack="true"></asp:TextBox>&nbsp;presione enter después de escribir el código
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Simple" Visible="False">
                            <MasterTableView Width="100%" DataKeyNames="id,unitario,unitario2,unitario3,unitario4,precio" Name="Items" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Código</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>' Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Descripción</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# eval("descripcion") %>' Width="290px" CssClass="box" TextMode="MultiLine" MaxLength="400" Height="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>U. Medida</HeaderTemplate>
                                        <ItemTemplate >
                                            <asp:Label ID="lblUnidad" runat="server" Text='<%# eval("unidad") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>P. Unit. 1</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpUnitario1" runat="server" Text='<%# eval("unitario", "{0:$##,##0.0#}") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>P. Unit. 2</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpUnitario2" runat="server" Text='<%# eval("unitario2", "{0:$##,##0.0#}") %>' Width="50px" ></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>P. Unit. 3</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpUnitario3" runat="server" Text='<%# eval("unitario3", "{0:$##,##0.0#}") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>P. Unit. 4</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpUnitario4" runat="server" Text='<%# eval("unitario4", "{0:$##,##0.0#}") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Cant.</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Skin="Default" Width="50px"
                                                MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Tipo precio.</HeaderTemplate>
                                        <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0" Value="0"
                                                Skin="Default" Width="80px" style="margin-top: 2em;margin-bottom: .6em;" >
                                                <NumberFormat DecimalDigits="2" GroupSeparator=","/>
                                         </telerik:RadNumericTextBox>
                                        <select onchange="changePrice(this.value,this)" class="item">
                                            <option value='<%# eval("precio") %>' >Unitario <%#Eval("defaultprecioid")%></option>
                                            <option value='<%# eval("unitario") %>' >Unitario 1</option>
                                            <option value='<%# eval("unitario2") %>' >Unitario 2</option>
                                            <option value='<%# eval("unitario3") %>' >Unitario 3</option>
                                            <option value='<%# eval("unitario4") %>' >Unitario 4</option>
                                        </select>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="predial">
                                        <HeaderTemplate>Cuenta Predial</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCuenta" runat="server" Width="100px" Skin="Default"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Descuento.</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtDescuento" runat="server" Skin="Default" Width="60px"
                                                MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>


                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Add">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' 
                                                CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar producto comoo partida" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <br />
                        <asp:Button ID="btnCancelSearch" Visible="false" runat="server" CausesValidation="False" CssClass="item" Text="Cancelar" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <td>
                        <br />
                        <telerik:RadGrid ID="itemsList" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Simple" Visible="False">
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="" UniqueName="cantidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="precio" HeaderText="Precio Unitario" UniqueName="precio" DataFormatString="{0:C}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="" UniqueName="importe" DataFormatString="{0:C}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="False" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <br />
    <asp:Panel ID="panelResume" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>

            <br />

            <table width="100%" align="left">
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Descuento = "></asp:Label>
                        &nbsp;<asp:Label ID="lblDescuentoValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblIVA" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 32%">
                        <asp:Label ID="lblIEPS" runat="server" CssClass="item" Font-Bold="True" Text="IEPS = "></asp:Label>
                        &nbsp;<asp:Label ID="lblIEPSValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 32%">
                        <asp:Label ID="lblRetISR" runat="server" CssClass="item" Font-Bold="True" Text="Ret. ISR = "></asp:Label>
                        &nbsp;<asp:Label ID="lblRetISRValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 32%">
                        <asp:Label ID="lblRetIVA" runat="server" CssClass="item" Font-Bold="True" Text="Ret. IVA = "></asp:Label>
                        &nbsp;<asp:Label ID="lblRetIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <%--<asp:Panel ID="panelRetencion" runat="server" Visible="false">
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblRet" runat="server" CssClass="item" Font-Bold="True" Text="Retención 4% = "></asp:Label>
                            &nbsp;<asp:Label ID="lblRetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                </asp:Panel>--%>
                <tr>
                    <td width="16%" align="left" style="width: 32%">
                        <asp:Label ID="lblTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="16%">
                        <br />
                        <br />
                        <asp:Button ID="btnCreateInvoice" runat="server" CausesValidation="true" CssClass="item" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelInvoice" runat="server" CausesValidation="False" CssClass="item" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>

        </fieldset>

    </asp:Panel>
    <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" CenterIfModal="true" AutoSize="False" Behaviors="Close" VisibleOnPageLoad="False" Width="740" Height="600">
        <ContentTemplate>
            <br />
            <table align="center" width="95%">
                <tr>
                    <td>
                        <asp:TextBox ID="txtErrores" TextMode="MultiLine" Width="100%" Rows="32" ReadOnly="true" CssClass="item" runat="server"></asp:TextBox>
                    </td>
                    <tr>
                        <td align="left" width="16%">
                            <br />
                            <br />
                            <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" CssClass="item" Text="Aceptar" />
                            <br />
                            <br />
                        </td>
                    </tr>
                </tr>
            </table>
            <br />
        </ContentTemplate>
    </telerik:RadWindow>
    <%--</telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>