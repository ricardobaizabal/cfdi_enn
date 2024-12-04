<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Facturar_Transporte" Codebehind="Facturar_Transporte.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .titulos 
        {
            font-family:verdana;
            font-size:medium;
            color:Purple;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

        <asp:Panel ID="panelClients" runat="server">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />
                
                <table border="0" cellpadding="2" cellspacing="0" align="left" width="95%">
                    <tr>
                        <td class="item" colspan="2">
                            <strong>Seleccione el cliente:</strong>&nbsp;<asp:RequiredFieldValidator ID="valClienteID" runat="server" InitialValue="0" ErrorMessage="Seleccione el cliente al cual le va a facturar." ControlToValidate="cmbClient" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="cmbClient" runat="server" CausesValidation="false" CssClass="item" AutoPostBack="true"></asp:DropDownList>            
                        </td>
                        <td class="item" colspan="2">
                            <strong>Lugar de expedición:</strong>&nbsp;<asp:RequiredFieldValidator ID="valExpedicion" runat="server" ErrorMessage="Especifique el lugar de expedición." ControlToValidate="txtLugarExpedicion" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <telerik:RadTextBox ID="txtLugarExpedicion" Runat="server" Width="350px" CssClass="item">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"><br /></td>
                    </tr>
                    <tr>
                        <td class="item">
                            <strong>Tipo de documento:</strong>&nbsp;<asp:RequiredFieldValidator ID="valSerieId" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="serieid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="serieid" runat="server" CssClass="box" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td class="item">
                            <strong>Forma de pago:</strong>&nbsp;<asp:RequiredFieldValidator ID="valTipoPago" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="tipopagoid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="tipopagoid" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                        <td class="item">
                            <strong>Tasa:</strong>&nbsp;<asp:RequiredFieldValidator ID="valTasaid" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="serieid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="tasaid" runat="server" CssClass="box" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td class="item">
                            <asp:panel ID="panelDivisas" runat="server" Visible="false">
                                <strong>Tipo de cambio:</strong>&nbsp;<asp:RequiredFieldValidator ID="valTipoCambio" runat="server" ControlToValidate="tipocambio" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                                $ <telerik:RadNumericTextBox ID="tipocambio" runat="server" NumberFormat-DecimalDigits="2" Value="0"></telerik:RadNumericTextBox>
                            </asp:panel>
                        </td>
                    </tr>
                </table>
                    
                
            </fieldset>

        </asp:Panel>

        <br />    

        <asp:Panel ID="panelSpecificClient" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/portalcfd/images/datClient.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientData" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />

                <table width="100%">
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="True" Text="Condiciones"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblSocialReasonValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblContactValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:DropDownList ID="condicionesId" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblTipoPrecio" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Precio:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblContactPhoneValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblRFCValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblTipoPrecioValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="66%" colspan="2">
                            <asp:Label ID="lblEnviar" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        
                        <td width="33%">
                            <asp:Label ID="lblMetodoPago" runat="server" CssClass="item" Font-Bold="true"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="valMetodoPago" runat="server" ErrorMessage="Requerido" ControlToValidate="formapagoid" CssClass="item" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="66%">
                            <telerik:RadTextBox ID="enviara" Runat="server" Width="450px" CssClass="item">
                            </telerik:RadTextBox>
                        </td>
                        <td width="33%">
                            <asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="66%" colspan="2">
                            <asp:Label ID="lblInstruccionesEspeciales" runat="server" CssClass="item" 
                                Font-Bold="True" Text="Instrucciones Especiales y/o Consignado a:"></asp:Label>
                        </td>
                        
                        <td width="33%">
                            <asp:Label ID="lblNumCtaPago" runat="server" Font-Bold="true" CssClass="item"></asp:Label>    
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="66%">
                            <telerik:RadTextBox ID="instrucciones" Runat="server" Width="450px" CssClass="item" TextMode="MultiLine" Height="40px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="33%">
                            <telerik:RadTextBox ID="txtNumCtaPago" Runat="server" CssClass="item">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="33%">
                            <asp:CheckBox Visible="true" CssClass="item" ID="chkAduana" runat="server" Text="Incluye información aduanera" AutoPostBack="true" TextAlign="Right" />
                        </td>
                        <td width="33%">
                            <asp:CheckBox ID="chkAddenda" runat="server" CssClass="item" Text="Incluye Addenda AHMSA" AutoPostBack="true" Visible="false" />
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    <asp:Panel ID="panelInformacionAduanera" runat="server" Visible ="false">
                        <tr>
                            <td colspan="3" class="item" style="line-height:20px;">
                                <strong>Nombre de la aduana:</strong> <asp:RequiredFieldValidator ID="valNombreAduana" runat="server" ControlToValidate="nombreaduana" ErrorMessage="Escriba el nombre de la aduana." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadTextBox ID="nombreaduana" Runat="server" Width="450px" CssClass="item">
                                </telerik:RadTextBox>
                                <br />
                                <strong>Fecha de pedimento:</strong> <asp:RequiredFieldValidator ID="valFechaPedimento" runat="server" ControlToValidate="fechapedimento" ErrorMessage="Selecciona la fecha del pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadDatePicker ID="fechapedimento" runat="server">
                                </telerik:RadDatePicker><br />
                                <strong>Número de pedimento:</strong> <asp:RequiredFieldValidator ID="valNumeroPedimento" runat="server" ControlToValidate="numeropedimento" ErrorMessage="Escriba el número de pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadTextBox ID="numeropedimento" Runat="server" Width="450px" CssClass="item">
                                </telerik:RadTextBox>
                                <br />
                            </td>
                        </tr>
                    </asp:Panel>
                    
                    
                    
                </table>

            </fieldset>

        </asp:Panel>
        
        <asp:Panel ID="panelAddendaAHMSA" runat="server" Visible="false">
            <br />
            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblAddendaAHMSA" Text="Addenda AHMSA" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />
                <span style="color:Red; font-weight:bold; font-size:12px;">* Campos obligatorios</span><br /><br />
                <table width="100%">
                        <tr>
                            <td colspan="3" class="item">
                                <table border="0" cellpadding="3" cellspacing="2" align="center" width="100%" style="line-height:20px;">
                                    <tr>
                                        <td>
                                            <strong>Tipo de documento:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:DropDownList ID="adTipo" runat="server" CssClass="item"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Clase de documento:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:DropDownList ID="adClase" runat="server" CssClass="item"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Número de sociedad SAP:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:DropDownList id="adNumsociedad" runat="server" CssClass="item"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Número de división SAP:&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span></strong><br />
                                            <asp:DropDownList ID="adNumdivision" runat="server" CssClass="item"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                            <strong>Número de proveedor:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:TextBox ID="adNumProveedor" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Correo:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:TextBox ID="adCorreo" runat="server" CssClass="box" Width="250px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Clave de moneda:</strong>&nbsp;<span style="color:Red; font-weight:bold; font-size:12px;">*</span><br />
                                            <asp:DropDownList ID="adMoneda" runat="server" CssClass="box"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <strong>Número de pedido:</strong><br />
                                            <asp:TextBox ID="adPedido" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                            <strong>Número de recepción:</strong><br />
                                            <asp:TextBox ID="adRecepcion" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Número de Hoja de Servicio:</strong><br />
                                            <asp:TextBox ID="adHojaServicio" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Número de Transporte:</strong><br />
                                            <asp:TextBox ID="adTransporte" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                        <td>    
                                            <strong>Número de la cuento por pagar:</strong><br />
                                            <asp:TextBox ID="adCtaxPag" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Ejercicio fiscal:</strong><br />
                                            <asp:TextBox ID="adEjercicio" runat="server" CssClass="box"></asp:TextBox>
                                        </td>
                                        <td>
                                            <strong>Fecha Inicio Per. Liquidación</strong><br />
                                            <telerik:RadDatePicker ID="adFechainicio" Runat="server" Skin="Web20">
                                                <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                                    UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                </Calendar>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <strong>Fecha Final Per. Liquidación:</strong><br />
                                            <telerik:RadDatePicker ID="adFechaFin" Runat="server" Skin="Web20">
                                                <Calendar Skin="Web20" UseColumnHeadersAsSelectors="False" 
                                                    UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                </Calendar>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
        </asp:Panel>

        <br />

        <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <asp:HiddenField ID="productoid" runat="server" />
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td  valign="bottom" class="item">
                            <strong>Buscar:</strong> <asp:TextBox ID="txtSearchItem" runat="server" CssClass="box" AutoPostBack="true"></asp:TextBox>&nbsp;presione enter después de escribir el código
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Simple" Visible="False">
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Código</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descripción</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# eval("descripcion") %>' Width="480px" CssClass="box" TextMode="MultiLine" Height="25px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>U. Medida</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnidad" runat="server" Text='<%# eval("unidad") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Cant.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Skin="Default" Width="50px"
                                                    MinValue="0" Value='0'>
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Precio Unit.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0"  Value="0"
                                                    Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descuento.</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkDescuento" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                                        <ItemStyle VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="" UniqueName="cantidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="precio" HeaderText="" UniqueName="precio" DataFormatString="{0:C}" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="" UniqueName="importe" DataFormatString="{0:C}" >
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
                    <td><br /></td>
                </tr>
                </table>

            </fieldset>

        </asp:Panel>

        <br />

        <asp:Panel ID="panelResume" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />

                <table width="100%" align="left">
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblManiobras" runat="server" Text="Maniobras = " CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblManiobrasValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Descuento="></asp:Label>
                            &nbsp;<asp:Label ID="lblDescuentoValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
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
                            <asp:Label ID="lblRetISR" runat="server" CssClass="item" Font-Bold="True" Text="Ret. ISR="></asp:Label>
                            &nbsp;<asp:Label ID="lblRetISRValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 32%">
                            <asp:Label ID="lblRetIVA" runat="server" CssClass="item" Font-Bold="True" Text="Ret. IVA="></asp:Label>
                            &nbsp;<asp:Label ID="lblRetIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <asp:panel ID="panelRetencion" runat="server" Visible="false">
                        <tr>
                            <td width="16%" align="left" style="width: 32%">
                                <asp:Label ID="lblRet" runat="server" CssClass="item" Font-Bold="True" Text="Retención 4%="></asp:Label>
                                &nbsp;<asp:Label ID="lblRetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                    </asp:panel>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" width="16%">
                            <br /><br />
                            <asp:Button ID="btnCreateInvoice" runat="server" CausesValidation="true" CssClass="item" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelInvoice" runat="server" CausesValidation="False" CssClass="item" />    
                            <br /><br />
                        </td>
                    </tr>
                </table>

            </fieldset>

        </asp:Panel>
</asp:Content>

